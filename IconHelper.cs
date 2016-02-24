using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WinR
{
    /// <summary>
    /// Structure that encapsulates basic information of icon embedded in a file.
    /// </summary>
    public struct EmbeddedIconInfo
    {
        public string FileName;
        public int IconIndex;
    }

    static class IconHelper
    {

        /// <summary>
        /// Extract the icon from file.
        /// <param name="fileAndParam">The params string, such as ex: 
        ///    "C:\\Program Files\\NetMeeting\\conf.exe,1".</param>
        /// <returns>This method always returns the large size of the icon 
        ///    (may be 32x32 px).</returns>
        internal static ImageSource ExtractIconFromFile(string fileAndParam)
        {
            try
            {
                EmbeddedIconInfo embeddedIcon = GetEmbeddedIconInfo(fileAndParam);

                //Gets the handle of the icon.
                IntPtr iconPtr = Win32Api.ExtractIcon(0, embeddedIcon.FileName,embeddedIcon.IconIndex);

                //Gets the real icon.
                return Imaging.CreateBitmapSourceFromHIcon(iconPtr, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Parses the parameters string to the structure of EmbeddedIconInfo.
        /// </summary>
        /// <param name="fileAndParam">The params string, such as ex: 
        ///    "C:\\Program Files\\NetMeeting\\conf.exe,1".</param>
        private static EmbeddedIconInfo GetEmbeddedIconInfo(string fileAndParam)
        {
            EmbeddedIconInfo embeddedIcon = new EmbeddedIconInfo();

            if (String.IsNullOrEmpty(fileAndParam))
                return embeddedIcon;

            //Use to store the file contains icon.
            string fileName = String.Empty;

            //The index of the icon in the file.
            int iconIndex = 0;
            string iconIndexString = String.Empty;

            int commaIndex = fileAndParam.IndexOf(",");
            //if fileAndParam is some thing likes this: 
            //"C:\\Program Files\\NetMeeting\\conf.exe,1".
            if (commaIndex > 0)
            {
                fileName = fileAndParam.Substring(0, commaIndex);
                iconIndexString = fileAndParam.Substring(commaIndex + 1);
            }
            else
                fileName = fileAndParam;

            if (!String.IsNullOrEmpty(iconIndexString))
            {
                //Get the index of icon.
                iconIndex = int.Parse(iconIndexString);
                if (iconIndex < 0)
                    iconIndex = 0;  //To avoid the invalid index.
            }

            embeddedIcon.FileName = fileName;
            embeddedIcon.IconIndex = iconIndex;

            return embeddedIcon;
        }
    }


}
