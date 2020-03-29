namespace Beeffective.ViewModels
{
    class CategoryViewModel : ViewModel
    {
        public CategoryViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}