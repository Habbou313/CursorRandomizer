namespace CursorRandomizer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ProfileGroupBox = new GroupBox();
            ProfileComboBox = new ComboBox();
            AddButton = new Button();
            DeleteButton = new Button();
            OkButton = new Button();
            CancelButton = new Button();
            NewProfileNameTextBox = new TextBox();
            ApplyButton = new Button();
            CursorListBox = new ListBox();
            CustomizeGroupBox = new GroupBox();
            BrowseButton = new Button();
            ResetButton = new Button();
            SaveButton = new Button();
            RandomizeGroupBox = new GroupBox();
            RandomListLabel = new Label();
            RandomButtom = new Button();
            RandomCheckBox = new CheckBox();
            RandomCheckedListBox = new CheckedListBox();
            ProfileGroupBox.SuspendLayout();
            CustomizeGroupBox.SuspendLayout();
            RandomizeGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // ProfileGroupBox
            // 
            ProfileGroupBox.Controls.Add(ProfileComboBox);
            ProfileGroupBox.Controls.Add(AddButton);
            ProfileGroupBox.Controls.Add(DeleteButton);
            ProfileGroupBox.Controls.Add(OkButton);
            ProfileGroupBox.Controls.Add(CancelButton);
            ProfileGroupBox.Controls.Add(NewProfileNameTextBox);
            ProfileGroupBox.Location = new Point(416, 9);
            ProfileGroupBox.Margin = new Padding(2);
            ProfileGroupBox.Name = "ProfileGroupBox";
            ProfileGroupBox.Padding = new Padding(2);
            ProfileGroupBox.Size = new Size(260, 124);
            ProfileGroupBox.TabIndex = 0;
            ProfileGroupBox.TabStop = false;
            ProfileGroupBox.Text = "Profiles";
            // 
            // ProfileComboBox
            // 
            ProfileComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ProfileComboBox.FormattingEnabled = true;
            ProfileComboBox.Location = new Point(21, 30);
            ProfileComboBox.Margin = new Padding(2);
            ProfileComboBox.Name = "ProfileComboBox";
            ProfileComboBox.Size = new Size(218, 27);
            ProfileComboBox.TabIndex = 0;
            ProfileComboBox.SelectedIndexChanged += ProfileComboBox_SelectedIndexChanged;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(31, 75);
            AddButton.Margin = new Padding(2);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(94, 29);
            AddButton.TabIndex = 1;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(135, 75);
            DeleteButton.Margin = new Padding(2);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(94, 29);
            DeleteButton.TabIndex = 3;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // OkButton
            // 
            OkButton.Location = new Point(31, 75);
            OkButton.Margin = new Padding(2);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(94, 29);
            OkButton.TabIndex = 5;
            OkButton.Text = "OK";
            OkButton.UseVisualStyleBackColor = true;
            OkButton.Visible = false;
            OkButton.Click += OkButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(135, 75);
            CancelButton.Margin = new Padding(2);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(94, 29);
            CancelButton.TabIndex = 6;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Visible = false;
            CancelButton.Click += CancelButton_Click;
            // 
            // NewProfileNameTextBox
            // 
            NewProfileNameTextBox.Location = new Point(21, 30);
            NewProfileNameTextBox.Margin = new Padding(2);
            NewProfileNameTextBox.Name = "NewProfileNameTextBox";
            NewProfileNameTextBox.Size = new Size(218, 27);
            NewProfileNameTextBox.TabIndex = 4;
            NewProfileNameTextBox.Visible = false;
            // 
            // ApplyButton
            // 
            ApplyButton.Location = new Point(576, 463);
            ApplyButton.Margin = new Padding(2);
            ApplyButton.Name = "ApplyButton";
            ApplyButton.Size = new Size(94, 29);
            ApplyButton.TabIndex = 7;
            ApplyButton.Text = "Apply";
            ApplyButton.UseVisualStyleBackColor = true;
            ApplyButton.Click += ApplyButton_Click;
            // 
            // CursorListBox
            // 
            CursorListBox.DrawMode = DrawMode.OwnerDrawVariable;
            CursorListBox.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            CursorListBox.FormattingEnabled = true;
            CursorListBox.ItemHeight = 19;
            CursorListBox.Location = new Point(10, 24);
            CursorListBox.Margin = new Padding(2);
            CursorListBox.Name = "CursorListBox";
            CursorListBox.Size = new Size(372, 406);
            CursorListBox.TabIndex = 1;
            CursorListBox.DrawItem += CursorListBox_DrawItem;
            CursorListBox.MeasureItem += CursorListBox_MeasureItem;
            CursorListBox.MouseDoubleClick += CursorListBox_MouseDoubleClick;
            // 
            // CustomizeGroupBox
            // 
            CustomizeGroupBox.Controls.Add(BrowseButton);
            CustomizeGroupBox.Controls.Add(ResetButton);
            CustomizeGroupBox.Controls.Add(CursorListBox);
            CustomizeGroupBox.Location = new Point(9, 9);
            CustomizeGroupBox.Margin = new Padding(2);
            CustomizeGroupBox.Name = "CustomizeGroupBox";
            CustomizeGroupBox.Padding = new Padding(2);
            CustomizeGroupBox.Size = new Size(392, 483);
            CustomizeGroupBox.TabIndex = 3;
            CustomizeGroupBox.TabStop = false;
            CustomizeGroupBox.Text = "Customize";
            // 
            // BrowseButton
            // 
            BrowseButton.Location = new Point(288, 444);
            BrowseButton.Margin = new Padding(2);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Size = new Size(94, 29);
            BrowseButton.TabIndex = 3;
            BrowseButton.Text = "Browse";
            BrowseButton.UseVisualStyleBackColor = true;
            BrowseButton.Click += BrowseButton_Click;
            // 
            // ResetButton
            // 
            ResetButton.Location = new Point(184, 444);
            ResetButton.Margin = new Padding(2);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(94, 29);
            ResetButton.TabIndex = 2;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = true;
            ResetButton.Click += ResetButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(472, 463);
            SaveButton.Margin = new Padding(2);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(94, 29);
            SaveButton.TabIndex = 4;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // RandomizeGroupBox
            // 
            RandomizeGroupBox.Controls.Add(RandomListLabel);
            RandomizeGroupBox.Controls.Add(RandomButtom);
            RandomizeGroupBox.Controls.Add(RandomCheckBox);
            RandomizeGroupBox.Controls.Add(RandomCheckedListBox);
            RandomizeGroupBox.Location = new Point(416, 148);
            RandomizeGroupBox.Margin = new Padding(2);
            RandomizeGroupBox.Name = "RandomizeGroupBox";
            RandomizeGroupBox.Padding = new Padding(2);
            RandomizeGroupBox.Size = new Size(260, 302);
            RandomizeGroupBox.TabIndex = 6;
            RandomizeGroupBox.TabStop = false;
            RandomizeGroupBox.Text = "Randomize";
            // 
            // RandomListLabel
            // 
            RandomListLabel.AutoSize = true;
            RandomListLabel.Location = new Point(9, 90);
            RandomListLabel.Name = "RandomListLabel";
            RandomListLabel.Size = new Size(93, 19);
            RandomListLabel.TabIndex = 3;
            RandomListLabel.Text = "Random list";
            // 
            // RandomButtom
            // 
            RandomButtom.Location = new Point(76, 45);
            RandomButtom.Name = "RandomButtom";
            RandomButtom.Size = new Size(108, 29);
            RandomButtom.TabIndex = 2;
            RandomButtom.Text = "Randomize";
            RandomButtom.UseVisualStyleBackColor = true;
            RandomButtom.Click += RandomButtom_Click;
            // 
            // RandomCheckBox
            // 
            RandomCheckBox.AutoSize = true;
            RandomCheckBox.Location = new Point(14, 21);
            RandomCheckBox.Margin = new Padding(2);
            RandomCheckBox.Name = "RandomCheckBox";
            RandomCheckBox.Size = new Size(240, 23);
            RandomCheckBox.TabIndex = 1;
            RandomCheckBox.Text = "Randomize cursor when login";
            RandomCheckBox.UseVisualStyleBackColor = true;
            RandomCheckBox.CheckedChanged += RandomCheckBox_CheckedChanged;
            // 
            // RandomCheckedListBox
            // 
            RandomCheckedListBox.CheckOnClick = true;
            RandomCheckedListBox.FormattingEnabled = true;
            RandomCheckedListBox.Location = new Point(10, 111);
            RandomCheckedListBox.Margin = new Padding(2);
            RandomCheckedListBox.Name = "RandomCheckedListBox";
            RandomCheckedListBox.Size = new Size(240, 180);
            RandomCheckedListBox.TabIndex = 0;
            RandomCheckedListBox.ItemCheck += RandomCheckedListBox_ItemCheck;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 502);
            Controls.Add(ApplyButton);
            Controls.Add(RandomizeGroupBox);
            Controls.Add(SaveButton);
            Controls.Add(CustomizeGroupBox);
            Controls.Add(ProfileGroupBox);
            Font = new Font("Microsoft JhengHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(2);
            Name = "MainForm";
            Text = "Cursor Randomizer";
            ProfileGroupBox.ResumeLayout(false);
            ProfileGroupBox.PerformLayout();
            CustomizeGroupBox.ResumeLayout(false);
            RandomizeGroupBox.ResumeLayout(false);
            RandomizeGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox ProfileGroupBox;
        private ComboBox ProfileComboBox;
        private Button AddButton;
        private Button DeleteButton;
        private ListBox CursorListBox;
        private GroupBox CustomizeGroupBox;
        private Button BrowseButton;
        private Button ResetButton;
        private Button SaveButton;
        private TextBox NewProfileNameTextBox;
        private Button CancelButton;
        private Button OkButton;
        private GroupBox RandomizeGroupBox;
        private CheckBox RandomCheckBox;
        private CheckedListBox RandomCheckedListBox;
        private Button ApplyButton;
        private Button RandomButtom;
        private Label RandomListLabel;
    }
}