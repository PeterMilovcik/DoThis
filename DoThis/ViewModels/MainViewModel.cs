using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using Beeffective.Commands;
using Beeffective.Extensions;
using Beeffective.Models;
using Beeffective.Views;

namespace Beeffective.ViewModels
{
    class MainViewModel : ExpandableViewModel
    {
        private Visibility readOnlyTitleBarVisibility;
        private Visibility editableTitleBarVisibility;
        private string titleBarText;
        private string editableTitleBarText;
        private ItemViewModel selectedItem;
        private readonly List<ItemViewModel> allItems;

        public MainViewModel(IMainView view)
            : base(view)
        {
            AddCommand = new DelegateCommand(obj => Add());
            SubmitCommand = new DelegateCommand(obj => Submit());
            Timer = new TimerViewModel();
            Timer.Ticked += OnTimerTicked;
            Timer.TimerFinished += OnTimerFinished;
            ReadOnlyTitleBarVisibility = Visibility.Visible;
            EditableTitleBarVisibility = Visibility.Collapsed;
            allItems = new List<ItemViewModel>();
            Items = new ObservableCollection<ItemViewModel>(LoadItems());
            SelectedItem = Items.FirstOrDefault(i => i.IsSelected);
        }

        private IEnumerable<ItemViewModel> LoadItems()
        {
            var result = new List<ItemViewModel>();
            var itemModels = App.Database.Items;
            foreach (var itemModel in itemModels)
            {
                var itemViewModel = new ItemViewModel(itemModel);
                Subscribe(itemViewModel);
                allItems.Add(itemViewModel);
            }

            foreach (var itemViewModel in allItems)
            {
                if (itemViewModel.ParentId == 0)
                {
                    result.Add(itemViewModel);
                }
                else
                {
                    var parent = allItems.First(i => i.Id == itemViewModel.ParentId);
                    parent.SubItems.Add(itemViewModel);
                }
            }

            return result;
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
                OnSelectedItemChanged();
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }

        public ICommand SubmitCommand { get; }

        public TimerViewModel Timer { get; }

        private void OnTimerFinished(object sender, EventArgs e) => SelectedItem.IfNotNull(item => item.Iterations++);

        private void Add()
        {
            IsExpanded = true;
            EditableTitleBarVisibility = Visibility.Visible;
            ReadOnlyTitleBarVisibility = Visibility.Collapsed;
            View.FocusEditableTitleBar();
        }

        private void Submit()
        {
            EditableTitleBarVisibility = Visibility.Collapsed;
            ReadOnlyTitleBarVisibility = Visibility.Visible;
            if (!string.IsNullOrEmpty(EditableTitleBarText))
            {
                var categories = EditableTitleBarText.ParseCategories();
                categories.ForEach(c => EditableTitleBarText = EditableTitleBarText.Replace(c, ""));
                EditableTitleBarText = EditableTitleBarText
                    .Replace("#", "")
                    .Replace("@","");
                EditableTitleBarText = Regex.Replace(EditableTitleBarText, @"\s+", " ");

                var entry = App.Database.Items.Add(
                    new ItemModel
                    {
                        Title = EditableTitleBarText,
                        CreatedAt = DateTime.Now,
                        Categories = string.Join(" ", categories)
                    });
                App.Database.SaveChanges();
                var newModel = entry.Entity;
                var newViewModel = new ItemViewModel(newModel);
                Subscribe(newViewModel);
                Items.Add(newViewModel);
                TitleBarText = EditableTitleBarText;
                EditableTitleBarText = string.Empty;
                SelectedItem = newViewModel;
            }
        }

        private void Subscribe(ItemViewModel newViewModel)
        {
            newViewModel.Selected += OnItemSelected;
            newViewModel.Removed += OnItemRemoved;
            newViewModel.SubItemAdded += OnSubItemAdded;
        }

        private void OnSelectedItemChanged() => TitleBarText = SelectedItem?.Title;

        private void OnItemSelected(object sender, EventArgs e)
        {
            SelectedItem = sender as ItemViewModel;
            Items.Except(new[] { SelectedItem }).ToList()
                .ForEach(i => i.IsSelected = false);
        }

        private void OnItemRemoved(object sender, EventArgs e)
        {
            if (sender is ItemViewModel viewModel)
            {
                if (SelectedItem == viewModel)
                {
                    SelectedItem = null;
                }

                Remove(Items, viewModel);
            }
        }

        private void Remove(ObservableCollection<ItemViewModel> items, ItemViewModel viewModel)
        {
            if (items.Contains(viewModel))
            {
                items.Remove(viewModel);
            }
            else
            {
                foreach (var itemViewModel in items)
                {
                    Remove(itemViewModel.SubItems, viewModel);
                }
            }
        }

        private void OnSubItemAdded(object sender, ItemEventArgs e) => Subscribe(e.Item);

        private void OnTimerTicked(object sender, EventArgs e)
        {
            if (SelectedItem == null) return;
            SelectedItem.AggregatedTime = SelectedItem.AggregatedTime.Add(Timer.Interval);
        }
    }
}
