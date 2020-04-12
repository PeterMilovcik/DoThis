namespace Beeffective.Views
{
    public interface IWindow
    {
        object DataContext { get; set; }
        double Left { get; set; }
        double Top { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        void Show();
        void Hide();
        void Close();
    }
}