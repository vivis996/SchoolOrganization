using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Telerik.WinControls;
using MySql.Data.MySqlClient;

namespace Proyexto_Final___Vianey
{
    public partial class Ver_Alum : Form
    {
        MyConection conectar = new MyConection();
        int idGrado = 0, idGrupo = 0, idGrupo_Alumno = 0;
        bool Iniciar = false, grupo = false;
        public Ver_Alum()
        {
            InitializeComponent();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (panel3.Enabled)
            {
                panel3.Enabled = false;
            }
            else
            {
                if (txb_Nombres.Text.Count() > 3)
                    panel3.Enabled = true;
                else
                    MessageBox.Show("Busque un alumno antes de editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Variables.Matricula = Convert.ToInt32(txbMatricula.Text);

                conectar.Crear_Conexion();
                string selecciona = "SELECT * FROM `alumnos` WHERE matricula=" + txbMatricula.Text + ";";
                MySqlCommand buscar_alumnos = new MySqlCommand(selecciona, conectar.GetConexion());
                MySqlDataReader leer = buscar_alumnos.ExecuteReader();
                if (leer.Read() == true)
                {
                    txb_Nombres.Text = leer["nombres"].ToString();
                    txb_ApePa.Text = leer["ape_pa"].ToString();
                    txb_ApeMa.Text = leer["ape_ma"].ToString();
                    txbContraseña.Text = leer["contra_alum"].ToString();
                    //txb_Grado.Text = leer["grado"].ToString();
                    //txb_Grupo.Text = leer["grupo"].ToString();
                    if (leer["genero"].ToString() == "Masculino")
                        rbMasculino.Checked = true;
                    else
                        rbFemenino.Checked = true;
                    txb_Tipo_Sangre.Text = leer["tipo_sang"].ToString();
                    txb_Calle_Num.Text = leer["calle_num"].ToString();
                    txb_Colo_Comu.Text = leer["colon_comu"].ToString();
                    mtxb_Cod_Postal.Text = leer["cod_pos"].ToString();
                    txb_Ciudad.Text = leer["ciudad"].ToString();
                    txb_Municipio.Text = leer["muni"].ToString();
                    txb_Estado.Text = leer["estado"].ToString();
                    rtbAlergias.Text = leer["alergias"].ToString();
                    if (rtbAlergias.Text.Count() > 0)
                        rbSi.Checked = true;
                    else
                        rbNo.Checked = true;
                    txb_tutor_Ape_Pa.Text = leer["ape_pa_tutor"].ToString();
                    txb_tutor_Ape_Ma.Text = leer["ape_ma_tutor"].ToString();
                    txb_tutor_Nombres.Text = leer["nombres_tutor"].ToString();
                    try
                    {
                        mtxb_Fecha_nac.Text = leer["fecha_nac"].ToString().Substring(0, 10);
                        Variables fecha = new Variables();

                        int dia = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(0, 2));
                        int mes = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(3, 2));
                        int año = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(6));
                        mtxb_Edad.Text = fecha.Calcular_Edad(dia, mes, año).ToString();
                    }
                    catch { }
                    if (leer["genero_tutor"].ToString() == "Masculino")
                        rb_tutor_Masculino.Checked = true;
                    else
                        rb_tutor_Femenino.Checked = true;

                    // NIVEL DE PROFESION!!


                    //
                    txb_tutor_Profesion.Text = leer["prof"].ToString();
                    mtxb_tutor_Num_tel.Text = leer["num_tel_tutor"].ToString();
                    txb_tutor_Correo.Text = leer["correo_tutor"].ToString();
                    mtxb_tutor_Num_hijos.Text = leer["num_hijos_tutor"].ToString();
                    txb_tutor_Calle_Num.Text = leer["calle_num_tutor"].ToString();
                    txb_tutor_Colo_Com.Text = leer["colon_comu_tutor"].ToString();
                    mtxb_tutor_Cod_Postal.Text = leer["cod_pos_tutor"].ToString();
                    txb_tutor_Ciudad.Text = leer["ciudad_tutor"].ToString();
                    txb_tutor_Municipio.Text = leer["muni_tutor"].ToString();
                    txb_tutor_Estado.Text = leer["estado_tutor"].ToString();
                    idGrupo_Alumno = Convert.ToInt32(leer["grupo_idgrupo"]);
                    idGrupo = idGrupo_Alumno;
                    ////////////////////////////////////////////////////////////////////////////////

                    string id = "";
                    conectar.Crear_Conexion();
                    string selecciona2 = "SELECT `grado_idgrado`, `nombre_grupo` FROM `grupo` WHERE `idgrupo` =" + idGrupo_Alumno + ";";
                    MySqlCommand Buscar2 = new MySqlCommand(selecciona2, conectar.GetConexion());
                    MySqlDataReader leer2 = Buscar2.ExecuteReader();
                    if (leer2.Read())
                    {
                        id = leer2["grado_idgrado"].ToString();
                        idGrado = Convert.ToInt32(id);
                        cbGrupo.Text = leer2["nombre_grupo"].ToString();
                    }
                    conectar.Cerrar_Conexion();
                    conectar.Crear_Conexion();
                    selecciona2 = "SELECT `grado` FROM `grado` WHERE `idgrado` =" + idGrado + ";";
                    MySqlCommand Buscar3 = new MySqlCommand(selecciona2, conectar.GetConexion());
                    MySqlDataReader leer3 = Buscar3.ExecuteReader();
                    if (leer3.Read())
                    {
                        cbGrado.Text = leer3["grado"].ToString();
                    }
                    cbGrado.SelectedItem = cbGrado.Text;
                    cbGrupo.SelectedItem = cbGrupo.Text;
                    ////////////////////////////////////////////////////////////////////////////////

                }
                else
                {
                    //RadMessageBox.SetThemeName(this.ThemeName);
                    Telerik.WinControls.RadMessageBox.Show("No se encontro alumno", "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error);
                }
            }
            catch (MySqlException)
            {
                //RadMessageBox.SetThemeName(this.ThemeName);
                Telerik.WinControls.RadMessageBox.Show("La base de datos no esta disponible", "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error);
            }
            catch (System.FormatException)
            {
                //RadMessageBox.SetThemeName(this.ThemeName);
                Telerik.WinControls.RadMessageBox.Show("Ingrese matricula", "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Variables.Matricula = 0;
            this.Close();
        }

        private void rbSi_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNo.Checked)
            {
                rtbAlergias.Text = "";
                rtbAlergias.Enabled = false;
            }
            else
                rtbAlergias.Enabled = true;
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNo.Checked)
            {
                rtbAlergias.Text = "";
                rtbAlergias.Enabled = false;
            }
        }

