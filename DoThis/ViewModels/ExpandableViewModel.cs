using System;
using System.Windows;
using System.Windows.Input;
using Beeffective.Commands;
using Beeffective.Extensions;
using Beeffective.Views;
using MaterialDesignThemes.Wpf;

namespace Beeffective.ViewModels
{
    class ExpandableViewModel : ClosableViewModel
    {
        private string customIcon;
        private bool isExpanded;
        private double expandedLeft;
        private double collapsedLeft;

        protected ExpandableViewModel(IMainView mainView)
            : base(mainView)
        {
            CustomIcon = nameof(PackIconKind.ArrowLeft);
            ShowHideCommand = new DelegateCommand(obj => ShowOrHide());
            ShowCommand = new DelegateCommand(obj => Show());
            HideCommand = new DelegateCommand(obj => Hide());
        }

        private const int ExpansionOffset = 40;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (Equals(isExpanded, value)) return;
                isExpanded = value;
                CustomIcon = isExpanded ? nameof(PackIconKind.ArrowRight) : nameof(PackIconKind.ArrowLeft);
                isExpanded.IfTrue(RaiseExpanded).IfFalse(RaiseCollapsed);
                OnPropertyChanged();
            }
        }

        public double ExpandedLeft
        {
            get => expandedLeft;
            set
            {
                if (Equals(expandedLeft, value)) return;
                expandedLeft = value;
            }
        }

        public double CollapsedLeft
        {
            get => collapsedLeft;
            set
            {
                if (Equals(collapsedLeft, value)) return;
                collapsedLeft = value;
                OnPropertyChanged();
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

        public ICommand ShowHideCommand { get; }
        public ICommand ShowCommand { get; }
        public ICommand HideCommand { get; }

        public void SetWorkArea(Rect workArea)
        {
            ExpandedLeft = workArea.Width - View.Width;
            CollapsedLeft = workArea.Width - ExpansionOffset;
            View.Top = workArea.Height / 5;
        }

        public event EventHandler Expanded;
        private void RaiseExpanded() => Expanded?.Invoke(this, EventArgs.Empty);
        public event EventHandler Collapsed;
        private void RaiseCollapsed() => Collapsed?.Invoke(this, EventArgs.Empty);
        protected void ShowOrHide() => IsExpanded = !IsExpanded;
        protected void Show() => IsExpanded = true;
        protected void Hide() => IsExpanded = false;
    }
}