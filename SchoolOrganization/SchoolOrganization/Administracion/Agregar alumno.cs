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
    public partial class Agregar_alumno : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;
        private MySqlDataAdapter MSQLDA;

        int idGrado = 0, idGrupo = 0;
        bool Iniciar = false, grupo = false;
        public Agregar_alumno()
        {
            InitializeComponent();
        }
        private void Agregar_alumno_Load(object sender, EventArgs e)
        {
            rbNo.IsChecked = true;
            conectar.Crear_Conexion();
            string selecciona = "SELECT `grado` FROM `grado` where Activo=1;";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDA = new MySqlDataAdapter(MSQLC);
            DataSet tht = new DataSet();
            MSQLC.Connection = conectar.GetConexion();
            MSQLDA.Fill(tht, "grado");
            cbGrado.DataSource = tht.Tables["grado"].DefaultView;
            Iniciar = false;
            grupo = false;
            cbGrado.ValueMember = "grado";
            cbGrado.Text = "Selecciona";
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string dia = mtxb_Fecha_nac.Text;
            string genero = "", fecha = dia.Substring(6) + "-" + dia.Substring(3, 2) + "-" + dia.Substring(0, 2) + " 00:00:00";
            //string num = mtxb_tutor_Num_tel.Text.Replace("-", ""), genero_tutor = "";
            if (rbMasculino.IsChecked)
                genero = "Masculino";
            if (rbFemenino.IsChecked)
                genero = "Femenino";
            //if (rb_tutor_Masculino.IsChecked)
            //    genero_tutor = "Masculino";
            //if (rb_tutor_Femenino.IsChecked)
            //    genero_tutor = "Femenino";


            conectar.Crear_Conexion();
            string insertar = "INSERT INTO `proyecto_final`.`alumnos` (`matricula`, `ape_pa`, `ape_ma`, `nombres`, `contra_alum`, `genero`, `fecha_nac`, `tipo_sang`, `calle_num`, `colon_comu`, `cod_pos`, `ciudad`, `muni`, `estado`, `alergias`,`grupo_idgrupo`) VALUES ("
                + "'" + txbMatricula.Text + "','" + txb_ApePa.Text + "','" + txb_ApeMa.Text + "','" + txb_Nombres.Text + "','" + 123 + "','" + genero + "','" + fecha
                + "','" + txb_Tipo_Sangre.Text + "','" + txb_Calle_Num.Text + "','" + txb_Colo_Comu.Text
                + "','" + mtxb_Cod_Postal.Text + "','" + txb_Ciudad.Text + "','" + txb_Municipio.Text + "','" + txb_Estado.Text + "','" + rtbAlergias.Text
                + "','" + idGrupo + "')";
            MSQLC = new MySqlCommand(insertar);
            MSQLC.Connection = conectar.GetConexion();
            try
            {
                MSQLC.ExecuteNonQuery();
                conectar.Cerrar_Conexion();


                int cantidad = 0;
                conectar.Crear_Conexion();
                // Se obtiene la cantidad de materias
                string selecciona = "SELECT count(*) FROM `materia` WHERE `grupo_idgrupo`=" + idGrupo + ";";
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read() == true)
                {
                    cantidad = Convert.ToInt32(MSQLDR["count(*)"]);
                }
                conectar.Cerrar_Conexion();
                int[] Ids_Materias = new int[cantidad];
                int[] cant_Parciales = new int[cantidad];
                // Se obtiene los ids de las materias
                selecciona = "SELECT min(`idmateria`) FROM `materia` WHERE `grupo_idgrupo`=" + idGrupo + ";";
                for (int i = 0; i < cantidad; i++)
                {
                    conectar.Crear_Conexion();
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    MSQLDR = MSQLC.ExecuteReader();
                    if (MSQLDR.Read() == true)
                    {
                        Ids_Materias[i] = Convert.ToInt32(MSQLDR["min(`idmateria`)"]);
                    }
                    conectar.Cerrar_Conexion();
                    selecciona = selecciona.Insert(selecciona.Length - 1, " and `idmateria`!=" + Ids_Materias[i]);
                }
                // Se obtiene la cantidad de parciales de la materia
                for (int i = 0; i < cantidad; i++)
                {
                    selecciona = "SELECT `cantidad` FROM `materia` WHERE `idmateria`=" + Ids_Materias[i].ToString();
                    conectar.Crear_Conexion();
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    MSQLDR = MSQLC.ExecuteReader();
                    if (MSQLDR.Read() == true)
                    {
                        cant_Parciales[i] = Convert.ToInt32(MSQLDR["cantidad"]);
                    }
                    conectar.Cerrar_Conexion();
                }
                // Se agregan la cantidad de parciales de la materia
                for (int i = 0; i < cantidad; i++)
                {
                    for (int j = 0; j < cant_Parciales[i]; j++)
                    {
                        conectar.Crear_Conexion();
                        insertar = "INSERT INTO `parcial`(`alumnos_matricula`, `materia_idmateria`, `numero`, `calificacion`) VALUES ("
                            + txbMatricula.Text + "," + Ids_Materias[i].ToString() + "," + (j + 1) + "," + 0 + ")";
                        MSQLC = new MySqlCommand(insertar);
                        MSQLC.Connection = conectar.GetConexion();
                        MSQLC.ExecuteNonQuery();
                        conectar.Cerrar_Conexion();
                    }
                }
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Se ha agregado satisfactoriamente", "Éxito", MessageBoxButtons.OK, RadMessageIcon.Info);
                this.Close();
            }
            catch (MySqlException)
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("La base de datos no esta disponible", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
            catch (System.FormatException)
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Ingrese matricula", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
            catch (System.InvalidOperationException)
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Error con base de datos", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void mtxb_Fecha_nac_TextChanged(object sender, EventArgs e)
        {
            Variables fecha = new Variables();
            try
            {
                int dia = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(0, 2));
                int mes = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(3, 2));
                int año = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(6, 4));
                mtxb_Edad.Text = fecha.Calcular_Edad(dia, mes, año).ToString();
            }
            catch { }
        }

        private void cbGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = "";
            grupo = false;
            if (Iniciar == true && cbGrado.SelectedIndex >= 0)
            {
                conectar.Crear_Conexion();
                string selecciona2 = "SELECT `idgrado` FROM `grado` where Activo=1 and grado=" + cbGrado.Text + ";";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read())
                {
                    id = MSQLDR["idgrado"].ToString();
                    idGrado = Convert.ToInt32(id);

                }
                conectar.Cerrar_Conexion();
                string fecha = DateTime.Today.ToString("MM");
                if (Convert.ToInt32(fecha) >= 8)
                {

                }
                selecciona2 = "SELECT `nombre_grupo` FROM `grupo` WHERE grado_idgrado=" + id + ";";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDA = new MySqlDataAdapter(MSQLC);
                DataSet tht = new DataSet();
                MSQLC.Connection = conectar.GetConexion();
                MSQLDA.Fill(tht, "grupo");
                cbGrupo.DataSource = tht.Tables["grupo"].DefaultView;
                grupo = false;
                cbGrupo.ValueMember = "nombre_grupo";
                cbGrupo.Text = "Selecciona";
            }
            Iniciar = true;
        }

        private void cbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grupo)
            {
                string id = "";
                conectar.Crear_Conexion();
                string selecciona2 = "SELECT * FROM `grupo` WHERE `nombre_grupo` LIKE '" + cbGrupo.Text + "' AND `grado_idgrado` = " + idGrado + " ORDER BY `idgrupo` ASC;";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read())
                {
                    id = MSQLDR["idgrupo"].ToString();
                    idGrupo = Convert.ToInt32(id);
                }
                conectar.Cerrar_Conexion();
            }
            grupo = true;
        }
    }
}
