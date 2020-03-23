namespace Beeffective.Views
{
    interface IMainView
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        void Close();
        void FocusEditableTitleBar();
    }
}
