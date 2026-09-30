using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiAppDeEstudio.Models;
using System;
using System.IO;

namespace MiAppDeEstudio.Data
{
    /// <summary>
    /// Base de datos de la app. Guarda las sesiones en MySQL.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Cargar la cadena de conexión desde appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().HasKey(s => s.Id);
            modelBuilder.Entity<Session>().Property(s => s.Topic).IsRequired();
        }
    }
}
