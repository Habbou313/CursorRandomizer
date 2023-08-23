using Microsoft.Win32;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using IniParser;
using IniParser.Exceptions;
using IniParser.Model;

namespace CursorRandomizer
{
    public partial class MainForm : Form
    {
        private List<Profile> Profiles { get; set; }
        private DataTable CursorTable { get; set; }
        private OpenFileDialog OpenDialog { get; set; }
        private IniData Settings { get; set; }

        public MainForm()
        {
            InitializeComponent();

            // Initialize OpenDialog
            OpenDialog = new OpenFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "Cursors (*.ani, *.cur)|*.ani; *.cur",
                FilterIndex = 0
            };

            // Initialize Settings
            FileIniDataParser iniParser = new FileIniDataParser();
            try
            {
                // Read Settings from settings.ini
                Settings = iniParser.ReadFile("settings.ini", Encoding.UTF8);
            }
            catch (ParsingException)
            {
                // File "settings.ini" not found or corrupted, create a blank one
                Settings = new IniData();
                Settings["Profile"]["currentProfile"] = "Default";
                Settings["Randomization"]["randomizeWhenLogin"] = "false";
                iniParser.WriteFile("settings.ini", Settings, Encoding.UTF8);

                // Clean up randomization registry key
                CleanUp();
            }

            // Initialize Profiles
            try
            {
                // Read Profiles from profiles.json
                using (StreamReader inputFile = new StreamReader("profiles.json", Encoding.UTF8))
                {
                    Profiles = JsonSerializer.Deserialize<List<Profile>>(inputFile.ReadLine());
                }
            }
            catch (Exception)
            {
                // File "profiles.json" not found or corrupted, create a blank one
                using (StreamWriter outputFile = new StreamWriter("profiles.json", false, Encoding.UTF8))
                {
                    Profiles = new List<Profile>();
                    outputFile.WriteLine(JsonSerializer.Serialize<List<Profile>>(Profiles));
                }
            }

            // Initialize ProfileComboBox and RandomCheckedListBox
            RandomCheckedListBox.BeginUpdate();
            ProfileComboBox.BeginUpdate();

            ProfileComboBox.Items.Add("Default");
            foreach (Profile profile in Profiles)
            {
                ProfileComboBox.Items.Add(profile.Name);
                RandomCheckedListBox.Items.Add(profile.Name);
                if (profile.Radomizable)
                {
                    RandomCheckedListBox.SetItemChecked(RandomCheckedListBox.Items.Count - 1, true);
                }
            }
            ProfileComboBox.SelectedItem = Settings["Profile"]["currentProfile"];

            RandomCheckedListBox.EndUpdate();
            ProfileComboBox.EndUpdate();

            // Initialize CursorListBox and CursorTable
            GetCursorTable(ProfileComboBox.SelectedIndex - 1);
            RefreshCursorListBox();

            // Initialize RandomCheckBox
            if (Settings["Randomization"]["randomizeWhenLogin"] == "true")
            {
                RandomCheckBox.Checked = true;
            }
            else
            {
                RandomCheckBox.Checked = false;
            }
        }

        public MainForm(string option)
        {
            if (option != "-r")
            {
                Console.Write("Usage: CursorRandomizer [-r]");
                Environment.Exit(160);
            }

            // Get full path to bypass windows startup problem
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string settingsPath = Path.Combine(appPath, "settings.ini");
            string profilesPath = Path.Combine(appPath, "profiles.json");

            // Initialize Settings
            FileIniDataParser iniParser = new FileIniDataParser();
            try
            {
                // Read Settings from settings.ini
                Settings = iniParser.ReadFile(settingsPath, Encoding.UTF8);
            }
            catch (ParsingException)
            {
                // File "settings.ini" not found or corrupted, create a blank one
                Settings = new IniData();
                Settings["Profile"]["currentProfile"] = "Default";
                Settings["Randomization"]["randomizeWhenLogin"] = "false";
                iniParser.WriteFile(settingsPath, Settings, Encoding.UTF8);

                // Clean up randomization registry key
                CleanUp();

                // No randomization setting, exit immediately
                Environment.Exit(-1);
            }

            // Initialize Profiles
            try
            {
                // Read Profiles from profiles.json
                using (StreamReader inputFile = new StreamReader(profilesPath, Encoding.UTF8))
                {
                    Profiles = JsonSerializer.Deserialize<List<Profile>>(inputFile.ReadLine());
                }
            }
            catch (Exception)
            {
                // File "profiles.json" not found or corrupted, create a blank one
                using (StreamWriter outputFile = new StreamWriter(profilesPath, false, Encoding.UTF8))
                {
                    Profiles = new List<Profile>();
                    outputFile.WriteLine(JsonSerializer.Serialize<List<Profile>>(Profiles));
                }

                // No custom profiles, exit immediately
                Environment.Exit(-1);
            }

            Radomize();
            iniParser.WriteFile(settingsPath, Settings, Encoding.UTF8);
            Environment.Exit(0);
        }

