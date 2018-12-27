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
    public partial class Menu_profesor : Telerik.WinControls.UI.RadForm
    {
        public Menu_profesor()
        {
            InitializeComponent();
        }

        private void rbContraseña_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cambiar_contraseña_Prof cambiar = new Cambiar_contraseña_Prof();
            cambiar.ShowDialog();
            this.Show();
        }

        private void Menu_profesor_Load(object sender, EventArgs e)
        {
            this.Text = "Menu profesor (" + Variables.Nombre + ")";
        }

        private void btnGrupo_Click(object sender, EventArgs e)
        {
            this.Hide();
            Grupo_profesor prof = new Grupo_profesor();
            prof.ShowDialog();
            Variables.IdAlumno = 0;
            this.Show();
        }

        private void btnAlumno_Click(object sender, EventArgs e)
        {
            this.Hide();
            Calificacion_alumno calificacion = new Calificacion_alumno();
            calificacion.ShowDialog();
            Variables.IdAlumno = 0;
            this.Show();
        }
    }
}
