using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;

namespace WinR.Core.Configuration
{
    class InstallWinRShortCut
    {
        internal void Execute()
        {
            var sendToFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SendTo), WinRAssemblyInfo.Product + ".lnk");
            var desktopFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), WinRAssemblyInfo.Product + ".lnk");
            string winRexecutableFilePath = Environment.GetCommandLineArgs()[0];

            this.MakeShortcut(sendToFilePath, winRexecutableFilePath);
            this.MakeShortcut(desktopFilePath, winRexecutableFilePath);
        }

        private void MakeShortcut(string fileName, string targetFileName)
        {
            IWshShell shell = new WshShell();
            IWshShortcut shortcut = shell.CreateShortcut(fileName) as IWshShortcut;
            shortcut.TargetPath = targetFileName;
            shortcut.Description = "Made with WinR!!! \n" + targetFileName;//Add path

            shortcut.Save();
        }
    }
}
