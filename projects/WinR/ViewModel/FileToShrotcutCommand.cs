﻿using System;
using System.Windows.Input;
using WinR.Core;
using WinR.Core.Model;

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
            //Environment.Exit(0);
        }

        public event EventHandler CanExecuteChanged;
    }
}
