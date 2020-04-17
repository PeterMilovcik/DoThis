using Beeffective.Common;

namespace Beeffective.ViewModels
{
    public class ViewModel : Observable
    {
        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (Equals(isBusy, value)) return;
                isBusy = value;
                OnPropertyChanged();
            }
        }
    }
}