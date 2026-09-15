using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3MLIDTS_DavidLuis_04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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