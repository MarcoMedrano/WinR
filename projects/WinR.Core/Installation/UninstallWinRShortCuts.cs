
namespace WinR.Core.Installation
{
    using System;
    using System.IO;

    class UninstallWinRShortCuts
    {
        internal void Execute()
        {
            var sendToFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SendTo), WinRAssemblyInfo.Product + ".lnk");
            var desktopFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), WinRAssemblyInfo.Product + ".lnk");
            
            File.Delete(sendToFilePath);
            File.Delete(desktopFilePath);
        }
    }
}
