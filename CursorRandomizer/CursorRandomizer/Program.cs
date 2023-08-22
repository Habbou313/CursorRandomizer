namespace CursorRandomizer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            switch (args.Length)
            {
                case 0:
                    Application.Run(new MainForm());
                    break;
                case 1:
                    Application.Run(new MainForm(args[0]));
                    break;
                default:
                    Console.Write("Usage: CursorRandomizer [-r]");
                    break;
            }
        }
    }
}