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
    public partial class Menu_director : Telerik.WinControls.UI.RadForm
    {
        public Menu_director()
        {
            InitializeComponent();
        }

        private void btnVer_Alumnos_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ver_alumno ficha = new Ver_alumno();
            ficha.ShowDialog();
            Variables.Matricula = 0;
            this.Show();
        }

        private void btnProfesores_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ver_Profesor ver_profe = new Ver_Profesor();
            ver_profe.ShowDialog();
            Variables.Matricula = 0;
            this.Show();
        }

        private void btnIngresar_Alumno_Click(object sender, EventArgs e)
        {
            this.Hide();
            Agregar_alumno agregar = new Agregar_alumno();
            agregar.ShowDialog();
            this.Show();
        }

        private void btnIngresar_Profesores_Click(object sender, EventArgs e)
        {
            this.Hide();
            Agregar_Profesor profesor = new Agregar_Profesor();
            profesor.ShowDialog();
            this.Show();
        }

        private void btnGrupos_Click(object sender, EventArgs e)
        {
            this.Hide();
            Grupos grupo = new Grupos();
            grupo.ShowDialog();
            this.Show();
        }

        private void btnAgregarGrupo_Click(object sender, EventArgs e)
        {
            this.Hide();
            Agregar_grupo grupo = new Agregar_grupo();
            grupo.ShowDialog();
            this.Show();
        }
    }
}