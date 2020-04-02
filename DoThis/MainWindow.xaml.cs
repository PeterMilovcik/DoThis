using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
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
        private readonly TimeSpan animationTimeSpan = TimeSpan.FromMilliseconds(250);

        public MainWindow()
        {
            viewModel = new MainViewModel(this);
            viewModel.Expanded += OnExpanded;
            viewModel.Collapsed += OnCollapsed;
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OnExpanded(object sender, EventArgs e)
        {
            var animation = new DoubleAnimation(viewModel.CollapsedLeft, viewModel.ExpandedLeft, new Duration(animationTimeSpan));
            BeginAnimation(LeftProperty, animation);
        }

        private void OnCollapsed(object sender, EventArgs e)
        {
            var animation = new DoubleAnimation(viewModel.ExpandedLeft, viewModel.CollapsedLeft, new Duration(animationTimeSpan));
            BeginAnimation(LeftProperty, animation);
        }

        public void FocusEditableTitleBar() => EditableTitleBar.Focus();

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
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

        private void OnEditableNewSubItemTextKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (e.Key == Key.Enter)
                {
                    var itemViewModel = textBox.DataContext as ItemViewModel;
                    itemViewModel.SubmitNewSubItemCommand.Execute(null);
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new HoneycompWindow();
            window.Show();
        }
    }
}
