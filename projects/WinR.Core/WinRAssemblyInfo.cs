namespace WinR.Core
{
    using System;
    using System.IO;

    static class WinRAssemblyInfo
    {
        internal const string Company = "Universal";
        internal const string Product = "WinR";
        internal const string Copyright = "Copyright © Universal 2016";
        internal const string DefaultShourtcutsFolder = "shortcuts";

        internal static string DefaultShortcutsPath { get; private set; }

        static WinRAssemblyInfo()
        {
            DefaultShortcutsPath = Path.Combine(
                                                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                                                Product + "\\" + DefaultShourtcutsFolder);
        }

    }
}
