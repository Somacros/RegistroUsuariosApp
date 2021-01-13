using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RegistroUsuarios
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if(txtName.Text != "" && txtName.Text != null)
            {
                string messageBack = registerUser(txtName.Text, calculateAge(dtpBirth.Value));
                MessageBox.Show(messageBack, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } else
            {
                MessageBox.Show("¡Por favor introduzca un nombre en el campo solicitado!","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string registerUser(string Name, int age)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = desktopPath + @"\Users.txt";
            if (!File.Exists(path))
            {
                using(StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(Name);
                    sw.WriteLine(age.ToString());
                    sw.WriteLine("------------------------------------------------------");
                }

                return String.Format("Nombre: {0}. \nEdad: {1}.\n\nFichero creado con éxito y usuario añadido de manera correcta en {2}.", Name, age, path );
            } else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(Name);
                    sw.WriteLine(age.ToString());
                    sw.WriteLine("------------------------------------------------------");
                }

                return String.Format("Nombre: {0}.\nEdad: {1}.\n\nEl nuevo usuario fué agregado al fichero de texto que se encuentra en {2}",Name, age, path);
            }
        }

        private int calculateAge(DateTime date)
        {

            DateTime now = DateTime.Today;

            int age = now.Year - date.Year;

            if (now.Month < date.Month || (now.Month == date.Month && now.Day < date.Day))
                age--;

            return age;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            dtpBirth.Value = DateTime.Now;
        }
    }
}
