using System.Windows.Input;
using Beeffective.Commands;
using Beeffective.Views;

namespace Beeffective.ViewModels
{
    internal class ClosableViewModel : ViewModel
    {
        protected ClosableViewModel(IMainView mainView)
        {
            View = mainView;
            CloseCommand = new DelegateCommand(obj => Close());
        }

        protected IMainView View { get; }
        public ICommand CloseCommand { get; }
        protected void Close() => View.Close();
    }
}