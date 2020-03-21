using DoThis.ViewModels;
using DoThis.Views;
using System;
using System.Windows;

namespace DoThis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainView
    {
        private readonly MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel(this);
            DataContext = viewModel;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            viewModel.AdjustPositionTo(SystemParameters.WorkArea);
        }
    }
}
