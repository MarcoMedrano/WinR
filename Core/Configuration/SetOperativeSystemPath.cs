using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinR.Properties;

namespace WinR.Core.Configuration
{
    class SetOperativeSystemPath
    {
        internal void Execute(string newPath)
        {
            
            // Validations!!! ???? UnitTests
            var oldPath = Settings.Default.ShortcutsPath;
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
