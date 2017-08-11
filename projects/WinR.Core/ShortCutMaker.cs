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
                directory.Create();

            string linkPath = Path.Combine(WinRAssemblyInfo.DefaultShourtcutsFolder, this.model.ShortcutName + ".lnk");
            IWshShell shell = new WshShell();
            IWshShortcut shortcut = shell.CreateShortcut(linkPath) as IWshShortcut;
            shortcut.Arguments = "";
            shortcut.TargetPath = this.model.FilePath;
            shortcut.WorkingDirectory = new FileInfo(this.model.FilePath).Directory.FullName;
            // not sure about what this is for
            shortcut.WindowStyle = 1;
            shortcut.Description = "Made with WinR!!!";//Add path
            shortcut.Save();

            if (this.model.RunAsAdministrator)
                this.SetAsAdministrator(linkPath);
        }

        private void SetAsAdministrator(string linkPath)
        {
            using (FileStream fs = new FileStream(linkPath, FileMode.Open, FileAccess.ReadWrite))
            {
                fs.Seek(21, SeekOrigin.Begin);
                fs.WriteByte(0x22);
            }
        }
    }
}
