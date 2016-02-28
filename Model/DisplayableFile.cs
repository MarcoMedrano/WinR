using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WinR.Model
{
    class DisplayableFile
    {
        internal static readonly DisplayableFile Default = new DisplayableFile { DisplayName = "Windows (default)", Icon = null, Command = "%"};

        public string FullPath { get; set; }
        public string DisplayName { get; set; }
        public ImageSource Icon { get; set; }
        public string Command { get; set; }
        public string IconPath { get; set; }

        public override string ToString()
        {
            return this.DisplayName + ", " + this.Command + ", " + this.IconPath;
        }

        public override bool Equals(object obj)
        {
            var file = obj as DisplayableFile;

            if (file != null)
            {
                return this.Command == file.Command;
            }

            return base.Equals(obj);
        }

        static DisplayableFile FromFullPath(string path)
        {
            var instance = new DisplayableFile
            {
                FullPath = path,
                DisplayName = Path.GetFileName(path),
                Icon = GetImage(path),
            };
            
            return instance;
        }

        public static ImageSource GetImage(string filePath)
        {
            var icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath);

            return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
