using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Maya.System
{
    /// <summary>
    /// 
    /// </summary>
    public enum GenerateFileNameType
    {
        /// <summary> Generate a file name based on <see cref="T:System.DateTime.Now.Ticks"/> </summary>
        Date,

        /// <summary> Generate a file name based on <see cref="M:System.DateTime.Now.Ticks.GetHashCode()"/> </summary>
        DateHash,

        /// <summary> Generate a file name using a <see cref="T:System.Guid"/> </summary>
        Guid,

        /// <summary> Generate a random file name </summary>
        Random
    }



    /// <summary>
    /// A bunch of useful Directory methods.
    /// </summary>
    public class DirectoryHelper
    {
        /// <summary> Returns the Folder Name ONLY </summary>
        public string FolderName { get; private set; }                  // My Folder

        /// <summary> Get the directory's base path. </summary>
        public string BasePath { get; private set; }                    // C:/Temp

        /// <summary> Get the directory's full path. </summary>
        public string FullPath => Path.Combine(BasePath, FolderName);   // C:/Temp/My Folder

        /// <summary> Create a Directory to act as the applications temp folder. </summary>
        /// <param name="folderName">The folder name to use.</param>
        /// <param name="basePath">The base path to use. Defaults to <see cref="M:System.IO.Path.GetTempPath()"/></param>
        /// <param name="createIfNeeded">Create the folder if it does not exist. Defaults to true.</param>
        public DirectoryHelper(string folderName, string basePath = null, bool createIfNeeded = true)
        {
            FolderName = folderName;
            BasePath = Path.Combine(basePath ?? Path.GetTempPath(), FolderName);

            if (createIfNeeded)
                Directory.CreateDirectory(FullPath);
        }

        /// <summary>
        /// Return a string by combining the temp directory's full path and the path you provide.
        /// </summary>
        /// <param name="path"> The path to append.</param>
        /// <returns></returns>
        public string GetPathFor(string path)                  // C:/Temp/My Folder/{path}
        {
            return Path.Combine(FullPath, path);
        }





        /// <summary>
        /// Generate a random file name.
        /// </summary>
        /// <param name="g"> The type of method used to generate file name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string GenerateFileName(GenerateFileNameType g)
        {
            switch (g)
            {
                case GenerateFileNameType.Date:
                    return $"{DateTime.Now.Ticks}";

                case GenerateFileNameType.DateHash:
                    return $"{DateTime.Now.Ticks.GetHashCode().ToString("x").ToUpper()}";

                case GenerateFileNameType.Guid:
                    return $"{Guid.NewGuid()}";

                case GenerateFileNameType.Random:
                    return Path.GetRandomFileName();

                default:
                    throw new ArgumentOutOfRangeException(nameof(g), g, null);
            }
        }

        /// <summary>
        /// Get the next available name for a file. (Ex: if 'C:\MyTestFile.html' exists, 'C:\MyTestFile2.html' will be returned.)
        /// </summary>
        /// <param name="filename"> The file path. </param>
        /// <returns> Returns the next available filename. </returns>
        public string GetNextAvailableFilename(string filename)
        {
            if (!File.Exists(filename)) return filename;

            string alternateFilename;
            var fileNameIndex = 1;
            do
            {
                fileNameIndex += 1;
                alternateFilename = CreateNumberedFilename(filename, fileNameIndex);
            } while (File.Exists(alternateFilename));

            return alternateFilename;
        }

        private string CreateNumberedFilename(string filename, int number)
        {
            string plainName = Path.GetFileNameWithoutExtension(filename);
            string extension = Path.GetExtension(filename);
            return $"{plainName}{number}{extension}";
        }




        /// <summary>
        /// Delete all files from the temp path.
        /// </summary>
        public void Clean()
        {
            var di = new DirectoryInfo(FullPath);
            foreach (var file in di.EnumerateFiles()) file.Delete();
            foreach (var subDirectory in di.EnumerateDirectories()) subDirectory.Delete(true);
        }



        public static bool IsDirectoryVisible(string path)
        {
            try
            {
                Directory.GetAccessControl(path);
                return true;
            }
            catch (UnauthorizedAccessException) { return true; }
            catch { return false; }
        }


    }




    /// <summary>
    /// Directory Extensions
    /// </summary>
    public static class DirectoryExtensions
    {
        /// <summary>
        /// Delete all files from the temp path.
        /// </summary>
        /// <param name="directory"> the directory to clean</param>
        public static void DeleteAllFiles(this DirectoryInfo directory)
        {
            foreach (var file in directory.EnumerateFiles()) file.Delete();
            foreach (var subDirectory in directory.EnumerateDirectories()) subDirectory.Delete(true);
        }
    }
}