        #region Helper Function
        private Icon GetIcon(string path)
        {
            try
            {
                return Icon.ExtractAssociatedIcon(Environment.ExpandEnvironmentVariables(path));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File \"{path}\" not found");
            }
        }

        private void GetCursorTable(int profileNumber)
        {
            CursorTable = new DataTable();
            CursorTable.Columns.Add("Cursor", typeof(Icon));
            CursorTable.Columns.Add("Name", typeof(string));
            Profile profile = (profileNumber >= 0) ? Profiles[profileNumber] : new Profile();
            for (int i = 0; i < 17; i++)
            {
                try
                {
                    CursorTable.Rows.Add(GetIcon(profile.Cursors[i]), Profile.CursorName[i]);
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message + ", replace with default");
                    profile.Cursors[i] = Profile.DefaultCursors[i];
                    CursorTable.Rows.Add(GetIcon(Profile.DefaultCursors[i]), Profile.CursorName[i]);
                }
            }
        }

        private void RefreshCursorListBox()
        {
            CursorListBox.BeginUpdate();
            int topIndex = CursorListBox.TopIndex;

            CursorListBox.DataSource = null;
            CursorListBox.DataSource = CursorTable;
            CursorListBox.DisplayMember = "Cursor";
            CursorListBox.ValueMember = "Name";

            CursorListBox.TopIndex = topIndex;
            CursorListBox.EndUpdate();
        }

        private void ChangeCursor()
        {
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                string cursorPath = OpenDialog.FileName;
                OpenDialog.InitialDirectory = Path.GetDirectoryName(cursorPath);

                try
                {
                    // Set cursor to the file
                    int profileIndex = ProfileComboBox.SelectedIndex;
                    int cursorIndex = CursorListBox.SelectedIndex;
                    CursorTable.Rows[cursorIndex]["Cursor"] = GetIcon(cursorPath);
                    if (cursorPath.StartsWith(Directory.GetCurrentDirectory()))
                    {
                       cursorPath = Path.GetRelativePath(Directory.GetCurrentDirectory(), cursorPath);
                    }
                    Profiles[profileIndex - 1].Cursors[cursorIndex] = cursorPath;
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                RefreshCursorListBox();
            }
        }

        private void AddProfileMode()
        {
            ProfileComboBox.Visible = false;
            AddButton.Visible = false;
            DeleteButton.Visible = false;

            NewProfileNameTextBox.Visible = true;
            OkButton.Visible = true;
            CancelButton.Visible = true;

            ApplyButton.Enabled = false;
            CursorListBox.Enabled = false;
            ResetButton.Enabled = false;
            BrowseButton.Enabled = false;
            SaveButton.Enabled = false;
            RandomCheckBox.Enabled = false;
            RandomButtom.Enabled = false;
            RandomCheckedListBox.Enabled = false;
        }

        private void NormalMode()
        {
            NewProfileNameTextBox.Visible = false;
            OkButton.Visible = false;
            CancelButton.Visible = false;

            ProfileComboBox.Visible = true;
            AddButton.Visible = true;
            DeleteButton.Visible = true;

            ApplyButton.Enabled = true;
            CursorListBox.Enabled = true;
            ResetButton.Enabled = true;
            BrowseButton.Enabled = true;
            SaveButton.Enabled = true;
            RandomCheckBox.Enabled = true;
            RandomButtom.Enabled = true;
            RandomCheckedListBox.Enabled = true;
        }

        /// <summary>
        /// Apply profile and save all settings
        /// </summary>
        /// <param name="profile"></param>
        private void ApplyProfile(Profile profile)
        {
            // Set currentProfile to selected profile
            Settings["Profile"]["currentProfile"] = profile.Name;

            // Change user cursors
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors", true))
            {
                if (key != null)
                {
                    // Name of attributes of registry key
                    string[] names =
                    {
                        "Arrow",
                        "Help",
                        "AppStarting",
                        "Wait",
                        "Crosshair",
                        "IBeam",
                        "NWPen",
                        "No",
                        "SizeNS",
                        "SizeWE",
                        "SizeNWSE",
                        "SizeNESW",
                        "SizeAll",
                        "UpArrow",
                        "Hand",
                        "Pin",
                        "Person"
                    };

                    for (int i = 0; i < 17; i++)
                    {
                        if (profile.Cursors[i] == Profile.DefaultCursors[i])
                        {
                            // In windows 10 default cursor scheme, crosshair and ibeam aren't set to any file
                            // The files in Profile.DefaultCursors are for display
                            if (i == 4 || i == 5)
                            {
                                key.SetValue(names[i], "");
                            }
                            else
                            {
                                key.SetValue(names[i], profile.Cursors[i]);
                            }
                        }
                        else
                        {
                            // Is absolute path
                            if (profile.Cursors[i].Contains(':'))
                            {
                                key.SetValue(names[i], profile.Cursors[i]);
                            }
                            else
                            {
                                // Get full path to bypass windows startup problem
                                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                                key.SetValue(names[i], Path.Combine(appPath, profile.Cursors[i]));
                            }
                        }
                    }
                }
            }

            // Refresh user cursor
            SystemParametersInfo(0x0057, 0, 0, 0);
        }

        private void Save()
        {
            // Save Profiles to profiles.json
            using (StreamWriter outputFile = new StreamWriter("profiles.json", false, Encoding.UTF8))
            {
                outputFile.WriteLine(JsonSerializer.Serialize<List<Profile>>(Profiles));
            }

            // Save Settings to settings.ini
            FileIniDataParser iniParser = new FileIniDataParser();
            iniParser.WriteFile("settings.ini", Settings, Encoding.UTF8);

            // Save random mode
            if (Settings["Randomization"]["randomizeWhenLogin"] == "true")
            {
                // Modify registry key to turn on random mode
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    if (key != null)
                    {
                        key.SetValue("CursorRandomizer", Application.ExecutablePath + " -r");
                    }
                }
            }
            else
            {
                // Modify registry key to turn off random mode
                CleanUp();
            }
        }

        /// <summary>
        /// Clean up randomization registry key
        /// </summary>
        private void CleanUp()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (key != null)
                {
                    try
                    {
                        key.DeleteValue("CursorRandomizer");
                    }
                    catch (ArgumentException)
                    {
                        // Key not exist, do nothing
                    }
                }
            }
        }

        /// <summary>
        /// Randomize cursor to one of cursors in random list
        /// </summary>
        private void Radomize()
        {
            List<Profile> randomList = new List<Profile>();
            foreach (Profile profile in Profiles)
            {
                if (profile.Radomizable)
                {
                    randomList.Add(profile);
                }
            }

            // Randomly choose a profile
            Random random = new Random();
            int index = random.Next(0, randomList.Count);
            ApplyProfile(randomList[index]);
        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);
        #endregion

        #region Form object event

        #region Profile
        private void ProfileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProfileComboBox.SelectedIndex == 0)
            {
                ResetButton.Enabled = false;
                BrowseButton.Enabled = false;
                DeleteButton.Enabled = false;
            }
            else
            {
                ResetButton.Enabled = true;
                BrowseButton.Enabled = true;
                DeleteButton.Enabled = true;
            }

            GetCursorTable(ProfileComboBox.SelectedIndex - 1);
            RefreshCursorListBox();
            CursorListBox.TopIndex = 0;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddProfileMode();
            NewProfileNameTextBox.Text = "Enter profile name";
            NewProfileNameTextBox.Focus();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Delete \"{ProfileComboBox.Text}\"?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Delete profile
                RandomCheckedListBox.Items.RemoveAt(ProfileComboBox.SelectedIndex - 1);
                Profiles.RemoveAt(ProfileComboBox.SelectedIndex - 1);
                ProfileComboBox.Items.RemoveAt(ProfileComboBox.SelectedIndex);
                ProfileComboBox.SelectedIndex = 0;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (NewProfileNameTextBox.Text == "")
            {
                MessageBox.Show("Profile name can't be empty");
                return;
            }
            else if (NewProfileNameTextBox.Text == "Default")
            {
                MessageBox.Show("Profile name can't be \"Default\"");
                return;
            }

            foreach (Profile profile in Profiles)
            {
                if (NewProfileNameTextBox.Text == profile.Name)
                {
                    MessageBox.Show($"Profile \"{profile.Name}\" already exists");
                    return;
                }
            }

            // Add new profile
            Profiles.Add(new Profile(NewProfileNameTextBox.Text));
            ProfileComboBox.Items.Add(NewProfileNameTextBox.Text);
            ProfileComboBox.SelectedIndex = Profiles.Count;

            RandomCheckedListBox.BeginUpdate();
            RandomCheckedListBox.Items.Add(NewProfileNameTextBox.Text);
            RandomCheckedListBox.SetItemChecked(RandomCheckedListBox.Items.Count - 1, true);
            RandomCheckedListBox.EndUpdate();
            NormalMode();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            NormalMode();
        }
        #endregion

        #region Customize
        private void CursorListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 40;
        }

        private void CursorListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Get row of CursorListBox  
            DataRowView row = (DataRowView)CursorListBox.Items[e.Index];

            e.DrawBackground();
            Graphics g = e.Graphics;

            // Draw cursor
            Rectangle rec = new Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
            g.DrawIconUnstretched((Icon)row["Cursor"], rec);

            // Draw cursor name
            Point p = new Point(e.Bounds.X + e.Bounds.Height + 40, e.Bounds.Y + 8);
            e.Graphics.DrawString((string)row["Name"], e.Font, new SolidBrush(Color.Black), p);
        }

        private void CursorListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ProfileComboBox.SelectedIndex != 0)
            {
                ChangeCursor();
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            // Reset cursor to default
            int profileIndex = ProfileComboBox.SelectedIndex;
            int cursorIndex = CursorListBox.SelectedIndex;
            Profiles[profileIndex - 1].Cursors[cursorIndex] = Profile.DefaultCursors[cursorIndex];
            CursorTable.Rows[cursorIndex]["Cursor"] = GetIcon(Profile.DefaultCursors[cursorIndex]);
            RefreshCursorListBox();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            ChangeCursor();
        }
        #endregion

        #region Randomize
        private void RandomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RandomCheckBox.Checked)
            {
                Settings["Randomization"]["randomizeWhenLogin"] = "true";
            }
            else
            {
                Settings["Randomization"]["randomizeWhenLogin"] = "false";
            }
        }

        private void RandomCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Profiles[e.Index].Radomizable = (e.NewValue == CheckState.Checked);
        }

        private void RandomButtom_Click(object sender, EventArgs e)
        {
            Radomize();

            // Change profile to selected profile
            ProfileComboBox.SelectedItem = Settings["Profile"]["currentProfile"];

            Save();
        }
        #endregion

        /// <summary>
        /// Apply selected profile and save settings and profiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            Profile profile;
            int index = ProfileComboBox.SelectedIndex;
            if (index > 0)
            {
                profile = Profiles[index - 1];
            }
            else
            {
                profile = new Profile();
            }

            ApplyProfile(profile);
            Save();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion
    }
}
