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
    public partial class Instalación : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        public Instalación()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txbContraseña.Text == txbRepetir.Text)
            {
                conectar.Crear_Conexion();
                string insertar = "INSERT INTO `director`(`iddirector`, `contra`) VALUES (" + txbUsuario.Text + ",'" + txbContraseña.Text + "')";
                MSQLC = new MySqlCommand(insertar);
                MSQLC.Connection = conectar.GetConexion();
                MSQLC.ExecuteNonQuery();
                conectar.Cerrar_Conexion();
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Se ha agregado satisfactoriamente", "Éxito", MessageBoxButtons.OK, RadMessageIcon.Info);
                this.Close();
            }
            else
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Contraseñas no coinciden", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            }
        }
    }
}
