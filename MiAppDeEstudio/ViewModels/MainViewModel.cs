using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiAppDeEstudio.Data;
using MiAppDeEstudio.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MiAppDeEstudio.ViewModels
{
    /// <summary>
    /// ViewModel de la página principal. Maneja la lógica de la app.
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private readonly AppDbContext _db;

        // --- Campos del formulario ---
        [ObservableProperty]
        private string _selectedDateString = DateTime.Today.ToString("yyyy-MM-dd");

        [ObservableProperty]
        private string _topic = string.Empty;

        [ObservableProperty]
        private string _minutesText = string.Empty;

        // --- Datos para mostrar ---
        [ObservableProperty]
        private ObservableCollection<Session> _sessions = new();

        [ObservableProperty]
        private int _currentStreak;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private bool _hasError;

        public MainViewModel()
        {
            _db = new AppDbContext();
            _db.Database.EnsureCreated();
            _ = LoadDataAsync();
        }

        /// <summary>
        /// Carga las sesiones y calcula la racha.
        /// </summary>
        private async Task LoadDataAsync()
        {
            await Task.Run(() =>
            {
                var sessions = _db.Sessions
                    .OrderByDescending(s => s.Date)
                    .ThenByDescending(s => s.Id)
                    .ToList();

                var streak = CalculateStreak(sessions);

                // Actualizamos en el hilo de UI
                App.MainWindow?.DispatcherQueue.TryEnqueue(() =>
                {
                    Sessions = new ObservableCollection<Session>(sessions);
                    CurrentStreak = streak;
                });
            });
        }

        /// <summary>
        /// Calcula la racha actual de días consecutivos estudiando.
        /// La racha termina hoy o ayer (si hoy aún no se ha estudiado).
        /// </summary>
        private int CalculateStreak(System.Collections.Generic.List<Session> sessions)
        {
            if (sessions.Count == 0) return 0;

            // Obtenemos las fechas únicas con al menos una sesión
            var studyDates = sessions
                .Select(s => s.Date.Date)
                .Distinct()
                .OrderByDescending(d => d)
                .ToList();

            DateTime today = DateTime.Today;

            // Si hoy no hay sesión, empezamos desde ayer (la racha sigue viva)
            DateTime currentDate = studyDates[0];
            if (currentDate < today.AddDays(-1))
            {
                // El último estudio fue hace más de un día: racha rota
                return 0;
            }

            int streak = 0;
            foreach (var date in studyDates)
            {
                if (date == currentDate)
                {
                    streak++;
                    currentDate = currentDate.AddDays(-1);
                }
                else
                {
                    break;
                }
            }

            return streak;
        }

        /// <summary>
        /// Guarda una nueva sesión en la base de datos.
        /// </summary>
        [RelayCommand]
        private async Task SaveSessionAsync()
        {
            HasError = false;
            ErrorMessage = string.Empty;

            // Validaciones
            if (string.IsNullOrWhiteSpace(Topic))
            {
                ErrorMessage = "¡Escribe el tema que estudiaste! ✏️";
                HasError = true;
                return;
            }

            if (!int.TryParse(MinutesText, out int minutes) || minutes <= 0)
            {
                ErrorMessage = "¡Los minutos deben ser un número mayor a 0! ⏱️";
                HasError = true;
                return;
            }

            if (!DateTime.TryParse(SelectedDateString, out DateTime sessionDate))
            {
                sessionDate = DateTime.Today;
            }

            var session = new Session
            {
                Date = sessionDate.Date,
                Topic = Topic.Trim(),
                Minutes = minutes
            };

            await Task.Run(() =>
            {
                _db.Sessions.Add(session);
                _db.SaveChanges();
            });

            // Limpiamos el formulario
            Topic = string.Empty;
            MinutesText = string.Empty;
            SelectedDateString = DateTime.Today.ToString("yyyy-MM-dd");

            // Recargamos los datos
            await LoadDataAsync();
        }
    }
}
