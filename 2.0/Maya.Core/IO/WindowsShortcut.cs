using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace Maya.Core.IO
{
    /// <summary>
    /// Create a Windows shortcut pointing to a file or folder.
    /// </summary>
    public class WindowsShortcut
    {
        private readonly string _sourceDestination;
        private readonly string _targetDestination;
        private readonly string _targetFileName;
        private readonly WshShell _shell;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceDestination"> The path the shortcut will be created from. </param>
        /// <param name="targetDestination"> The location where the shortcut will be placed. </param>
        /// <param name="targetFileName"> The name of the shortcut file created. <para/>NOTE: DO NOT include the '.lnk' file extension. </param>
        public WindowsShortcut(string sourceDestination, string targetDestination, string targetFileName)
        {
            _sourceDestination = sourceDestination;
            _targetDestination = targetDestination;
            _targetFileName = targetFileName;
            _shell = new WshShell();
        }

        /// <summary>
        /// Create the window shortcut.
        /// </summary>
        public void Create()
        {
            if (File.Exists(GetShortcutPath)) return;

            if (_shell.CreateShortcut(GetShortcutPath) is IWshShortcut shortcut)
            {
                shortcut.TargetPath = _sourceDestination;
                shortcut.Save();
            }
        }



        private string GetShortcutPath => Path.Combine(_targetDestination, $"{_targetFileName}.lnk");
    }
}
