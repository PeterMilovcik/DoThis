using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Beeffective.Commands;
using Beeffective.Extensions;
using Beeffective.Models;
using Beeffective.Views;
using MaterialDesignThemes.Wpf;

namespace Beeffective.ViewModels
{
    class ItemViewModel : ViewModel
    {
        private readonly ItemModel model;
        private string icon;
        private string newCategory;
        private bool isDialogOpen;
        private Visibility editableNewSubItemVisibility;
        private string editableNewSubItemText;

        public ItemViewModel(ItemModel model)
        {
            this.model = model;
            Categories = new ObservableCollection<CategoryViewModel>(ParseCategories());
            SubItems = new ObservableCollection<ItemViewModel>();
            UpdateIcon();
            SelectCommand = new DelegateCommand(obj => Select());
            RemoveCommand = new DelegateCommand(obj => Remove());
            ShowAddCategoryDialogCommand = new DelegateCommand(async obj => await ShowAddCategoryDialogAsync());
            AddSubItemCommand = new DelegateCommand(obj => AddSubItem());
            SubmitNewCategoryCommand = new DelegateCommand(async obj => await SubmitNewCategoryAsync());
            SubmitNewSubItemCommand = new DelegateCommand(obj => SubmitNewSubItem());
            EditableNewSubItemVisibility = Visibility.Collapsed;
        }

        public int Id => model.Id;

        public string Title
        {
            get => model.Title;
            set
            {
                if (Equals(model.Title, value)) return;
                model.Title = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CategoryViewModel> Categories { get; }

        public string NewCategory
        {
            get => newCategory;
            set
            {
                if (Equals(newCategory, value)) return;
                newCategory = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemViewModel> SubItems { get; }

        public ICommand ShowAddCategoryDialogCommand { get; }

        private async Task ShowAddCategoryDialogAsync()
        {
            var view = new AddCategoryView { DataContext = this };
            await DialogHost.Show(view, "ItemsDialogHost");
        }

        public ICommand SubmitNewCategoryCommand { get; }

        private async Task SubmitNewCategoryAsync()
        {
            if (string.IsNullOrEmpty(NewCategory)) return;
            NewCategory = NewCategory.Trim();
            if (Categories.Any(c => c.Name == NewCategory)) return;
            var categoryViewModel = new CategoryViewModel(NewCategory);
            Categories.Add(categoryViewModel);
            model.Categories += $" {NewCategory}";
            App.Database.Update(model);
            await App.Database.SaveChangesAsync();
            IsDialogOpen = false;
            NewCategory = string.Empty;
        }

        public bool IsDialogOpen
        {
            get => isDialogOpen;
            set
            {
                if (Equals(isDialogOpen, value)) return;
                isDialogOpen = value;
                OnPropertyChanged();
            }
        }

        private IEnumerable<CategoryViewModel> ParseCategories()
        {
            if (string.IsNullOrWhiteSpace(model.Categories)) return new List<CategoryViewModel>();
            return model.Categories.Split(" ")
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Select(c =>
                {
                    var viewModel = new CategoryViewModel(c);
                    viewModel.Removed += OnCategoryRemoved;
                    return viewModel;
                });
        }

        public int Iterations
        {
            get => model.Iterations;
            set
            {
                if (Equals(model.Iterations, value)) return;
                model.Iterations = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public DateTime CreatedAt
        {
            get => model.CreatedAt;
            set
            {
                if (Equals(model.CreatedAt, value)) return;
                model.CreatedAt = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public string Details
        {
            get => model.Details;
            set
            {
                if (Equals(model.Details, value)) return;
                model.Details = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public DateTime CompletedAt
        {
            get => model.CompletedAt;
            set
            {
                if (Equals(model.CompletedAt, value)) return;
                model.CompletedAt = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public int Urgency
        {
            get => model.Urgency;
            set
            {
                if (Equals(model.Urgency, value)) return;
                model.Urgency = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public int Importance
        {
            get => model.Importance;
            set
            {
                if (Equals(model.Importance, value)) return;
                model.Importance = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public TimeSpan AggregatedTime
        {
            get => model.AggregatedTime;
            set
            {
                if (Equals(model.AggregatedTime, value)) return;
                model.AggregatedTime = value;
                SaveModel();
                OnPropertyChanged();
            }
        }

        public string Icon
        {
            get => icon;
            set
            {
                if (Equals(icon, value)) return;
                icon = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectCommand { get; }

        public event EventHandler Selected;

        private void OnSelected()
            => Selected?.Invoke(this, EventArgs.Empty);

        private void Select() => IsSelected = !IsSelected;

        public bool IsSelected
        {
            get => model.IsSelected;
            set
            {
                if (Equals(model.IsSelected, value)) return;
                model.IsSelected = value;
                SaveModel();
                UpdateIcon();
                model.IsSelected.Call(OnSelected);
                OnPropertyChanged();
            }
        }

        public ICommand RemoveCommand { get; }


        private void Remove()
        {
            App.Database.Remove(model);
            App.Database.SaveChanges();
            OnRemoved();
        }


        public event EventHandler Removed;

        private void OnRemoved() => Removed?.Invoke(this, EventArgs.Empty);

        public ICommand AddSubItemCommand { get; }

        private void AddSubItem() =>
            EditableNewSubItemVisibility = EditableNewSubItemVisibility == Visibility.Collapsed
                ? Visibility.Visible
                : Visibility.Collapsed;

        public Visibility EditableNewSubItemVisibility
        {
            get => editableNewSubItemVisibility;
            set
            {
                if (Equals(editableNewSubItemVisibility, value)) return;
                editableNewSubItemVisibility = value;
                OnPropertyChanged();
            }
        }

        public string EditableNewSubItemText
        {
            get => editableNewSubItemText;
            set
            {
                if (Equals(editableNewSubItemText, value)) return;
                editableNewSubItemText = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitNewSubItemCommand { get; }

        private void SubmitNewSubItem()
        {
            EditableNewSubItemVisibility = Visibility.Collapsed;
            if (!string.IsNullOrEmpty(EditableNewSubItemText))
            {
                var categories = EditableNewSubItemText.ParseCategories();
                categories.ForEach(c => EditableNewSubItemText = EditableNewSubItemText.Replace(c, ""));
                EditableNewSubItemText = EditableNewSubItemText
                    .Replace("#", "")
                    .Replace("@", "");
                EditableNewSubItemText = Regex.Replace(EditableNewSubItemText, @"\s+", " ");

                var entry = App.Database.Items.Add(
                    new ItemModel
                    {
                        Title = EditableNewSubItemText,
                        ParentId = Id,
                        CreatedAt = DateTime.Now,
                        Categories = string.Join(" ", categories)
                    });
                App.Database.SaveChanges();
                var newModel = entry.Entity;
                var item = new ItemViewModel(newModel);
                SubItems.Add(item);
            }
        }

        private void SaveModel()
        {
            App.Database.Update(model);
            App.Database.SaveChanges();
        }

        private void UpdateIcon() => 
            Icon = model.IsSelected ? nameof(PackIconKind.Pause) : nameof(PackIconKind.Play);

        private void OnCategoryRemoved(object sender, EventArgs e)
        {
            if (sender is CategoryViewModel categoryViewModel)
            {
                model.Categories = model.Categories.Replace(categoryViewModel.Name, "");
                App.Database.Update(model);
                App.Database.SaveChanges();
                Categories.Remove(categoryViewModel);
            }
        }
    }
}
