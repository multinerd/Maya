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
        public string FolderName { get; private set; }      // My Folder

        public string FullPath { get; private set; }        // C:/Temp/My Folder

        public DirectoryHelper(string folder, string tempPath = null)
        {
            FolderName = folder;                                        
            FullPath = Path.Combine(tempPath ?? Path.GetTempPath(), FolderName);    
            Directory.CreateDirectory(FullPath);
        }

        public string PathFor(string path)                  // C:/Temp/My Folder/{path}
        {
            return Path.Combine(FullPath, path);
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
}
