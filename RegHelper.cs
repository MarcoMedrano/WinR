using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace WinR
{
    static class RegHelper
    {
        public static IEnumerable<string> RecommendedPrograms(string ext)
        {
            List<string> progs = new List<string>();

            string baseKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\" + ext;

            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(baseKey + @"\OpenWithList"))
            {
                string mruList = (string) registryKey?.GetValue("MRUList");
                if (mruList != null)
                {
                    progs.AddRange(mruList.Select(c => registryKey.GetValue(c.ToString()).ToString()));
                }
            }

            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(baseKey + @"\OpenWithProgids"))
            {
                if (registryKey != null)
                {
                    progs.AddRange(registryKey.GetValueNames());
                }
                //TO DO: Convert ProgID to ProgramName, etc.
            }

            return progs;
        }

        public static string GetPathForAppName(string program)
        {
            string[] appPaths =
            {
                @"Software\Microsoft\Windows\CurrentVersion\App Paths\",
                @"WOW6432Node\Software\Microsoft\Windows\CurrentVersion\App Paths\",
            };
            string path = null;

            foreach (var appPath in appPaths)
            {
                using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey(appPath + program))
                {
                    if (regKey != null)
                        path = regKey.GetValue("").ToString();

                    if (string.IsNullOrEmpty(path) && regKey != null)
                        path = regKey.GetValue("Path").ToString();

                    if (string.IsNullOrEmpty(path) == false)
                        return path.Replace("\"", string.Empty);
                }
            }

            return path;
        }
    }
}
