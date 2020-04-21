using Beeffective.Common;
using Beeffective.Models;

namespace Beeffective.ViewModels
{
    public class ViewModel : Observable
    {
        private bool isBusy;

        public ViewModel()
        {
            Honeycomb = App.Container.Resolve<HoneycombModel>();
        }

        public HoneycombModel Honeycomb { get; }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
    }
}