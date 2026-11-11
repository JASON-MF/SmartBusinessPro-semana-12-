namespace SmartBusinessPro.Models
{
    /// <summary>
    /// Modelo plano que se muestra en el DataGridView.
    /// </summary>
    public class ProductoVista
    {
        public string Producto { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}
