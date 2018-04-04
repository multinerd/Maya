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
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            if (string.IsNullOrWhiteSpace(asciiText)) return;
            var stArray = asciiText.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var stars = new string('*', stArray[0].Length);
            CenterInConsole(stars);
            foreach (var s in stArray)
            {
                CenterInConsole(s);
            }
            CenterInConsole(stars);
            Console.ResetColor();
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




        // https://stackoverflow.com/questions/2743260/is-it-possible-to-write-to-the-console-in-colour-in-net

        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to the standard output stream, using White text and DarkGreen background.
        /// </summary>
        /// <param name="value"> The value to write. </param>
        public static void WriteConfirm(string value)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to the standard output stream, using White text and DarkCyan background.
        /// </summary>
        /// <param name="value"> The value to write. </param>
        public static void WriteInfo(string value)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value);
            Console.ResetColor();
        }


        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to the standard output stream, using White text and DarkRed background.
        /// </summary>
        /// <param name="value"> The value to write. </param>
        public static void WriteError(string value)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value);
            Console.ResetColor();
        }


        /// <summary>
        /// Writes a blank line, followed by the current line terminator, to the standard output stream.
        /// </summary>
        public static void WriteBlankLine()
        {
            Console.WriteLine(Environment.NewLine);
        }





    }
}
