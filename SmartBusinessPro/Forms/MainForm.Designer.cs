#nullable disable
namespace SmartBusinessPro.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            btnProbarConexion = new Button();
            btnCargar = new Button();
            lblEstado = new Label();
            dgvProductos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(12, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(330, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "SmartBusinessPro - Inventario";
            // 
            // btnProbarConexion
            // 
            btnProbarConexion.Location = new Point(12, 50);
            btnProbarConexion.Name = "btnProbarConexion";
            btnProbarConexion.Size = new Size(150, 32);
            btnProbarConexion.TabIndex = 1;
            btnProbarConexion.Text = "Probar conexión";
            btnProbarConexion.UseVisualStyleBackColor = true;
            btnProbarConexion.Click += btnProbarConexion_Click;
            // 
            // btnCargar
            // 
            btnCargar.Location = new Point(172, 50);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(150, 32);
            btnCargar.TabIndex = 2;
            btnCargar.Text = "Cargar productos";
            btnCargar.UseVisualStyleBackColor = true;
            btnCargar.Click += btnCargar_Click;
            // 
            // lblEstado
            // 
            lblEstado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblEstado.Location = new Point(12, 92);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(760, 23);
            lblEstado.TabIndex = 3;
            lblEstado.Text = "Presione 'Probar conexión' o 'Cargar productos'.";
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(12, 122);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(760, 380);
            dgvProductos.TabIndex = 4;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 516);
            Controls.Add(dgvProductos);
            Controls.Add(lblEstado);
            Controls.Add(btnCargar);
            Controls.Add(btnProbarConexion);
            Controls.Add(lblTitulo);
            MinimumSize = new Size(640, 400);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SmartBusinessPro";
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Button btnProbarConexion;
        private Button btnCargar;
        private Label lblEstado;
        private DataGridView dgvProductos;
    }
}
