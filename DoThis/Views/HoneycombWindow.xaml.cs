using System;
using System.Windows;
using Beeffective.ViewModels;

namespace Beeffective.Views
{
    /// <summary>
    /// Interaction logic for HoneycombWindow.xaml
    /// </summary>
    public partial class HoneycombWindow : Window
    {
        private readonly HoneycombViewModel viewModel;

        public HoneycombWindow(HoneycombViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            DataContext = viewModel;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            viewModel.HideTopmostWindow();
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            viewModel.ShowTopmostWindow();
        }
    }
}
