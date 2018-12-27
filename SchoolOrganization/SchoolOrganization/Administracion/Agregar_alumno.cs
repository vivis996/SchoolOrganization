using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Proyexto_Final___Vianey
{
    public partial class Agregar_Alumno : Form
    {
        MyConection conectar = new MyConection();
        int idGrado = 0, idGrupo = 0;
        bool Iniciar = false, grupo = false;
        public Agregar_Alumno()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbSi_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSi.Checked)
            {
                rtbAlergias.Enabled = true;
            }
        }
        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNo.Checked)
            {
                rtbAlergias.Text = "";
                rtbAlergias.Enabled = false;
            }
        }

        private void Ficha_Tecnica_Load(object sender, EventArgs e)
        {
            rbNo.Checked = true;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string dia = mtxb_Fecha_nac.Text;
            string num = mtxb_tutor_Num_tel.Text.Replace("-",""),genero = "", genero_tutor = "", fecha = dia.Substring(6) + "-" + dia.Substring(3, 2) + "-" + dia.Substring(0, 2) + " 00:00:00";
            if(rbMasculino.Checked)
                genero = "Masculino";
            if(rbFemenino.Checked)
                genero = "Femenino";
            if(rb_tutor_Masculino.Checked)
                genero_tutor="Masculino";
            if(rb_tutor_Femenino.Checked)
                genero_tutor="Femenino";


            conectar.Crear_Conexion();
            string insertar = "INSERT INTO `proyecto_final`.`alumnos` (`matricula`, `ape_pa`, `ape_ma`, `nombres`, `contra_alum`, `genero`, `fecha_nac`, `tipo_sang`, `calle_num`, `colon_comu`, `cod_pos`, `ciudad`, `muni`, `estado`, `alergias`, `ape_pa_tutor`, `ape_ma_tutor`, `nombres_tutor`, `genero_tutor`, `nivel_max_tutor`, `num_tel_tutor`, `correo_tutor`, `num_hijos_tutor`, `calle_num_tutor`, `colon_comu_tutor`, `cod_pos_tutor`, `ciudad_tutor`, `muni_tutor`, `estado_tutor`, `prof`, `grupo_idgrupo`) VALUES ("
                + "'" + txb_Matricula.Text + "','" + txb_ApePa.Text + "','" + txb_ApeMa.Text + "','" + txb_Nombres.Text + "','" + 123 +"','" + genero + "','" + fecha
                + "','" + txb_Tipo_Sangre.Text + "','" + txb_Calle_Num.Text + "','" + txb_Colo_Comu.Text
                + "','" + mtxb_Cod_Postal.Text + "','" + txb_Ciudad.Text + "','" + txb_Municipio.Text + "','" + txb_Estado.Text + "','" + rtbAlergias.Text
                + "','" + txb_tutor_Ape_Pa.Text + "','" + txb_tutor_Ape_Ma.Text + "','" + txb_tutor_Nombres.Text + "','" + genero_tutor + "','" + cb_Nivel_Estudio.Text
                + "','" + num + "','" + txb_tutor_Correo.Text + "','" + mtxb_tutor_Num_hijos.Text + "','" + txb_tutor_Calle_Num.Text
                + "','" + txb_tutor_Colo_Com.Text + "','" + mtxb_tutor_Cod_Postal.Text + "','" + txb_tutor_Ciudad.Text + "','" + txb_Municipio.Text + "','" + txb_tutor_Estado.Text + "','" + txb_tutor_Profesion.Text + "','" + idGrupo + "')";
            MySqlCommand pro = new MySqlCommand(insertar);
            pro.Connection = conectar.GetConexion();
            try
            {
                pro.ExecuteNonQuery();
                conectar.Cerrar_Conexion();
                MessageBox.Show("Se ha ingresado satisfactoriamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Error con base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("Se ha duplicado la informacion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                //agregar.Crear_Conexion();
                //insertar = "INSERT INTO "
                //this.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("Se ha duplicado la informacion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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