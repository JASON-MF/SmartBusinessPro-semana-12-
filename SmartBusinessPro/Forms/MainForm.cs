using Microsoft.EntityFrameworkCore;
using SmartBusinessPro.Data;
using SmartBusinessPro.Helpers;
using SmartBusinessPro.Models;

namespace SmartBusinessPro.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // ---------- Probar conexión ----------
        private async void btnProbarConexion_Click(object? sender, EventArgs e)
        {
            Ocupado(true, "Probando conexión...");
            try
            {
                using var db = new SmartBusinessContext();

                // Abrir la conexión lanza la excepción real si algo falla
                await db.Database.OpenConnectionAsync();
                var conexion = db.Database.GetDbConnection();
                string servidor = conexion.DataSource;
                string baseDatos = conexion.Database;
                await db.Database.CloseConnectionAsync();

                int total = await db.Productos.CountAsync();

                MostrarEstado($"Conexión exitosa → Servidor: {servidor} | BD: {baseDatos} | Productos: {total}", true);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
            finally
            {
                Ocupado(false);
            }
        }

        // ---------- Cargar productos ----------
        private async void btnCargar_Click(object? sender, EventArgs e)
        {
            Ocupado(true, "Consultando productos...");
            try
            {
                using var db = new SmartBusinessContext();

                List<ProductoVista> datos = await db.Productos
                    .AsNoTracking()
                    .OrderBy(p => p.Nombre)
                    .Select(p => new ProductoVista
                    {
                        Producto = p.Nombre,
                        Precio = p.Precio,
                        Existencia = p.Existencia,
                        Categoria = p.Categoria!.Nombre
                    })
                    .ToListAsync();

                dgvProductos.DataSource = datos;

                if (dgvProductos.Columns["Precio"] != null)
                {
                    dgvProductos.Columns["Precio"]!.DefaultCellStyle.Format = "C2";
                    dgvProductos.Columns["Precio"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                MostrarEstado($"Se recuperaron {datos.Count} producto(s) desde SQL Server.", true);
            }
            catch (Exception ex)
            {
                dgvProductos.DataSource = null;
                MostrarError(ex);
            }
            finally
            {
                Ocupado(false);
            }
        }

        // ---------- Utilidades de interfaz ----------
        private void Ocupado(bool ocupado, string? mensaje = null)
        {
            btnProbarConexion.Enabled = !ocupado;
            btnCargar.Enabled = !ocupado;
            UseWaitCursor = ocupado;

            if (ocupado && mensaje != null)
            {
                lblEstado.ForeColor = Color.DimGray;
                lblEstado.Text = mensaje;
            }
        }

        private void MostrarEstado(string mensaje, bool correcto)
        {
            lblEstado.ForeColor = correcto ? Color.DarkGreen : Color.Firebrick;
            lblEstado.Text = mensaje;
        }

        private void MostrarError(Exception ex)
        {
            string mensaje = ErrorHelper.Traducir(ex);
            MostrarEstado("Error de conexión o consulta (ver mensaje).", false);
            MessageBox.Show(mensaje, "SmartBusinessPro - Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
