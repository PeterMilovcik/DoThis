namespace Beeffective.Views
{
    interface IWindow
    {
        double Left { get; set; }
        double Top { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        void Close();
    }
}