using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Telerik.WinControls;

namespace SchoolOrganization
{
    public partial class Cambiar_contraseña_Prof : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;

        public Cambiar_contraseña_Prof()
        {
            InitializeComponent();
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            string contraseña = "";
            conectar.Crear_Conexion();
            string selecciona = "SELECT `contra_prof` FROM `profesor` WHERE `idprofesor`=" + Variables.Matricula.ToString() + ";";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDR = MSQLC.ExecuteReader();
            if (MSQLDR.Read() == true)
            {
                contraseña = MSQLDR["contra_prof"].ToString();
            }
            conectar.Cerrar_Conexion();
            if (contraseña == txbActual.Text)
            {
                if (txbNueva.Text.Length > 3)
                {
                    if (txbNueva.Text == txbRepetir.Text)
                    {
                        contraseña = txbNueva.Text;
                        conectar.Crear_Conexion();
                        selecciona = "UPDATE `profesor` SET `contra_prof`='" + contraseña + "' WHERE `idprofesor`=" + Variables.Matricula.ToString() + ";";
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
                    RadMessageBox.Show("Contraseña demasiado corta", "Error", MessageBoxButtons.OK, RadMessageIcon.Info);
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