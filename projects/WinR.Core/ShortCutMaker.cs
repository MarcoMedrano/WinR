using System;
using System.IO;
using IWshRuntimeLibrary;
using WinR.Core.Model;

namespace WinR.Core
{
    class ShortCutMaker
    {
        private readonly FileToShortCutModel model;

        public ShortCutMaker(FileToShortCutModel model)
        {
            this.model = model;
        }

        public void Make()
        {
            var directory = new DirectoryInfo(WinRAssemblyInfo.DefaultShourtcutsFolder);
            if (directory.Exists == false)
            {
                directory.Create();
            }

            IWshShell shell = new WshShell();
            IWshShortcut shortcut = shell.CreateShortcut(Path.Combine(WinRAssemblyInfo.DefaultShourtcutsFolder, this.model.ShortcutName + ".lnk")) as IWshShortcut;
            shortcut.Arguments = "";
            shortcut.TargetPath = this.model.FilePath;
            shortcut.WorkingDirectory = new FileInfo(this.model.FilePath).Directory.FullName;
            // not sure about what this is for
            shortcut.WindowStyle = 1;
            shortcut.Description = "Made with WinR!!!";//Add path
            shortcut.Save();
        }
    }
}
