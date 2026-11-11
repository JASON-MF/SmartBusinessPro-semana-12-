namespace SmartBusinessPro.Models
{
    /// <summary>
    /// Entidad que representa la tabla Categorias.
    /// </summary>
    public class Categoria
    {
        public int CategoriaId { get; set; }          // PK
        public string Nombre { get; set; } = string.Empty;

        // Navegación: una categoría tiene muchos productos
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
