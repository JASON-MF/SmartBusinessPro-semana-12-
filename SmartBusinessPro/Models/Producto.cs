namespace SmartBusinessPro.Models
{
    /// <summary>
    /// Entidad que representa la tabla Productos.
    /// </summary>
    public class Producto
    {
        public int ProductoId { get; set; }           // PK
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Existencia { get; set; }

        public int CategoriaId { get; set; }          // FK -> Categorias
        public Categoria? Categoria { get; set; }     // Navegación
    }
}
