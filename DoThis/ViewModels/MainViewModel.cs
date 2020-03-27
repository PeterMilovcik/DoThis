using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Beeffective.Commands;
using Beeffective.Extensions;
using Beeffective.Models;
using Beeffective.Views;
using MaterialDesignThemes.Wpf;

namespace Beeffective.ViewModels
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
        private ItemViewModel selectedItem;
        private double expandedLeft;
        private double collapsedLeft;

        public MainViewModel(IMainView view)
        {
            this.view = view;
            CustomIcon = nameof(PackIconKind.ArrowLeft);
            ShowHideCommand = new DelegateCommand(obj => ShowHide());
            ShowCommand = new DelegateCommand(obj => Show());
            HideCommand = new DelegateCommand(obj => Hide());
            CloseCommand = new DelegateCommand(obj => Close());
            AddCommand = new DelegateCommand(obj => Add());
            SubmitCommand = new DelegateCommand(obj => Submit());
            Timer = new TimerViewModel();
            Timer.Ticked += OnTimerTicked;
            Timer.TimerFinished += OnTimerFinished;
            ReadOnlyTitleBarVisibility = Visibility.Visible;
            EditableTitleBarVisibility = Visibility.Collapsed;
            Items = new ObservableCollection<ItemViewModel>(LoadItems());
            foreach (var item in Items)
            {
                item.Selected += OnItemSelected;
            }

            SelectedItem = Items.FirstOrDefault(i => i.IsSelected);
        }

        private IEnumerable<ItemViewModel> LoadItems() => 
            App.Database.Items.Select(m => new ItemViewModel(m));

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (Equals(isExpanded, value)) return;
                isExpanded = value;
                CustomIcon = isExpanded ? nameof(PackIconKind.ArrowRight) : nameof(PackIconKind.ArrowLeft);
                //view.Left = isExpanded ? ExpandedLeft : CollapsedLeft;
                if (isExpanded)
                {
                    OnExpanded();
                }
                else
                {
                    OnCollapsed();
                }
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

        public ObservableCollection<ItemViewModel> Items { get; }

        public ItemViewModel SelectedItem
        {
            get => selectedItem;
            set
            {
                if (Equals(selectedItem, value)) return;
                selectedItem = value;
                if (selectedItem != null)
                {
                    OnSelectedItemChanged();
                }
                OnPropertyChanged();
            }
        }

        public ICommand ShowHideCommand { get; }
        
        public ICommand ShowCommand { get; }
        
        public ICommand HideCommand { get; }

        public ICommand CloseCommand { get; }

        public ICommand AddCommand { get; }

        public ICommand SubmitCommand { get; }

        public void SetWorkArea(Rect workArea)
        {
            this.workArea = workArea;
            ExpandedLeft = workArea.Width - view.Width;
            CollapsedLeft = workArea.Width - ExpansionOffset;
            view.Top = workArea.Height / 5;
        }

        public TimerViewModel Timer { get; }

        public event EventHandler Expanded;

        private void OnExpanded() =>
            Expanded?.Invoke(this, EventArgs.Empty);

        public event EventHandler Collapsed;

        private void OnCollapsed() =>
            Collapsed?.Invoke(this, EventArgs.Empty);

        private void OnTimerFinished(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                SelectedItem.Iterations++;
            }
        }

        private void ShowHide() => IsExpanded = !IsExpanded;
        
        private void Show() => IsExpanded = true;
        
        private void Hide() => IsExpanded = false;

        private void Add()
        {
            IsExpanded = true;
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
                var entry = App.Database.Items.Add(
                    new ItemModel
                    {
                        Title = EditableTitleBarText,
                        CreatedAt = DateTime.Now,
                    });
                App.Database.SaveChanges();
                var newModel = entry.Entity;
                var newViewModel = new ItemViewModel(newModel);
                Items.Add(newViewModel);
                TitleBarText = EditableTitleBarText;
                EditableTitleBarText = string.Empty;
                SelectedItem = newViewModel;
            }
        }

        private void OnSelectedItemChanged()
        {
            TitleBarText = SelectedItem.Title;
        }

        private void OnItemSelected(object sender, EventArgs e)
        {
            SelectedItem = sender as ItemViewModel;
            Items.Except(new[] { SelectedItem }).ToList()
                .ForEach(i => i.IsSelected = false);
        }

        private void OnTimerTicked(object sender, EventArgs e) 
            => SelectedItem.IfNotNull(
                () => SelectedItem.AggregatedTime = SelectedItem.AggregatedTime.Add(Timer.Interval));

        private void Close() => view.Close();
    }
}
