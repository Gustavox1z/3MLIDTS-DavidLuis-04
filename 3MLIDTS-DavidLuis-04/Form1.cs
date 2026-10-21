using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _3MLIDTS_DavidLuis_04
{
    public partial class Form1 : Form
    {
        string conexion = "Server=127.0.0.1;Port=3306;Database=formulario;Uid=root;Pwd=Davidpro#4659;";

        public Form1()
        {
            InitializeComponent();
            txtNombre.TextChanged += ValidarNombre;
            txtApellido.TextChanged += ValidarApellido;
            txtTelefono.Leave += ValidarTelefono;
            txtEdad.TextChanged += ValidarEdad;
            txtEstatura.TextChanged += ValidarEstatura;
        }

        private void InsertarRegistro(string nombre, string apellido, string telefono, int edad, decimal estatura, string genero)
        {
            using (MySqlConnection conection = new MySqlConnection(conexion))
            {
                conection.Open();

                string insertQuery = "INSERT INTO registros (nombre, apellidos, telefono, estatura, edad, genero) " +
                    "VALUES (@nombre, @apellidos, @telefono, @estatura, @edad, @genero)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, conection))
                {
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@apellidos", apellido);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@estatura", estatura);
                    command.Parameters.AddWithValue("@edad", edad);
                    command.Parameters.AddWithValue("@genero", genero);

                    command.ExecuteNonQuery();
                }
                conection.Close();
            }
        }

        private void ValidarNombre(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;
            if (!EsTextoValido(textboxLocal.Text))
            {
                MessageBox.Show("Escriba un nombre valido", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textboxLocal.Clear();
            }
        }
        private void ValidarApellido(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;
            if (!EsTextoValido(textboxLocal.Text))
            {
                MessageBox.Show("Escriba un Apellido valido", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textboxLocal.Clear();
            }
        }
        private void ValidarTelefono(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;
            if (!EsTelefonoValido(textboxLocal.Text))
            {
                MessageBox.Show("Escriba un telefono valido", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textboxLocal.BackColor = System.Drawing.Color.IndianRed;
                //textboxLocal.Clear();
                //Hola mundo
            }
            else
            {
                textboxLocal.BackColor = System.Drawing.Color.LightGreen;
            }
        }
        private void ValidarEdad(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;
            if (!EsEnteroValido(textboxLocal.Text))
            {
                {
                    MessageBox.Show("Escriba una edad valida", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textboxLocal.Clear();
                }
            }
        }
        private void ValidarEstatura(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;
            if (!EsDecimalValido(textboxLocal.Text))
            {
                {
                    MessageBox.Show("Escriba una estatura valida", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textboxLocal.Clear();
                }
            }
        }

        private bool EsTextoValido(string Valor)
        {
            return Regex.IsMatch(Valor, @"^[a-zA-Z\s]+$");
        }
        private bool EsEnteroValido(string Valor)
        {
            int resultado;
            return int.TryParse(Valor, out resultado);
        }
        private bool EsDecimalValido(string Valor)
        {
            decimal resultado;
            return decimal.TryParse(Valor, out resultado);
        }
        private bool EsTelefonoValido(string Valor)
        {
            long resultado;
            return long.TryParse(Valor, out resultado) && Valor.Length == 10;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombres = txtNombre.Text;
            string apellidos = txtApellido.Text;
            string telefonos = txtTelefono.Text;
            string edad = txtEdad.Text;
            string estatura = txtEstatura.Text;

            string genero = "";

            if (rbMasculino.Checked)
            {
                genero = "Masculino";
            }
            else if (rbFemenino.Checked)
            {
                genero = "Femenino";
            }
            else if (rbOtro.Checked)
            {
                genero = "Otro";
            }

            if(EsEnteroValido(edad) && EsDecimalValido(estatura) && EsTelefonoValido(telefonos) && EsTextoValido(nombres) && EsTextoValido(apellidos))
            {
                string mensaje = $" Nombres: {nombres}\n Apellidos: {apellidos}\n Telefono: {telefonos}\n Edad: {edad}\n Estatura: {estatura}\n Genero: {genero}";

                ///Ruta de almacenamiento de archivo TXT
                string rutaFile = "C:\\Users\\Gustavo\\Downloads\\datos.txt";
                ///Valor de verificacion de archivo existen
                bool ArchivoExiste = File.Exists(rutaFile);
                ///Instanciacion de la clase/objeto streamwriter para escribir archivo TXT

                if (ArchivoExiste == false)
                {
                    File.WriteAllText(rutaFile, mensaje);
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(rutaFile, true))
                    {
                        if (ArchivoExiste)
                        {
                            writer.WriteLine();
                            InsertarRegistro(nombres, apellidos, telefonos, int.Parse(edad), decimal.Parse(estatura), genero);
                            MessageBox.Show("Datos ingresados correctamente");
                        }
                        else
                        {
                            writer .WriteLine();
                            InsertarRegistro(nombres, apellidos, telefonos, int.Parse(edad), decimal.Parse(estatura), genero);
                            MessageBox.Show("Datos ingresados correctamente");
                        }
                    }
                }

                MessageBox.Show("Datos guardados con exito: \n\n" + mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, ingrese datos validos en los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEdad.Clear();
            txtEstatura.Clear();
            rbMasculino.Checked = false;
            rbFemenino.Checked = false;
            rbOtro.Checked = false;
        }
    }
}