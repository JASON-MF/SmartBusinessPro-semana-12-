using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SmartBusinessPro.Models;

namespace SmartBusinessPro.Data
{
    /// <summary>
    /// Contexto centralizado de acceso a datos. Ningún formulario necesita
    /// conocer la cadena de conexión: basta con crear un SmartBusinessContext.
    /// </summary>
    public class SmartBusinessContext : DbContext
    {
        public const string NombreCadenaConexion = "SmartBusinessConnection";

        private readonly string _cadenaConexion;

        /// <summary>Usa la cadena definida en App.config.</summary>
        public SmartBusinessContext() : this(LeerCadenaDesdeAppConfig())
        {
        }

        /// <summary>Permite indicar una cadena distinta (útil para pruebas de error).</summary>
        public SmartBusinessContext(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Categoria> Categorias => Set<Categoria>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Proveedor de SQL Server de Entity Framework Core
                optionsBuilder.UseSqlServer(_cadenaConexion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(e =>
            {
                e.ToTable("Categorias");
                e.HasKey(c => c.CategoriaId);
                e.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Producto>(e =>
            {
                e.ToTable("Productos");
                e.HasKey(p => p.ProductoId);
                e.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
                e.Property(p => p.Precio).HasColumnType("decimal(18,2)");

                e.HasOne(p => p.Categoria)
                 .WithMany(c => c.Productos)
                 .HasForeignKey(p => p.CategoriaId);
            });
        }

        private static string LeerCadenaDesdeAppConfig()
        {
            var cs = ConfigurationManager.ConnectionStrings[NombreCadenaConexion]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException(
                    $"No se encontró la cadena de conexión '{NombreCadenaConexion}' en App.config.");

            return cs;
        }
    }
}
