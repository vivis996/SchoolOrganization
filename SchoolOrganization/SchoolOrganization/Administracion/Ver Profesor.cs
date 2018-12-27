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
    public partial class Ver_Profesor : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;
        private MySqlDataAdapter MSQLDA;

        int idGrado = 0, idGrupo = 0, idGrupo_Alumno = 0, idMateria = 0;
        bool Iniciar = false, grupo = false, materia = false;

        public Ver_Profesor()
        {
            InitializeComponent();
        }
        private void Ver_Profesor_Load(object sender, EventArgs e)
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
            Auto_acompletar();
        }
        private void Auto_acompletar()
        {
            txbMatricula.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txbMatricula.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            conectar.Crear_Conexion();
            string selecciona = "SELECT * FROM `profesor`;";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDR = MSQLC.ExecuteReader();
            while (MSQLDR.Read())
            {
                string nombre = MSQLDR["idprofesor"].ToString();
                coll.Add(nombre);
            }
            conectar.Cerrar_Conexion();
            txbMatricula.AutoCompleteCustomSource = coll;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Variables.Matricula = Convert.ToInt32(txbMatricula.Text);
                conectar.Crear_Conexion();
                string selecciona = "SELECT * FROM `profesor` WHERE idprofesor=" + txbMatricula.Text + ";";
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read() == true)
                {
                    txb_Nombres.Text = MSQLDR["nombre_profesor"].ToString();
                    txb_ApePa.Text = MSQLDR["ape_pa"].ToString();
                    txb_ApeMa.Text = MSQLDR["ape_ma"].ToString();
                    txbContraseña.Text = MSQLDR["contra_prof"].ToString();
                    if (MSQLDR["genero"].ToString() == "Masculino")
                        rbMasculino.IsChecked = true;
                    else
                        rbFemenino.IsChecked = true;
                    txb_Tipo_Sangre.Text = MSQLDR["tip_san"].ToString();
                    try
                    {
                        mtxb_Fecha_nac.Text = MSQLDR["fecha_nac"].ToString().Substring(0, 10);
                        Variables fecha = new Variables();

                        int dia = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(0, 2));
                        int mes = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(3, 2));
                        int año = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(6));
                        mtxb_Edad.Text = fecha.Calcular_Edad(dia, mes, año).ToString();
                    }
                    catch { }
                    idGrupo_Alumno = Convert.ToInt32(MSQLDR["grupo_idgrupo"]);
                    idGrupo = idGrupo_Alumno;

                    ////////////////////////////////////////////////////////////////////////////////

                    string id = "";
                    conectar.Crear_Conexion();
                    string selecciona2 = "SELECT `grado_idgrado`, `nombre_grupo` FROM `grupo` WHERE `idgrupo` =" + idGrupo_Alumno + ";";
                    MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                    MSQLDR = MSQLC.ExecuteReader();
                    if (MSQLDR.Read())
                    {
                        id = MSQLDR["grado_idgrado"].ToString();
                        idGrado = Convert.ToInt32(id);
                        cbGrupo.Text = MSQLDR["nombre_grupo"].ToString();
                    }
                    conectar.Cerrar_Conexion();
                    conectar.Crear_Conexion();
                    selecciona2 = "SELECT `grado` FROM `grado` WHERE `idgrado` =" + idGrado + ";";
                    MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                    MSQLDR = MSQLC.ExecuteReader();
                    if (MSQLDR.Read())
                    {
                        cbGrado.Text = MSQLDR["grado"].ToString();
                    }
                    conectar.Cerrar_Conexion();
                    conectar.Crear_Conexion();
                    selecciona2 = "SELECT `idmateria`,`nombre_materia` FROM `materia` WHERE `grupo_idgrupo`=" + idGrupo + ";";
                    MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                    MSQLDR = MSQLC.ExecuteReader();
                    if (MSQLDR.Read())
                    {
                        cbMateria.Text = MSQLDR["nombre_materia"].ToString();
                        idMateria = Convert.ToInt32(MSQLDR["idmateria"].ToString());
                    }
                    cbGrado.SelectedItem = cbGrado.Text;
                    cbGrupo.SelectedItem = cbGrupo.Text;
                    cbMateria.SelectedItem = cbMateria.Text;
                    conectar.Cerrar_Conexion();
                    ////////////////////////////////////////////////////////////////////////////////

                }
                else
                {
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("No se encontro profesor", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
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
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (pnPrincipal.Enabled)
            {
                pnPrincipal.Enabled = false;
            }
            else
            {
                if (txb_Nombres.Text.Length > 3)
                    pnPrincipal.Enabled = true;
                else
                {
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("Busque un profesor antes de editar", "Aviso", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                }
            }
        }

        private void cbGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            grupo = false;
            materia = false;
            if (Iniciar == true && cbGrado.SelectedIndex >= 0)
            {
                cbMateria.Text = "";
                conectar.Crear_Conexion();
                string selecciona = "SELECT `idgrado` FROM `grado` where Activo=1 and grado=" + cbGrado.Text + ";";
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
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
                selecciona = "SELECT `nombre_grupo` FROM `grupo` WHERE grado_idgrado=" + idGrado + ";";
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                MSQLDA = new MySqlDataAdapter(MSQLC);
                DataSet tht = new DataSet();
                MSQLC.Connection = conectar.GetConexion();
                MSQLDA.Fill(tht, "grupo");
                cbGrupo.DataSource = tht.Tables["grupo"].DefaultView;
                grupo = false;
                cbGrupo.ValueMember = "nombre_grupo";
                cbGrupo.Text = "Selecciona";
                grupo = true;
                materia = true;
            }
            Iniciar = true;
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string dia = mtxb_Fecha_nac.Text;
            string genero = "",
                fecha = dia.Substring(6, 4) + "-" + dia.Substring(3, 2) + "-" + dia.Substring(0, 2) + " 00:00:00";
            if (rbMasculino.IsChecked)
                genero = "Masculino";
            if (rbFemenino.IsChecked)
                genero = "Femenino";
            string selecciona = "SELECT `contra_alum` FROM `alumnos` WHERE matricula=" + Variables.Matricula.ToString() + ";";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            conectar.Crear_Conexion();
            selecciona = "UPDATE `profesor` SET `nombre_profesor`='" + txb_Nombres.Text + "',`ape_pa`='" + txb_ApePa.Text +
                "',`ape_ma`='" + txb_ApeMa.Text + "',`genero`='" + genero + "',`contra_prof`='" + txbContraseña.Text +
                "',`grupo_idgrupo`=" + idGrupo + ",`tip_san`='" + txb_Tipo_Sangre.Text + "',`fecha_nac`='" + fecha +
                "',`materia_idmateria`=" + idMateria + " WHERE `idprofesor`=" + Variables.Matricula.ToString() + ";";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLC.Connection = conectar.GetConexion();
            MSQLC.ExecuteNonQuery();
            conectar.Cerrar_Conexion();
            RadMessageBox.SetThemeName(this.ThemeName);
            RadMessageBox.Show("Se ha modificado la información", "Éxito", MessageBoxButtons.OK, RadMessageIcon.Info);
        }
    }
}