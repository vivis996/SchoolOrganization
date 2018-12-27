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
    public partial class Parciales : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;

        int cant_parciales = 0, cant_alumnos = 0;
        public Parciales()
        {
            InitializeComponent();
        }

        private void Parciales_Load(object sender, EventArgs e)
        {
            conectar.Crear_Conexion();
            string selecciona2 = "SELECT `cantidad` FROM `materia` WHERE `idmateria`=" + Variables.IdMateria + ";";
            MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
            MSQLDR = MSQLC.ExecuteReader();
            if (MSQLDR.Read())
            {
                cant_parciales = Convert.ToInt32(MSQLDR["cantidad"].ToString());
            }
            conectar.Cerrar_Conexion();
            lbParciales.Text = "Número de parciales: " + cant_parciales;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            conectar.Crear_Conexion();
            string selecciona = "SELECT count(*) FROM `alumnos` WHERE `grupo_idgrupo`=" +  Variables.Idgrupo.ToString() + ";";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDR = MSQLC.ExecuteReader();
            if (MSQLDR.Read())
            {
                cant_alumnos = Convert.ToInt32(MSQLDR["count(*)"].ToString());
            }
            conectar.Cerrar_Conexion();
            if (cant_alumnos > 0)
            {
                cant_parciales++;
                conectar.Crear_Conexion();
                selecciona = "UPDATE `materia` SET `cantidad`=" + cant_parciales + " WHERE `idmateria`=" + Variables.IdMateria.ToString() + ";";
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                MSQLC.Connection = conectar.GetConexion();
                MSQLC.ExecuteNonQuery();
                conectar.Cerrar_Conexion();

                int[] Ids_Alumnos = new int[cant_alumnos];
                // Se obtiene los ids de los alumnos
                selecciona = "SELECT min(`matricula`) FROM `alumnos` WHERE `grupo_idgrupo`=" + Variables.Idgrupo.ToString() + ";";
                for (int i = 0; i < cant_alumnos; i++)
                {
                    conectar.Crear_Conexion();
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    MSQLDR = MSQLC.ExecuteReader();
                    if (MSQLDR.Read() == true)
                    {
                        Ids_Alumnos[i] = Convert.ToInt32(MSQLDR["min(`matricula`)"]);
                    }
                    conectar.Cerrar_Conexion();
                    selecciona = selecciona.Insert(selecciona.Length - 1, " and `matricula`!=" + Ids_Alumnos[i]);
                }


                for (int i = 0; i < cant_alumnos; i++)
                {
                    conectar.Crear_Conexion();
                    selecciona = "INSERT INTO `parcial`(`alumnos_matricula`, `materia_idmateria`, `numero`, `calificacion`) VALUES ("
                        + Ids_Alumnos[i].ToString() + "," + Variables.IdMateria.ToString() + "," + cant_parciales + "," + 0 + ")";
                    MSQLC = new MySqlCommand(selecciona);
                    MSQLC.Connection = conectar.GetConexion();
                    MSQLC.ExecuteNonQuery();
                    conectar.Cerrar_Conexion();
                }
            }
            else
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Debe haber alumnos en el salón para agregar parciales", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            }
            Parciales_Load(sender, e);
            btnAgregar.Enabled = true;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            btnQuitar.Enabled = false;
            conectar.Crear_Conexion();
            string selecciona = "SELECT count(*) FROM `alumnos` WHERE `grupo_idgrupo`=" + Variables.Idgrupo.ToString() + ";";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDR = MSQLC.ExecuteReader();
            if (MSQLDR.Read())
            {
                cant_alumnos = Convert.ToInt32(MSQLDR["count(*)"].ToString());
            }
            conectar.Cerrar_Conexion();
            if (cant_alumnos > 0)
            {
                if (cant_parciales > 1)
                {
                    int[] Ids_Alumnos = new int[cant_alumnos];
                    // Se obtiene los ids de los alumnos
                    selecciona = "SELECT min(`matricula`) FROM `alumnos` WHERE `grupo_idgrupo`=" + Variables.Idgrupo.ToString() + ";";
                    for (int i = 0; i < cant_alumnos; i++)
                    {
                        conectar.Crear_Conexion();
                        MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                        MSQLDR = MSQLC.ExecuteReader();
                        if (MSQLDR.Read() == true)
                        {
                            Ids_Alumnos[i] = Convert.ToInt32(MSQLDR["min(`matricula`)"]);
                        }
                        conectar.Cerrar_Conexion();
                        selecciona = selecciona.Insert(selecciona.Length - 1, " and `matricula`!=" + Ids_Alumnos[i]);
                    }



                    for (int i = 0; i < cant_alumnos; i++)
                    {
                        conectar.Crear_Conexion();
                        string seleccionar = "DELETE FROM `parcial` WHERE `alumnos_matricula`=" 
                            + Ids_Alumnos[i].ToString() + " and `materia_idmateria`=" + Variables.IdMateria.ToString() + " and `numero`=" + cant_parciales + ";";
                        MSQLC = new MySqlCommand(seleccionar, conectar.GetConexion());
                        MSQLC.Connection = conectar.GetConexion();
                        MSQLC.ExecuteNonQuery();
                    }


                    cant_parciales--;
                    selecciona = "UPDATE `materia` SET `cantidad`=" + cant_parciales + " WHERE `idmateria`=" + Variables.IdMateria.ToString() + ";";
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    conectar.Crear_Conexion();
                    MSQLC.Connection = conectar.GetConexion();
                    MSQLC.ExecuteNonQuery();
                    conectar.Cerrar_Conexion();
                }
                else
                {
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("Debe dejar mínimo un parcial", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);

                }
            }
            else
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Debe haber alumnos en el salón para agregar parciales", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            }
            Parciales_Load(sender, e);
            btnQuitar.Enabled = true;
        }
    }
}