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
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), WinRAssemblyInfo.Company, WinRAssemblyInfo.Product, "shortcuts");
            var directory = new DirectoryInfo(path);
            if (directory.Exists == false)
            {
                directory.Create();
            }

            IWshShell shell = new WshShell();
            IWshShortcut shortcut = shell.CreateShortcut(Path.Combine(path, model.ShortcutName + ".lnk")) as IWshShortcut;
            shortcut.Arguments = "";
            shortcut.TargetPath = model.FilePath;
            // not sure about what this is for
            shortcut.WindowStyle = 1;
            shortcut.Description = "Made with WinR!!!";//Add path
            //shortcut.WorkingDirectory = "c:\\app";
            //shortcut.IconLocation = "specify icon location";
            shortcut.Save();
        }
    }
}
