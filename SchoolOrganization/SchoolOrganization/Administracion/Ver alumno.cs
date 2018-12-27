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
    public partial class Ver_alumno : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;
        private MySqlDataAdapter MSQLDA;

        int idGrado = 0, idGrupo = 0, idGrupo_Alumno = 0;
        bool Iniciar = false, grupo = false;
        public Ver_alumno()
        {
            InitializeComponent();
        }
        private void Ver_alumno_Load(object sender, EventArgs e)
        {
            if (Variables.Matricula > 0)
            {
                txbMatricula.Text = Variables.Matricula.ToString();
                btnBuscar.PerformClick();
            }
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
            string selecciona = "SELECT * FROM `alumnos`;";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDR = MSQLC.ExecuteReader();
            while (MSQLDR.Read())
            {
                string nombre = MSQLDR["matricula"].ToString();
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
                string selecciona = "SELECT * FROM `alumnos` WHERE matricula=" + txbMatricula.Text + ";";
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read() == true)
                {
                    txb_Nombres.Text = MSQLDR["nombres"].ToString();
                    txb_ApePa.Text = MSQLDR["ape_pa"].ToString();
                    txb_ApeMa.Text = MSQLDR["ape_ma"].ToString();
                    txbContraseña.Text = MSQLDR["contra_alum"].ToString();
                    //txb_Grado.Text = leer["grado"].ToString();
                    //txb_Grupo.Text = leer["grupo"].ToString();
                    if (MSQLDR["genero"].ToString() == "Masculino")
                        rbMasculino.IsChecked = true;
                    else
                        rbFemenino.IsChecked = true;
                    txb_Tipo_Sangre.Text = MSQLDR["tipo_sang"].ToString();
                    txb_Calle_Num.Text = MSQLDR["calle_num"].ToString();
                    txb_Colo_Comu.Text = MSQLDR["colon_comu"].ToString();
                    mtxb_Cod_Postal.Text = MSQLDR["cod_pos"].ToString();
                    txb_Ciudad.Text = MSQLDR["ciudad"].ToString();
                    txb_Municipio.Text = MSQLDR["muni"].ToString();
                    txb_Estado.Text = MSQLDR["estado"].ToString();
                    rtbAlergias.Text = MSQLDR["alergias"].ToString();
                    if (rtbAlergias.Text.Length > 0)
                        rbSi.IsChecked = true;
                    else
                        rbNo.IsChecked = true;
                    //txb_tutor_Ape_Pa.Text = leer["ape_pa_tutor"].ToString();
                    //txb_tutor_Ape_Ma.Text = leer["ape_ma_tutor"].ToString();
                    //txb_tutor_Nombres.Text = leer["nombres_tutor"].ToString();
                    try
                    {
                        mtxb_Fecha_nac.Text = MSQLDR["fecha_nac"].ToString().Substring(0, 10);
                        Variables fecha = new Variables();

                        int dia = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(0, 2));
                        int mes = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(3, 2));
                        int año = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(6, 4));
                        mtxb_Edad.Text = fecha.Calcular_Edad(dia, mes, año).ToString();
                    }
                    catch { }
                    //if (leer["genero_tutor"].ToString() == "Masculino")
                    //    rb_tutor_Masculino.Checked = true;
                    //else
                    //    rb_tutor_Femenino.Checked = true;

                    // NIVEL DE PROFESION!!


                    ////
                    //txb_tutor_Profesion.Text = leer["prof"].ToString();
                    //mtxb_tutor_Num_tel.Text = leer["num_tel_tutor"].ToString();
                    //txb_tutor_Correo.Text = leer["correo_tutor"].ToString();
                    //mtxb_tutor_Num_hijos.Text = leer["num_hijos_tutor"].ToString();
                    //txb_tutor_Calle_Num.Text = leer["calle_num_tutor"].ToString();
                    //txb_tutor_Colo_Com.Text = leer["colon_comu_tutor"].ToString();
                    //mtxb_tutor_Cod_Postal.Text = leer["cod_pos_tutor"].ToString();
                    //txb_tutor_Ciudad.Text = leer["ciudad_tutor"].ToString();
                    //txb_tutor_Municipio.Text = leer["muni_tutor"].ToString();
                    //txb_tutor_Estado.Text = leer["estado_tutor"].ToString();
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
                    cbGrado.SelectedItem = cbGrado.Text;
                    cbGrupo.SelectedItem = cbGrupo.Text;
                    ////////////////////////////////////////////////////////////////////////////////
                }
                else
                {
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("No se encontro alumno", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
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
                    MessageBox.Show("Busque un alumno antes de editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void rbSi_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (rbNo.IsChecked)
            {
                rtbAlergias.Text = "";
                rtbAlergias.Enabled = false;
            }
            else
                rtbAlergias.Enabled = true;
        }

        private void rbNo_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (rbNo.IsChecked)
            {
                rtbAlergias.Text = "";
                rtbAlergias.Enabled = false;
            }
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Variables.Matricula = Convert.ToInt32(txbMatricula.Text);
            string dia = mtxb_Fecha_nac.Text;
            string genero = "",
               fecha = dia.Substring(6, 4) + "-" + dia.Substring(3, 2) + "-" + dia.Substring(0, 2) + " 00:00:00";
            //string num = mtxb_tutor_Num_tel.Text.Replace("-", ""), genero_tutor = "";
            if (rbMasculino.IsChecked)
                genero = "Masculino";
            if (rbFemenino.IsChecked)
                genero = "Femenino";
            //if (rb_tutor_Masculino.Checked)
            //    genero_tutor = "Masculino";
            //if (rb_tutor_Femenino.Checked)
            //    genero_tutor = "Femenino";
            conectar.Crear_Conexion();
            string selecciona = "SELECT `contra_alum` FROM `alumnos` WHERE matricula=" + Variables.Matricula.ToString() + ";";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            conectar.Crear_Conexion();
            selecciona = "UPDATE `alumnos` SET `ape_pa`='" + txb_ApePa.Text + "',`ape_ma`='" + txb_ApeMa.Text +
                "',`nombres`='" + txb_Nombres.Text + "',`contra_alum`=" + txbContraseña.Text + ",`genero`='" + genero +
                "',`fecha_nac`='" + fecha + "',`tipo_sang`='" + txb_Tipo_Sangre.Text + "',`calle_num`='" + txb_Calle_Num.Text +
                "',`colon_comu`='" + txb_Colo_Comu.Text + "',`cod_pos`='" + mtxb_Cod_Postal.Text + "',`ciudad`='" + txb_Ciudad.Text +
                "',`muni`='" + txb_Municipio.Text + "',`estado`='" + txb_Estado.Text + "',`alergias`='" + rtbAlergias.Text + 
                "' WHERE matricula=" + Variables.Matricula.ToString() + ";";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLC.Connection = conectar.GetConexion();
            MSQLC.ExecuteNonQuery();
            conectar.Cerrar_Conexion();
            RadMessageBox.SetThemeName(this.ThemeName);
            RadMessageBox.Show("Se ha modificado la información", "Éxito", MessageBoxButtons.OK, RadMessageIcon.Info);
        }
    }
}