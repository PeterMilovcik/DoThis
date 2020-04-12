using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Beeffective.Data;
using Beeffective.Models;
using Beeffective.ViewModels;

namespace Beeffective.Views
{
    /// <summary>
    /// Interaction logic for HoneycombWindow.xaml
    /// </summary>
    public partial class HoneycombWindow : Window
    {
        public HoneycombWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (HoneycombView.DataContext is HoneycombViewModel viewModel)
            {
                viewModel.HideTopmostWindow();
            }
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            if (HoneycombView.DataContext is HoneycombViewModel viewModel)
            {
                viewModel.ShowTopmostWindow();
            }
        }
    }
}
