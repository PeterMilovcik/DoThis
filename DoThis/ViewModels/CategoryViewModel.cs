using System;
using System.Windows.Input;
using Beeffective.Commands;

namespace Beeffective.ViewModels
{
    class CategoryViewModel : ViewModel
    {
        public CategoryViewModel(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            Name = name;
            RemoveCategoryCommand = new DelegateCommand(obj => RemoveCategory());
        }

        public string Name { get; }

        public ICommand RemoveCategoryCommand { get; }

        private void RemoveCategory() => RaiseRemoved();

        public event EventHandler Removed;

        private void RaiseRemoved()
            => Removed?.Invoke(this, EventArgs.Empty);
    }
}