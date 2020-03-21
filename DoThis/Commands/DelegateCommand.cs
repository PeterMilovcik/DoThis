using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DoThis.Commands
{
    class DelegateCommand : ICommand
    {
        private readonly Func<bool> canExecute;
        private readonly Action action;

        public DelegateCommand(Action action)
            : this(() => true, action)
        {
        }

        public DelegateCommand(Func<bool> canExecute, Action action)
        {
            this.canExecute = canExecute;
            this.action = action;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => canExecute();

        public void Execute(object parameter) => action();
    }
}
