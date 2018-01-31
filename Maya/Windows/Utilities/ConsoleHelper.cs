using System;

namespace Maya.Windows.Utilities
{
    /// <summary>
    /// A Console Helper
    /// </summary>
    public class ConsoleHelpers
    {
        /// <summary>
        /// Centers text in console in between 2 dividers.
        /// </summary>
        /// <param name="asciiText"> The string to pass. </param>
        public static void PrintProgramName(string asciiText)
        {
            if (string.IsNullOrWhiteSpace(asciiText)) return;
            var stArray = asciiText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var stars = new string('*', stArray[0].Length);
            CenterInConsole(stars);
            foreach (var s in stArray)
            {
                CenterInConsole(s);
            }
            CenterInConsole(stars);
        }


        /// <summary>
        /// Centers text in the console.
        /// </summary>
        /// <param name="text"> The text to center. </param>
        public static void CenterInConsole(string text)
        {
            var count = (Console.WindowWidth - text.Length) / 2;
            if (count < 0) count = 0;
            Console.WriteLine($"{new string(' ', count)} {text}");
        }
    }
}
