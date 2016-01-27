using System;
using System.IO;

namespace WinR.Model
{
    public class FileToShortCutModel
    {

        public string FilePath { get; private set; }
        public bool IsExecutable { get; private set; }
        public string ShortcutName { get; set; }
        public bool RunAsAdministrator { get; set; }

        public FileToShortCutModel(string tentativeFilePath)
        {
            var fileInfo = new FileInfo(tentativeFilePath);
            this.ShortcutName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

            if (this.IsLink(fileInfo.Extension))
            {
                fileInfo = this.ResolveLink(fileInfo);
            }

            this.FilePath = fileInfo.FullName;
            this.IsExecutable = CheckIfIsExecutable(fileInfo.Extension);
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
