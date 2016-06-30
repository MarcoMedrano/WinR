
namespace WinR.Core.Installation
{
    using System;
    using System.IO;

    class UninstallWinRShortCuts
    {
        internal void Execute()
        {
            var sendToFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SendTo), WinRAssemblyInfo.Product + ".lnk");
            var desktopFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), WinRAssemblyInfo.Product + ".lnk");

            if (File.Exists(sendToFilePath))
                File.Delete(sendToFilePath);

            if (File.Exists(desktopFilePath))
                File.Delete(desktopFilePath);
        }
    }
}
