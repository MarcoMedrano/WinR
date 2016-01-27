using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WinR.Annotations;
using WinR.Model;
using WinR.Properties;
//using System.Diagnostics.CodeContracts;

namespace WinR.ViewModel
{
    public class FileToShortCutViewModel : ViewModelBase
    {
        private string shortcutName;
        private bool canRunAsAdministrator;
        private bool runAsAdministrator;
        private ICommand okCommand;

        public FileToShortCutViewModel()
        {
           string userPaadata = this.GetUserAppDataPath();
            var a = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MyCompanyName\\MyApplicationName";
            
            this.Model = new FileToShortCutModel(Environment.GetCommandLineArgs()[1]);
            this.CanRunAsAdministrator = this.Model.IsExecutable;
            this.RunAsAdministrator = this.CanRunAsAdministrator && Settings.Default.RunAsAdministrator;

            this.OkCommand = new FileToShrotcutCommand(this.Model, new ShortCutMaker(this.Model));
        }

        public string GetUserAppDataPath()
        {
            string path = string.Empty;
            Assembly assm;
            Type at;
            object[] r;

            // Get the .EXE assembly
            assm = Assembly.GetEntryAssembly();
            // Get a 'Type' of the AssemblyCompanyAttribute
            at = typeof(AssemblyCompanyAttribute);
            // Get a collection of custom attributes from the .EXE assembly
            r = assm.GetCustomAttributes(at, false);
            // Get the Company Attribute
            AssemblyCompanyAttribute ct =
                          ((AssemblyCompanyAttribute)(r[0]));
            // Build the User App Data Path
            path = Environment.GetFolderPath(
                        Environment.SpecialFolder.ApplicationData);
            path += @"\" + ct.Company;
            path += @"\" + assm.GetName().Version.ToString();

            return path;
        }

        public FileToShortCutModel Model { get; set; }

        public string ShortcutName
        {
            get { return this.Model.ShortcutName; }
            set
            {
                this.Model.ShortcutName = value; 
                this.RaisePropertyChanged(nameof(this.OkCommand));
            }
        }

        public bool CanRunAsAdministrator
        {
            get { return canRunAsAdministrator; }
            set
            {
                canRunAsAdministrator = value; 

                this.RaisePropertyChanged(nameof(this.CanRunAsAdministrator));
                this.RaisePropertyChanged(nameof(this.RunAsAdministrator));
            }
        }

        public bool RunAsAdministrator
        {
            get { return this.Model.RunAsAdministrator; }
            set
            {
                this.Model.RunAsAdministrator = value;
                Settings.Default.RunAsAdministrator = value;
                Settings.Default.Save();
            }
        }

        public ICommand OkCommand
        {
            get { return okCommand; }
            set
            {
                okCommand = value; 
                this.RaisePropertyChanged(nameof(this.OkCommand));
            }
        }
    }
}
