using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.WinControls;
using Telerik.WinControls.Themes;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

// Libreria necesaria
using MySql.Data.MySqlClient;

namespace SchoolOrganization
{
    class MyConection
    {
        public MySqlConnection conexion;
        public void Crear_Conexion()
        {
            string conec = "Server=localhost;database=proyecto_final;uid=root;pwd=;";
            conexion = new MySqlConnection(conec);
            try
            {
                conexion.Open();
            }
            catch
            {
                RadMessageBox.Show("La base de datos no disponible", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        public void Cerrar_Conexion()
        {
            conexion.Close();
        }

        public MySqlConnection GetConexion()
        {
            return conexion;
        }
    }
}