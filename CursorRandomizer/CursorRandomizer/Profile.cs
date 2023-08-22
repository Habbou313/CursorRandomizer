namespace CursorRandomizer
{
    /// <summary>
    /// Containing information of a cursor porifle
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Path of default cursors
        /// </summary>
        public static string[] DefaultCursors { get; } =
        {
            @"%SystemRoot%\Cursors\aero_arrow.cur",
            @"%SystemRoot%\Cursors\aero_helpsel.cur",
            @"%SystemRoot%\Cursors\aero_working.ani",
            @"%SystemRoot%\Cursors\aero_busy.ani",
            @"%SystemRoot%\Cursors\lcross.cur",
            @"%SystemRoot%\Cursors\beam_r.cur",
            @"%SystemRoot%\Cursors\aero_pen.cur",
            @"%SystemRoot%\Cursors\aero_unavail.cur",
            @"%SystemRoot%\Cursors\aero_ns.cur",
            @"%SystemRoot%\Cursors\aero_ew.cur",
            @"%SystemRoot%\Cursors\aero_nwse.cur",
            @"%SystemRoot%\Cursors\aero_nesw.cur",
            @"%SystemRoot%\Cursors\aero_move.cur",
            @"%SystemRoot%\Cursors\aero_up.cur",
            @"%SystemRoot%\Cursors\aero_link.cur",
            @"%SystemRoot%\Cursors\aero_pin.cur",
            @"%SystemRoot%\Cursors\aero_person.cur"
        };

        /// <summary>
        /// Name of cursors
        /// </summary>
        public static string[] CursorName { get; } =
        {
            "Normal select",
            "Help select",
            "Working in background",
            "Busy",
            "Crosshair",
            "Text select",
            "Pen",
            "Not allow",
            "NS resize",
            "EW resize",
            "NWSE resize",
            "NESW resize",
            "Move",
            "Up",
            "Link",
            "Pin",
            "Person"
        };

        public string Name { get; set; }

        /// <summary>
        /// Path of cursors
        /// </summary>
        public string[] Cursors { get; set; }

        public bool Radomizable { get; set; }

        public Profile()
        {
            Name = "Default";
            Cursors = (string[])DefaultCursors.Clone();
            Radomizable = true;
        }

        public Profile(string name) : this()
        {
            this.Name = name;
        }
    }
}
