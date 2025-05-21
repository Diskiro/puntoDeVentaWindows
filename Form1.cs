using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PuntoDeVenta
{
    public partial class Form1 : Form
    {
        private TextBox txtTelefono = null!;
        private TextBox txtNombre = null!;
        private TextBox txtDireccion = null!;
        private TextBox txtColorCasa = null!;
        private TextBox txtReferencias = null!;
        private Button btnBuscar = null!;
        private Button btnGuardar = null!;
        private Label lblNombre = null!;
        private Label lblDireccion = null!;
        private Label lblColorCasa = null!;
        private Label lblReferencias = null!;
        private Panel panelDatos = null!;
        private readonly string connectionString = "Data Source=clientes.db;Version=3;";

        public Form1()
        {
            InitializeComponent();
            this.Icon = new Icon("Chetegamis_logo.ico");
            InitializeDatabase();
            SetupForm();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            // Colores principales
            Color primaryColor = Color.FromArgb(0, 122, 204);      // Azul principal
            Color secondaryColor = Color.FromArgb(240, 240, 240);  // Gris claro
            Color textColor = Color.FromArgb(64, 64, 64);          // Gris oscuro
            Color accentColor = Color.FromArgb(0, 153, 204);       // Azul acento

            // Configurar el formulario principal
            this.BackColor = secondaryColor;
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Configurar el panel de datos
            panelDatos.BackColor = Color.White;
            panelDatos.Padding = new Padding(20);
            panelDatos.BorderStyle = BorderStyle.FixedSingle;

            // Función para aplicar estilos a los controles
            void StyleControl(Control control)
            {
                if (control is TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.Font = new Font("Segoe UI", 10F);
                    textBox.Height = 30;
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = textColor;
                }
                else if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = primaryColor;
                    button.BackColor = primaryColor;
                    button.ForeColor = Color.White;
                    button.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                    button.Height = 35;
                    button.Cursor = Cursors.Hand;
                    button.FlatAppearance.MouseOverBackColor = accentColor;
                    button.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 102, 184);
                }
                else if (control is Label label)
                {
                    label.Font = new Font("Segoe UI", 10F);
                    label.ForeColor = textColor;
                }
                else if (control is Panel panel)
                {
                    panel.BackColor = Color.White;
                    panel.Padding = new Padding(10);
                }
            }

            // Aplicar estilos a todos los controles
            foreach (Control control in this.Controls)
            {
                StyleControl(control);
                if (control is Panel panel)
                {
                    foreach (Control childControl in panel.Controls)
                    {
                        StyleControl(childControl);
                    }
                }
            }

            // Estilo especial para el botón de guardar
            if (btnGuardar != null)
            {
                btnGuardar.Width = 120;
                btnGuardar.BackColor = Color.FromArgb(46, 204, 113); // Verde
                btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(46, 204, 113);
                btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96);
                btnGuardar.FlatAppearance.MouseDownBackColor = Color.FromArgb(34, 154, 86);
            }

            // Estilo especial para el botón de buscar
            if (btnBuscar != null)
            {
                btnBuscar.Width = 100;
            }

            // Estilo para los campos de texto
            if (txtTelefono != null)
            {
                txtTelefono.Width = 200;
            }
            if (txtNombre != null)
            {
                txtNombre.Width = 400;
            }
            if (txtDireccion != null)
            {
                txtDireccion.Width = 400;
            }
            if (txtColorCasa != null)
            {
                txtColorCasa.Width = 400;
            }
            if (txtReferencias != null)
            {
                txtReferencias.Width = 400;
            }
        }

        private void InitializeDatabase()
        {
            if (!System.IO.File.Exists("clientes.db"))
            {
                SQLiteConnection.CreateFile("clientes.db");
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"CREATE TABLE Clientes (
                        Telefono TEXT PRIMARY KEY,
                        Nombre TEXT NOT NULL,
                        Direccion TEXT NOT NULL,
                        ColorCasa TEXT NOT NULL,
                        Referencias TEXT
                    )";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void SetupForm()
        {
            this.Text = "Chetegamis";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Panel de búsqueda
            Panel searchPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(800, 80),
                Padding = new Padding(20)
            };

            Label lblTelefono = new Label
            {
                Text = "Teléfono:",
                Location = new Point(20, 25),
                AutoSize = true
            };

            txtTelefono = new TextBox
            {
                Location = new Point(100, 22),
                Size = new Size(200, 30),
                MaxLength = 10
            };
            txtTelefono.KeyPress += TxtTelefono_KeyPress;
            txtTelefono.TextChanged += TxtTelefono_TextChanged;

            btnBuscar = new Button
            {
                Text = "Buscar",
                Location = new Point(320, 20),
                Size = new Size(100, 35)
            };
            btnBuscar.Click += BtnBuscar_Click;

            searchPanel.Controls.AddRange(new Control[] { lblTelefono, txtTelefono, btnBuscar });

            // Panel de datos
            panelDatos = new Panel
            {
                Location = new Point(10, 90),
                Size = new Size(760, 400),
                Padding = new Padding(20),
                Visible = false
            };

            // Controles del panel de datos
            lblNombre = new Label { Text = "Nombre:", Location = new Point(20, 35), AutoSize = true };
            txtNombre = new TextBox { Location = new Point(130, 20), Size = new Size(400, 30) };

            lblDireccion = new Label { Text = "Dirección:", Location = new Point(20, 85), AutoSize = true };
            txtDireccion = new TextBox { Location = new Point(130, 70), Size = new Size(400, 30) };

            lblColorCasa = new Label { Text = "Color de Casa:", Location = new Point(20, 135), AutoSize = true };
            txtColorCasa = new TextBox { Location = new Point(130, 120), Size = new Size(400, 30) };

            lblReferencias = new Label { Text = "Referencias:", Location = new Point(20, 185), AutoSize = true };
            txtReferencias = new TextBox { Location = new Point(130, 170), Size = new Size(400, 30) };

            btnGuardar = new Button
            {
                Text = "Guardar",
                Location = new Point(130, 230),
                Size = new Size(120, 35)
            };
            btnGuardar.Click += BtnGuardar_Click;

            panelDatos.Controls.AddRange(new Control[] {
                lblNombre, txtNombre,
                lblDireccion, txtDireccion,
                lblColorCasa, txtColorCasa,
                lblReferencias, txtReferencias,
                btnGuardar
            });

            // Agregar paneles al formulario
            this.Controls.AddRange(new Control[] { searchPanel, panelDatos });
        }

        private void TxtTelefono_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Solo permitir números y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtTelefono_TextChanged(object? sender, EventArgs e)
        {
            // Remover cualquier carácter que no sea número
            txtTelefono.Text = Regex.Replace(txtTelefono.Text, "[^0-9]", "");
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Por favor ingrese un número de teléfono", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtTelefono.Text.Length != 10)
            {
                MessageBox.Show("El número de teléfono debe tener 10 dígitos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Clientes WHERE Telefono = @Telefono";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Cliente encontrado
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtDireccion.Text = reader["Direccion"].ToString();
                            txtColorCasa.Text = reader["ColorCasa"].ToString();
                            txtReferencias.Text = reader["Referencias"].ToString();
                            panelDatos.Visible = true;
                            btnGuardar.Text = "Actualizar";
                        }
                        else
                        {
                            // Cliente no encontrado
                            txtNombre.Clear();
                            txtDireccion.Clear();
                            txtColorCasa.Clear();
                            txtReferencias.Clear();
                            panelDatos.Visible = true;
                            btnGuardar.Text = "Guardar";
                        }
                    }
                }
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Por favor ingrese un número de teléfono", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtTelefono.Text.Length != 10)
            {
                MessageBox.Show("El número de teléfono debe tener 10 dígitos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || 
                string.IsNullOrWhiteSpace(txtDireccion.Text) || 
                string.IsNullOrWhiteSpace(txtColorCasa.Text))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = btnGuardar.Text == "Guardar" 
                    ? "INSERT INTO Clientes (Telefono, Nombre, Direccion, ColorCasa, Referencias) VALUES (@Telefono, @Nombre, @Direccion, @ColorCasa, @Referencias)"
                    : "UPDATE Clientes SET Nombre = @Nombre, Direccion = @Direccion, ColorCasa = @ColorCasa, Referencias = @Referencias WHERE Telefono = @Telefono";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    command.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    command.Parameters.AddWithValue("@ColorCasa", txtColorCasa.Text);
                    command.Parameters.AddWithValue("@Referencias", txtReferencias.Text);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Datos guardados correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
} 