using DoThis.Commands;
using DoThis.Models;
using DoThis.Views;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DoThis.ViewModels
{
    class MainViewModel : ViewModel
    {
        private const int ExpansionOffset = 40;
        private string customIcon;
        private readonly IMainView view;
        private bool isExpanded;
        private Rect workArea;
        private Visibility readOnlyTitleBarVisibility;
        private Visibility editableTitleBarVisibility;
        private string titleBarText;
        private string editableTitleBarText;

        public MainViewModel(IMainView view)
        {
            this.view = view;
            CustomIcon = nameof(PackIconKind.ArrowLeft);
            CustomCommand = new DelegateCommand(obj => ExpandOrCollapse());
            CloseCommand = new DelegateCommand(obj => Close());
            AddCommand = new DelegateCommand(obj => Add());
            SubmitCommand = new DelegateCommand(obj => Submit());
            Timer = new TimerViewModel();
            ReadOnlyTitleBarVisibility = Visibility.Visible;
            EditableTitleBarVisibility = Visibility.Collapsed;
            Items = new ObservableCollection<ItemModel>();
        }

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (Equals(isExpanded, value)) return;
                isExpanded = value;
                CustomIcon = isExpanded ? nameof(PackIconKind.ArrowRight) : nameof(PackIconKind.ArrowLeft);
                view.Left = isExpanded ? workArea.Width - view.Width : workArea.Width - ExpansionOffset;
                view.Top = workArea.Height / 5;
            }
        }

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

        public Visibility ReadOnlyTitleBarVisibility
        {
            get => readOnlyTitleBarVisibility;
            set
            {
                if (Equals(readOnlyTitleBarVisibility, value)) return;
                readOnlyTitleBarVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility EditableTitleBarVisibility
        {
            get => editableTitleBarVisibility;
            set
            {
                if (Equals(editableTitleBarVisibility, value)) return;
                editableTitleBarVisibility = value;
                OnPropertyChanged();
            }
        }

        public string TitleBarText
        {
            get => titleBarText;
            set
            {
                if (Equals(titleBarText, value)) return;
                titleBarText = value;
                OnPropertyChanged();
            }
        }

        public string EditableTitleBarText
        {
            get => editableTitleBarText;
            set
            {
                if (Equals(editableTitleBarText, value)) return;
                editableTitleBarText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemModel> Items { get; }

        public ICommand CustomCommand { get; }

        public ICommand CloseCommand { get; }

        public ICommand AddCommand { get; }

        public ICommand SubmitCommand { get; }

        public void SetWorkArea(Rect workArea) => this.workArea = workArea;

        public TimerViewModel Timer { get; }

        private void ExpandOrCollapse() => IsExpanded = !IsExpanded;

        private void Add()
        {
            EditableTitleBarVisibility = Visibility.Visible;
            ReadOnlyTitleBarVisibility = Visibility.Collapsed;
            view.FocusEditableTitleBar();
        }

        private void Submit()
        {
            EditableTitleBarVisibility = Visibility.Collapsed;
            ReadOnlyTitleBarVisibility = Visibility.Visible;
            if (!string.IsNullOrEmpty(EditableTitleBarText))
            {
                TitleBarText = EditableTitleBarText;
                Items.Add(new ItemModel { Title = EditableTitleBarText });
                EditableTitleBarText = string.Empty;
            }
        }

        private void Close() => view.Close();
    }
}
