using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using MySql.Data.MySqlClient;

namespace SchoolOrganization
{
    public partial class Cambiar_contraseña : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;

        public Cambiar_contraseña()
        {
            InitializeComponent();
        }
        private void btnCambiar_Click(object sender, EventArgs e)
        {
            string contraseña = "";
            conectar.Crear_Conexion();
            string selecciona = "SELECT `contra_alum` FROM `alumnos` WHERE matricula=" + Variables.Matricula.ToString() + ";";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDR = MSQLC.ExecuteReader();
            if (MSQLDR.Read() == true)
            {
                contraseña = MSQLDR["contra_alum"].ToString();
            }
            conectar.Cerrar_Conexion();
            if (contraseña == txbActual.Text)
            {
                if (txbNueva.Text == txbRepetir.Text)
                {
                    contraseña = txbNueva.Text;
                    conectar.Crear_Conexion();
                    selecciona = "UPDATE `alumnos` SET `contra_alum`='" + contraseña + "' WHERE `matricula`=" + Variables.Matricula.ToString() + ";";
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    MSQLC.Connection = conectar.GetConexion();
                    MSQLC.ExecuteNonQuery();
                    conectar.Cerrar_Conexion();
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("Se ha cambiado la contraseña", "Éxito", MessageBoxButtons.OK, RadMessageIcon.Info);
                    this.Close();
                }
                else
                {
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("Contraseñas no coinciden", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                }
            }
            else
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Contraseñas actual incorrecta", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }
    }
}
