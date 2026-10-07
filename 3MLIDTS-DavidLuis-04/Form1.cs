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

namespace _3MLIDTS_DavidLuis_04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtNombre.TextChanged += ValidarNombre;
            txtApellido.TextChanged += ValidarApellido;
            txtTelefono.Leave += ValidarTelefono;
            txtEdad.TextChanged += ValidarEdad;
            txtEstatura.TextChanged += ValidarEstatura;
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
                textboxLocal.Clear();
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

            string mensaje = $" Nombres: {nombres}\n Apellidos: {apellidos}\n Telefono: {telefonos}\n Edad: {edad}\n Estatura: {estatura}\n Genero: {genero}";

            ///Ruta de almacenamiento de archivo TXT
            string rutaFile = "C:\\Users\\Gustavo\\Downloads\\datos.txt";
            ///Valor de verificacion de archivo existen
            bool ArchivoExiste = File.Exists(rutaFile);
            ///Instanciacion de la clase/objeto streamwriter para escribir archivo TXT
            using (StreamWriter Escritor = new StreamWriter(rutaFile, true))
            {
                if (ArchivoExiste)
                {
                    Escritor.WriteLine();
                }
                Escritor.WriteLine(mensaje);
            }

            MessageBox.Show(mensaje, "Registros de usuario", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }
    }
}