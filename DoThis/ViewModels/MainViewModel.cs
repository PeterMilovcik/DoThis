using DoThis.Commands;
using DoThis.Views;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Input;

namespace DoThis.ViewModels
{
    class MainViewModel : ViewModel
    {
        public MainViewModel(IMainView view)
        {
            this.view = view;
            CustomIcon = nameof(PackIconKind.ArrowLeft);
            CustomCommand = new DelegateCommand(ExpandOrCollapse);
            CloseCommand = new DelegateCommand(Close);
        }

        private string customIcon;
        private readonly IMainView view;
        private bool isExpanded;
        private Rect workArea;

        public string CustomIcon
        {
            get => customIcon;
            set
            {
                if (Equals(customIcon, value)) return;
                customIcon = value;
                OnPropertyChanged();
            }
        }

        public ICommand CustomCommand { get; }

        public ICommand CloseCommand { get; }

        public void AdjustPositionTo(Rect workArea)
        {
            this.workArea = workArea;
            view.Left = workArea.Width - 30;
            view.Top = workArea.Height / 4;
        }

        private void ExpandOrCollapse()
        {
            isExpanded = !isExpanded;
            CustomIcon = isExpanded ? nameof(PackIconKind.ArrowRight) : nameof(PackIconKind.ArrowLeft);
            view.Left = isExpanded ? workArea.Width - 30 - view.Width : workArea.Width - 30;
        }

        private void Close() => view.Close();
    }
}
