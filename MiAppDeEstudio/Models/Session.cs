using System;

namespace MiAppDeEstudio.Models
{
    /// <summary>
    /// Representa una sesión de estudio guardada por el usuario.
    /// </summary>
    public class Session
    {
        public int Id { get; set; }

        /// <summary>
        /// Fecha local de la sesión (solo fecha, sin hora).
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Tema estudiado. Obligatorio.
        /// </summary>
        public string Topic { get; set; } = string.Empty;

        /// <summary>
        /// Minutos estudiados. Debe ser mayor a 0.
        /// </summary>
        public int Minutes { get; set; }
    }
}
