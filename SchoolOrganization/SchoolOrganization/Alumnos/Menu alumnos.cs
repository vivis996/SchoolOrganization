using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SchoolOrganization
{
    public partial class Menu_alumnos : Telerik.WinControls.UI.RadForm
    {
        public Menu_alumnos()
        {
            InitializeComponent();
        }

        private void btnMateria_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Show();
        }

        private void btnCalificaciones_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ver_calificaciones calificacion = new Ver_calificaciones();
            calificacion.ShowDialog();
            this.Show();
        }

        private void btnContraseña_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cambiar_contraseña contra = new Cambiar_contraseña();
            contra.ShowDialog();
            this.Show();
        }

        private void Menu_alumnos_Load(object sender, EventArgs e)
        {
            this.Text = "Menu alumnos (" + Variables.Nombre + ")";
        }
    }
}
