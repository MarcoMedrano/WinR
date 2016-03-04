using System;
using System.IO;
using System.Linq;

namespace WinR.Core.Configuration
{
    class SetOperativeSystemPath
    {
        internal void Execute(string newPath)
        {
            
            // Validations!!! ???? UnitTests
            var oldPath = string.Empty;//Settings.Default.ShortcutsPath;
            var allPaths = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            string[] paths = allPaths.Split(';');

            // Used GetFullPath to normalize strings from '/' to '\'
            string oldShortcutPathFound = paths.FirstOrDefault(x => Path.GetFullPath(x) == Path.GetFullPath(oldPath));

            if (oldShortcutPathFound != null)
            {
                allPaths.Replace(oldShortcutPathFound, newPath);
            }
            else
            {
                allPaths += allPaths.TrimEnd().EndsWith(";") ? newPath : ";" + newPath;
            }

            Environment.SetEnvironmentVariable("PATH", allPaths, EnvironmentVariableTarget.User);

        }
    }
}
