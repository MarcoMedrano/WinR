namespace WinR.Core.Installation
{
    using System;
    using System.IO;
    using System.Reflection;

    using IWshRuntimeLibrary;

    class InstallWinRShortCuts
    {
        internal void Execute()
        {
            var sendToFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SendTo), WinRAssemblyInfo.Product + ".lnk");
            var desktopFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), WinRAssemblyInfo.Product + ".lnk");
            string winRexecutableFilePath = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName, WinRAssemblyInfo.Product + ".exe");

            this.MakeShortcut(desktopFilePath, winRexecutableFilePath);
            this.MakeShortcut(sendToFilePath, winRexecutableFilePath);
        }

        private void MakeShortcut(string shortcutFileName, string targetFileName)
        {
            IWshShell shell = new WshShell();
            IWshShortcut shortcut = shell.CreateShortcut(shortcutFileName) as IWshShortcut;
            shortcut.TargetPath = targetFileName;
            shortcut.Description = "Made with WinR!!! \n" + targetFileName;//Add path

            shortcut.Save();
        }
    }
}
