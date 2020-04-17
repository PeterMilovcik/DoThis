using System;
using System.Windows;
using Beeffective.Extensions;
using Beeffective.ViewModels;

namespace Beeffective.Views
{
    public partial class HoneycombWindow : Window
    {
        private readonly HoneycombViewModel viewModel;

        public HoneycombWindow(HoneycombViewModel viewModel)
        {
            this.viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            viewModel.LoadAsync().FireAndForgetSafeAsync();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            App.Container.Resolve<ICellMenuWindow>().Hide();
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            App.Container.Resolve<ICellMenuWindow>().Show();
        }
    }
}
