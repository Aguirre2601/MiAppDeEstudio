using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MiAppDeEstudio.Views;

namespace MiAppDeEstudio
{
    /// <summary>
    /// Ventana principal. Contiene un Frame que navega a la página principal.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public static MainWindow? Main { get; private set; }

        public MainWindow()
        {
            this.InitializeComponent();
            Main = this;
            ContentFrame.Navigate(typeof(MainPage));
        }
    }
}
