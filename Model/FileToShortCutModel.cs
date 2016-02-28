using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WinR.Core;

namespace WinR.Model
{
    class FileToShortCutModel
    {

        public string FilePath { get; private set; }
        public string Extension { get; private set; }
        public bool IsExecutable { get; private set; }
        public string ShortcutName { get; set; }
        public bool RunAsAdministrator { get; set; }

        public List<DisplayableFile> ExecutableFiles { get; set; }

        public DisplayableFile SelectedExecutableFile { get; set; }


        public FileToShortCutModel(string tentativeFilePath)
        {
            var fileInfo = new FileInfo(tentativeFilePath);
            
            this.ShortcutName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

            if (this.IsLink(fileInfo.Extension))
            {
                fileInfo = this.ResolveLink(fileInfo);
            }

            this.FilePath = fileInfo.FullName;
            this.Extension = fileInfo.Extension;
            this.IsExecutable = CheckIfIsExecutable(fileInfo.Extension);

            this.ExecutableFiles = this.BuildExecutableFilesList();
            this.SelectedExecutableFile = this.SelectExecutableFile();
        }
        
        private List<DisplayableFile> BuildExecutableFilesList()
        {
            var list = new List<DisplayableFile>();
            list.Add(DisplayableFile.Default);
            #region TestingQueries

            //foreach (var assocF in Enum.GetValues(typeof(Win32Api.AssocF)))
            //{
            //    for (int i = 1; i <= 20; i++)
            //    {
            //        try
            //        {
            //            var res = Win32Api.AssocQueryString((Win32Api.AssocF)assocF, (Win32Api.AssocStr)i, "XamarinStudio.exe");
            //            Console.WriteLine("{0}.{1} {2}{3} = {4}", (int)(Win32Api.AssocF)assocF, i, (Win32Api.AssocF)assocF, (Win32Api.AssocStr)i, res);
            //        }
            //        catch { }

            //    }

            //}
            #endregion

            if (this.IsExecutable != false) return list;

            var recommendedPrograms = RegHelper.RecommendedPrograms(this.Extension);
            foreach (var program in recommendedPrograms)
            {
                var displayableFile = FindExecutableFile(program);

                if(displayableFile != null && list.Contains(displayableFile) == false)
                    list.Add(displayableFile);
            }

            list.ForEach(Console.WriteLine);

            return list;
        }

        private static DisplayableFile FindExecutableFile(string program)
        {
            DisplayableFile displayableFile = null;

            try
            {
                if (program.EndsWith(".exe"))
                {
                    displayableFile = FindByProgramName(program);
                }
                else
                {
                    displayableFile = FindByProgramId(program);
                }

                if (displayableFile == null)
                    return null;

                string appPath = displayableFile.Command.Remove(displayableFile.Command.LastIndexOf(".exe", StringComparison.InvariantCultureIgnoreCase) + 4);
                displayableFile.IconPath = displayableFile.IconPath ?? appPath;
                displayableFile.DisplayName = displayableFile.DisplayName ?? program;
                //displayableFile.Icon = DisplayableFile.GetImage(displayableFile.IconPath);
                displayableFile.Icon = IconHelper.ExtractIconFromFile(displayableFile.IconPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return displayableFile;
        }

        private static DisplayableFile FindByProgramId(string program)
        {
            // Look assuming program is a programID or CLSID. More at https://msdn.microsoft.com/en-us/library/windows/desktop/bb773471%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
            var displayableFile = new DisplayableFile();
            displayableFile.Command = Win32Api.AssocQueryString(Win32Api.AssocStr.Command, program);
            if (string.IsNullOrEmpty(displayableFile.Command))
                return null;

            displayableFile.DisplayName = Win32Api.AssocQueryString(Win32Api.AssocStr.FriendlyAppName, program);
            displayableFile.IconPath = Win32Api.AssocQueryString(Win32Api.AssocStr.DefaultIcon, program);

            displayableFile.Command = FormatAsCommand(displayableFile.Command);

            return displayableFile;
        }

        private static string FormatAsCommand(string pathOrCommand)
        {
            pathOrCommand = pathOrCommand.Replace("\"", string.Empty);

            if (pathOrCommand.Contains("%1") == false)
                pathOrCommand += " %1";

            return pathOrCommand;
        }

        private static DisplayableFile FindByProgramName(string program)
        {
            DisplayableFile displayableFile = new DisplayableFile();

            displayableFile.Command = Win32Api.AssocQueryString(Win32Api.AssocF.Open_ByExeName, Win32Api.AssocStr.Command,program);
            displayableFile.DisplayName = Win32Api.AssocQueryString(Win32Api.AssocF.Open_ByExeName,Win32Api.AssocStr.FriendlyAppName, program);
            displayableFile.IconPath = Win32Api.AssocQueryString(Win32Api.AssocF.Open_ByExeName,Win32Api.AssocStr.DefaultIcon, program);

            if (string.IsNullOrEmpty(displayableFile.Command))
            {
                var path = RegHelper.GetPathForAppName(program);
                if (path != null)
                {
                    displayableFile.Command = path.EndsWith(".exe") ? path : Path.Combine(path, program);
                }
            }

            if (string.IsNullOrEmpty(displayableFile.Command))
                return null;

            displayableFile.Command = FormatAsCommand(displayableFile.Command);

            return displayableFile;
        }

        private DisplayableFile SelectExecutableFile()
        {
            var res = Win32Api.AssocQueryString(Win32Api.AssocStr.Executable, this.Extension);

            var executablePath = Win32Api.FindExecutable(this.FilePath);
            var executableFile = string.IsNullOrEmpty(executablePath) ? null : new FileInfo(executablePath);

            if (executableFile != null && executableFile.Exists)
                return this.ExecutableFiles.FirstOrDefault(d => d.DisplayName == executableFile.Name) ?? this.ExecutableFiles.First();
            else
                return this.ExecutableFiles.First(d => d == DisplayableFile.Default);
        }

        private FileInfo ResolveLink(FileInfo fileInfo)
        {
            IWshRuntimeLibrary.IWshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(fileInfo.FullName);
            
            return new FileInfo(shortcut.TargetPath);
        }

        private bool IsLink(string extension)
        {
            extension = extension.ToLower();
            return extension == ".lnk"/* || extension == "url"*/;
        }

        private bool CheckIfIsExecutable(string extension)
        {
            extension = extension.ToLower();
            return extension == ".exe" || extension == ".cmd" || extension == ".com";
        }
    }
}
