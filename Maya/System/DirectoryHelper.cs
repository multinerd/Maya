using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Maya.System
{
    /// <summary>
    /// A bunch of useful Directory methods.
    /// </summary>
    public class DirectoryHelper
    {
        /// <summary>
        /// Returns the Folder Name ONLY
        /// </summary>
        public string FolderName { get; private set; }      // My Folder

        /// <summary>
        /// Get the directory's full path.
        /// </summary>
        public string FullPath { get; private set; }        // C:/Temp/My Folder

        /// <summary>
        /// Create a Directory to act as a temp folder.
        /// </summary>
        /// <param name="folder">The folder name to use.</param>
        /// <param name="tempPath">The folder path to use. Defaults to 'Path.GetTempPath()'</param>
        public DirectoryHelper(string folder, string tempPath = null)
        {
            FolderName = folder;                                        
            FullPath = Path.Combine(tempPath ?? Path.GetTempPath(), FolderName);    
            Directory.CreateDirectory(FullPath);
        }

        /// <summary>
        /// Return a string by combining the temp directory's full path and the path you provide.
        /// </summary>
        /// <param name="path"> The path to append.</param>
        /// <returns></returns>
        public string PathFor(string path)                  // C:/Temp/My Folder/{path}
        {
            return Path.Combine(FullPath, path);
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



        // Static 
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
        public static void Clean(this DirectoryInfo directory)
        {
            foreach (var file in directory.EnumerateFiles()) file.Delete();
            foreach (var subDirectory in directory.EnumerateDirectories()) subDirectory.Delete(true);
        }
    }
}
