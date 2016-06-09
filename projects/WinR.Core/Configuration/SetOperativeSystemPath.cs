using System;
using System.IO;
using System.Linq;

namespace WinR.Core.Configuration
{
    class SetOperativeSystemPath
    {
        private readonly string DefaultPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            WinRAssemblyInfo.Product + " / " + WinRAssemblyInfo.DefaultShourtcutsFolder);

        internal void Execute(string path = null)
        {
            path = path ?? this.DefaultPath;

            // Validations!!! ???? UnitTests
            var oldPath = string.Empty;//Settings.Default.ShortcutsPath;
            var allPaths = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            string[] paths = allPaths.Split(';');

            // Used GetFullPath to normalize strings from '/' to '\'
            string oldShortcutPathFound = paths.FirstOrDefault(x => Path.GetFullPath(x) == Path.GetFullPath(oldPath));

            if (oldShortcutPathFound != null)
            {
                allPaths.Replace(oldShortcutPathFound, path);
            }
            else
            {
                allPaths += allPaths.TrimEnd().EndsWith(";") ? path : ";" + path;
            }

            Environment.SetEnvironmentVariable("PATH", allPaths, EnvironmentVariableTarget.User);
        }
    }
}
