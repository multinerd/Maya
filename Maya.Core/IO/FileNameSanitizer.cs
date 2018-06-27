using System;
using System.IO;
using System.Text;

namespace Maya.Windows.Utilities
{
    /// <summary>
    /// Cleans paths of invalid characters.
    /// </summary>
    public static class FileNameSanitizer
    {
        private static readonly char[] InvalidFilenameChars;

        private static readonly char[] InvalidPathChars;

        static FileNameSanitizer()
        {
            InvalidFilenameChars = Path.GetInvalidFileNameChars();
            InvalidPathChars = Path.GetInvalidPathChars();
            Array.Sort(InvalidFilenameChars);
            Array.Sort(InvalidPathChars);
        }

        /// <summary> Cleans a filename of invalid characters </summary>
        /// <param name="input">the string to clean</param>
        /// <param name="errorChar">the character which replaces bad characters</param>
        /// <returns> A sanitized filename </returns>
        public static string SanitizeFilename(string input, char errorChar = '_')
        {
            return Sanitize(input, InvalidFilenameChars, errorChar);
        }

        /// <summary> Cleans a path of invalid characters </summary>
        /// <param name="input">the string to clean</param>
        /// <param name="errorChar">the character which replaces bad characters</param>
        /// <returns> A sanitized path </returns>
        public static string SanitizePath(string input, char errorChar = '_')
        {
            return Sanitize(input, InvalidPathChars, errorChar);
        }

        private static string Sanitize(string input, char[] invalidChars, char errorChar)
        {
            if (input == null)
                return null; 

            var result = new StringBuilder();

            foreach (var characterToTest in input)
                result.Append(Array.BinarySearch(invalidChars, characterToTest) >= 0 ? errorChar : characterToTest);

            return result.ToString();
        }

    }
}
