using System;
using System.IO;
using System.Linq;

namespace WinR.Core.Configuration
{
    //TODO Create another class to remove path when uninstalling
    class SetOperativeSystemPath
    {

        internal void Execute(string path = null)
        {
            path = path ?? WinRAssemblyInfo.DefaultShortcutsPath;

            // Validations!!! ???? UnitTests
            var oldPath = path;// For next version use: Settings.Default.ShortcutsPath;
            var allPaths = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            string[] paths = allPaths?.Split(';');

            // Used GetFullPath to normalize strings from '/' to '\'
            string oldShortcutPathFound = paths?.FirstOrDefault(x => Path.GetFullPath(x) == Path.GetFullPath(oldPath));

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
