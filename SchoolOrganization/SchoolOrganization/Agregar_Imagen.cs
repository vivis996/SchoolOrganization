using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolOrganization
{
    public partial class Agregar_Imagen : Form
    {
        public Agregar_Imagen()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.openFileDialog1.ShowDialog();
                if(this.openFileDialog1.FileName.Equals("")==false)
                {
                    pictureBox1.Load(this.openFileDialog1.FileName);
                    textBox1.Text = this.openFileDialog1.FileName.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar la imagen: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
