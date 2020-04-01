using System;

namespace Beeffective.ViewModels
{
    internal class ItemEventArgs : EventArgs
    {
        public ItemViewModel Item { get; }

        public ItemEventArgs(ItemViewModel item)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }
    }
}