        private void Ver_Alumno_Load(object sender, EventArgs e)
        {
            if (Variables.Matricula > 0)
            {
                txbMatricula.Text = Variables.Matricula.ToString();
                btnBuscar.PerformClick();
            }
            conectar.Crear_Conexion();
            string selecciona = "SELECT `grado` FROM `grado` where Activo=1;";
            MySqlCommand Buscar = new MySqlCommand(selecciona, conectar.GetConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(Buscar);
            DataSet tht = new DataSet();
            Buscar.Connection = conectar.GetConexion();
            cmc.Fill(tht, "grado");
            cbGrado.DataSource = tht.Tables["grado"].DefaultView;
            Iniciar = false;
            grupo = false;
            cbGrado.ValueMember = "grado";
            cbGrado.Text = "Selecciona";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Variables.Matricula = Convert.ToInt32(txbMatricula.Text);
            string dia = mtxb_Fecha_nac.Text;
            string num = mtxb_tutor_Num_tel.Text.Replace("-", ""), genero = "", genero_tutor = "", fecha = dia.Substring(6) + "-" + dia.Substring(3, 2) + "-" + dia.Substring(0, 2) + " 00:00:00";
            if (rbMasculino.Checked)
                genero = "Masculino";
            if (rbFemenino.Checked)
                genero = "Femenino";
            if (rb_tutor_Masculino.Checked)
                genero_tutor = "Masculino";
            if (rb_tutor_Femenino.Checked)
                genero_tutor = "Femenino";
            conectar.Crear_Conexion();
            string actualizar = "UPDATE alumnos SET ape_pa=@C2,ape_ma=@C3,nombres=@C4,contra_alum=@C5,genero=@C6,fecha_nac=@C7,tipo_sang=@C8,calle_num=@C9,colon_comu=@C10,cod_pos=@C11,ciudad=@C12,muni=@C13,estado=@C14,alergias=@C15,ape_pa_tutor=@C16,ape_ma_tutor=@C17,nombres_tutor=@C18,genero_tutor=@C19,nivel_max_tutor=@C20,num_tel_tutor=@C21,correo_tutor=@C22,num_hijos_tutor=@C23,calle_num_tutor=@C24,colon_comu_tutor=@C25,cod_pos_tutor=@C26,ciudad_tutor=@C27,muni_tutor=@C28,estado_tutor=@C29,prof=@C30 WHERE martricula='" + Variables.Matricula.ToString() + "'";
            MySqlCommand revisa = new MySqlCommand(actualizar);
            revisa.Connection = conectar.GetConexion();
            revisa.Parameters.AddWithValue("@C2", (txb_ApePa.Text));
            revisa.Parameters.AddWithValue("@C3", (txb_ApeMa.Text));
            revisa.Parameters.AddWithValue("@C4", (txb_Nombres.Text));
            revisa.Parameters.AddWithValue("@C5", (txbContraseña.Text));
            revisa.Parameters.AddWithValue("@C6", (genero));
            revisa.Parameters.AddWithValue("@C7", (fecha));
            revisa.Parameters.AddWithValue("@C8", (txb_Tipo_Sangre.Text));
            revisa.Parameters.AddWithValue("@C9", (txb_Calle_Num.Text));
            revisa.Parameters.AddWithValue("@C10", (txb_Colo_Comu.TabIndex));
            revisa.Parameters.AddWithValue("@C11", (mtxb_Cod_Postal.Text));
            revisa.Parameters.AddWithValue("@C12", (txb_Ciudad.TabIndex));
            revisa.Parameters.AddWithValue("@C13", (txb_Municipio.Text));
            revisa.Parameters.AddWithValue("@C14", (txb_Estado.Text));
            revisa.Parameters.AddWithValue("@C15", (rtbAlergias.Text));
            revisa.Parameters.AddWithValue("@C16", (txb_tutor_Ape_Pa.Text));
            revisa.Parameters.AddWithValue("@C17", (txb_tutor_Ape_Ma.Text));
            revisa.Parameters.AddWithValue("@C18", (txb_tutor_Nombres.Text));
            revisa.Parameters.AddWithValue("@C19", (genero_tutor));
            revisa.Parameters.AddWithValue("@C20", (cb_Nivel_Estudio.Text));
            revisa.Parameters.AddWithValue("@C21", (num));
            revisa.Parameters.AddWithValue("@C22", (txb_tutor_Correo.Text));
            revisa.Parameters.AddWithValue("@C23", (mtxb_tutor_Num_hijos.Text));
            revisa.Parameters.AddWithValue("@C24", (txb_tutor_Calle_Num.Text));
            revisa.Parameters.AddWithValue("@C25", (txb_tutor_Colo_Com.Text));
            revisa.Parameters.AddWithValue("@C26", (mtxb_tutor_Cod_Postal.Text));
            revisa.Parameters.AddWithValue("@C27", (txb_tutor_Ciudad.Text));
            revisa.Parameters.AddWithValue("@C28", (txb_tutor_Municipio.Text));
            revisa.Parameters.AddWithValue("@C29", (txb_tutor_Estado.Text));
            revisa.Parameters.AddWithValue("@C30", (txb_tutor_Profesion.Text));
            //revisa.Parameters.AddWithValue("@C33", ());
            try
            {
                conectar.GetConexion();
                revisa.ExecuteNonQuery();
                conectar.Cerrar_Conexion();
                MessageBox.Show("Registro Modificado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch
            {
                MessageBox.Show("ERRRORRR", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MySqlCommand Buscar2 = new MySqlCommand(selecciona2, conectar.GetConexion());
                MySqlDataReader leer = Buscar2.ExecuteReader();
                if (leer.Read())
                {
                    id = leer["idgrado"].ToString();
                    idGrado = Convert.ToInt32(id);

                }
                conectar.Cerrar_Conexion();
                string fecha = DateTime.Today.ToString("MM");
                if (Convert.ToInt32(fecha) >= 8)
                {

                }
                selecciona2 = "SELECT `nombre_grupo` FROM `grupo` WHERE grado_idgrado=" + id + ";";
                MySqlCommand Buscar = new MySqlCommand(selecciona2, conectar.GetConexion());
                MySqlDataAdapter cmc = new MySqlDataAdapter(Buscar);
                DataSet tht = new DataSet();
                Buscar.Connection = conectar.GetConexion();
                cmc.Fill(tht, "grupo");
                cbGrupo.DataSource = tht.Tables["grupo"].DefaultView;
                grupo = false;
                cbGrupo.ValueMember = "nombre_grupo";
                cbGrupo.Text = "Selecciona";
            }
            Iniciar = true;
        }

        private void mtxb_Fecha_nac_TextChanged(object sender, EventArgs e)
        {
            Variables fecha = new Variables();
            try
            {
                int dia = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(0, 2));
                int mes = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(3, 2));
                int año = Convert.ToInt32(mtxb_Fecha_nac.Text.ToString().Substring(6));
                mtxb_Edad.Text = fecha.Calcular_Edad(dia, mes, año).ToString();
            }
            catch { }
        }

        private void cbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grupo)
            {
                string id = "";
                conectar.Crear_Conexion();
                string selecciona2 = "SELECT * FROM `grupo` WHERE `nombre_grupo` LIKE '" + cbGrupo.Text + "' AND `grado_idgrado` = " + idGrado + " ORDER BY `idgrupo` ASC;";
                MySqlCommand Buscar2 = new MySqlCommand(selecciona2, conectar.GetConexion());
                MySqlDataReader leer = Buscar2.ExecuteReader();
                if (leer.Read())
                {
                    id = leer["idgrupo"].ToString();
                    idGrupo = Convert.ToInt32(id);
                }
                conectar.Cerrar_Conexion();
            }
            grupo = true;
        }
    }
}