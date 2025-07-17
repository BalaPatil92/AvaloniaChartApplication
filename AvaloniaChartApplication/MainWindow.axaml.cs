using Avalonia.Controls;

namespace AvaloniaChartApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        private void OpenAnotherWindow_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var linechartWindow = new LinecharRealtime();
            linechartWindow.Show();
        }
    }
}