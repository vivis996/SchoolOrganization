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
    public partial class Men_Encuestas : Form
    {
        public Men_Encuestas()
        {
            InitializeComponent();
        }

        private void lbFicha_clinica_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ficha_clinica ficha = new Ficha_clinica();
            ficha.ShowDialog();
            this.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbProxEgresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cuest_Prox_Egresar prox = new Cuest_Prox_Egresar();
            prox.ShowDialog();
            this.Show();
        }

        private void lbEgresados_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cuestionario_egresados egresados = new Cuestionario_egresados();
            egresados.ShowDialog();
            this.Show();
        }
    }
}
