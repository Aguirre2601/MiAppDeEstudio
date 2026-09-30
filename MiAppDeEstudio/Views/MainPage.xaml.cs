using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using MiAppDeEstudio.ViewModels;
using System;

namespace MiAppDeEstudio.Views
{
    /// <summary>
    /// Página principal de la app. Muestra el formulario, la racha y el historial.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();
            this.DataContext = ViewModel;
        }
    }

    /// <summary>
    /// Convierte un número a Visibility: 0 = Visible, >0 = Collapsed.
    /// Se usa para mostrar el mensaje cuando no hay sesiones.
    /// </summary>
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int count)
            {
                return count == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Convierte entre string (yyyy-MM-dd) y DateTime? para el DatePicker.
    /// </summary>
    public class StringToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string str && DateTime.TryParse(str, out DateTime dt))
            {
                return dt;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dt)
            {
                return dt.ToString("yyyy-MM-dd");
            }
            return DateTime.Today.ToString("yyyy-MM-dd");
        }
    }
}
