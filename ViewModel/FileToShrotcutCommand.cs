using System;
using System.Windows.Input;
using WinR.Model;

namespace WinR.ViewModel
{
    class FileToShrotcutCommand : ICommand
    {
        private FileToShortCutModel model;
        private ShortCutMaker shortCutMaker;

        public FileToShrotcutCommand(FileToShortCutModel model)
        {
            this.model = model;
        }

        public FileToShrotcutCommand(FileToShortCutModel model, ShortCutMaker shortCutMaker) : this(model)
        {
            this.shortCutMaker = shortCutMaker;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(model.ShortcutName);
        }

        public void Execute(object parameter)
        {
            this.shortCutMaker.Make();
        }

        public event EventHandler CanExecuteChanged;
    }
}
