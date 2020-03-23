using System;
using System.Windows;
using System.Windows.Input;
using Beeffective.ViewModels;
using Beeffective.Views;

namespace Beeffective
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

        public void FocusEditableTitleBar() => EditableTitleBar.Focus();

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            viewModel.SetWorkArea(SystemParameters.WorkArea);
            viewModel.IsExpanded = true;
        }

        private void OnEditableTitleBarTextKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                viewModel.SubmitCommand.Execute(null);
            }
        }
    }
}
