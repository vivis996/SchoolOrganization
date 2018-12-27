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
    public partial class Agregar_Profesor : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;
        private MySqlDataAdapter MSQLDA;

        int idGrado = 0, idGrupo = 0, idMateria = 0;
        bool Iniciar = false, grupo = false, materia = false;
        public Agregar_Profesor()
        {
            InitializeComponent();
        }

        private void Agregar_Profesor_Load(object sender, EventArgs e)
        {
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string dia = mtxb_Fecha_nac.Text;
            string genero = "",
                fecha = dia.Substring(6,4) + "-" + dia.Substring(3, 2) + "-" + dia.Substring(0, 2) + " 00:00:00";
            if (rbMasculino.IsChecked)
                genero = "Masculino";
            if (rbFemenino.IsChecked)
                genero = "Femenino";
            conectar.Crear_Conexion();
            string insertar = "INSERT INTO `profesor`(`idprofesor`, `ape_pa`, `ape_ma`, `nombre_profesor`, `genero`, `contra_prof`, `grupo_idgrupo`, `tip_san`, `fecha_nac`, `materia_idmateria`) VALUES ("
                + "'" + txb_Matricula.Text + "','" + txb_ApePa.Text + "','" + txb_ApeMa.Text + "','" + txb_Nombres.Text + "','"
                + genero + "','" + 1234 + "','" + idGrupo + "','" + txb_Tipo_Sangre.Text + "','" + fecha + "','" + idMateria + "')";
            MSQLC = new MySqlCommand(insertar);
            MSQLC.Connection = conectar.GetConexion();
            try
            {
                MSQLC.ExecuteNonQuery();
                conectar.Cerrar_Conexion();
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

        private void cbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grupo)
            {
                conectar.Crear_Conexion();
                string selecciona2 = "SELECT * FROM `grupo` WHERE `nombre_grupo` LIKE '" + cbGrupo.Text + "' AND `grado_idgrado` = " + idGrado + " ORDER BY `idgrupo` ASC;";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read())
                {
                    idGrupo = Convert.ToInt32(MSQLDR["idgrupo"].ToString());
                }
                conectar.Cerrar_Conexion();



                selecciona2 = "SELECT * FROM `materia` WHERE grupo_idgrupo=" + idGrupo + ";";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDA = new MySqlDataAdapter(MSQLC);
                DataSet tht = new DataSet();
                MSQLC.Connection = conectar.GetConexion();
                MSQLDA.Fill(tht, "materia");
                cbMateria.DataSource = tht.Tables["materia"].DefaultView;
                cbMateria.ValueMember = "nombre_materia";
                cbMateria.Text = "Selecciona";
            }
            grupo = true;
        }

        private void cbGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            grupo = false;
            materia = false;
            if (Iniciar == true && cbGrado.SelectedIndex >= 0)
            {
                cbMateria.Text = "";
                conectar.Crear_Conexion();
                string selecciona2 = "SELECT `idgrado` FROM `grado` where Activo=1 and grado=" + cbGrado.Text + ";";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read())
                {
                    idGrado = Convert.ToInt32(MSQLDR["idgrado"].ToString());
                }
                conectar.Cerrar_Conexion();
                string fecha = DateTime.Today.ToString("MM");
                if (Convert.ToInt32(fecha) >= 8)
                {

                }
                selecciona2 = "SELECT `nombre_grupo` FROM `grupo` WHERE grado_idgrado=" + idGrado + ";";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDA = new MySqlDataAdapter(MSQLC);
                DataSet tht = new DataSet();
                MSQLC.Connection = conectar.GetConexion();
                MSQLDA.Fill(tht, "grupo");
                cbGrupo.DataSource = tht.Tables["grupo"].DefaultView;
                grupo = false;
                cbGrupo.ValueMember = "nombre_grupo";
                cbGrupo.Text = "Selecciona";
                cbMateria.Text = "";
                grupo = true;
                materia = true;
            }
            Iniciar = true;
        }

        private void cbMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (materia)
            {
                conectar.Crear_Conexion();
                string selecciona2 = "SELECT `idmateria` FROM `materia` WHERE `grupo_idgrupo`=" + idGrupo
                    + " and nombre_materia='" + cbMateria.Text + "';";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read())
                {
                    idMateria = Convert.ToInt32(MSQLDR["idmateria"].ToString());

                }
                conectar.Cerrar_Conexion();
            }
            materia = true;
        }
    }
}