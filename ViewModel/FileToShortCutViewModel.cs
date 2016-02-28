using System;
using System.Collections.Generic;
using System.Windows.Input;
using WinR.Core;
using WinR.Model;
using WinR.Properties;
//using System.Diagnostics.CodeContracts;

namespace WinR.ViewModel
{
    class FileToShortCutViewModel : ViewModelBase
    {
        private bool canRunAsAdministrator;
        private bool runAsAdministrator;
        private ICommand okCommand;

        public FileToShortCutViewModel()
        {
            this.InitializeAsync();
        }

        private async void InitializeAsync()
        {
            this.Model = new FileToShortCutModel(Environment.GetCommandLineArgs()[1]);
            this.CanRunAsAdministrator = this.Model.IsExecutable;
            this.RunAsAdministrator = this.CanRunAsAdministrator && Settings.Default.RunAsAdministrator;

            this.OkCommand = new FileToShrotcutCommand(this.Model, new ShortCutMaker(this.Model));
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

        public List<DisplayableFile> ExecutableFiles
        {
            get { return this.Model.ExecutableFiles; }
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
