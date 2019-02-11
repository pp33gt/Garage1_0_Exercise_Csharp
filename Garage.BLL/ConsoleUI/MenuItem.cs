using System;

namespace Garage.BLL.ConsoleUI
{
    public class MenuItem
    {
        public Action Action { get; set; }
        public string Text { get; set; }
        public ConsoleKey ConsoleKey { get; set; }

        internal MenuItem(string text, Action action, ConsoleKey consoleKey)
        {
            Action = action;
            Text = text;
            ConsoleKey = consoleKey;
        }
    }
}
