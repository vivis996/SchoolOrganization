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

namespace SchoolOrganization
{
    public partial class Ficha_Tecnica_psicologa : Form
    {

        public Ficha_Tecnica_psicologa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEncuestas_Click(object sender, EventArgs e)
        {
            Variables.Matricula = Convert.ToInt32(txbMatricula.Text);
            this.Hide();
            Men_Encuestas men_encues = new Men_Encuestas();
            men_encues.ShowDialog();
            this.Show();
        }

        private void rbSi_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNo.Checked)
            {
                rtbAlergias.Text = "";
                rtbAlergias.Enabled = false;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Variables.Matricula = Convert.ToInt32(txbMatricula.Text);
            MyConection buscar = new MyConection();
            buscar.Crear_Conexion();
            string selecciona = "SELECT * FROM `alumnos` WHERE matricula=" + txbMatricula.Text + ";";
            MySqlCommand buscar_alumnos = new MySqlCommand(selecciona, buscar.GetConexion());
            MySqlDataReader leer = buscar_alumnos.ExecuteReader();
            if (leer.Read() == true)
            {
                txb_Nombres.Text = leer["nombres"].ToString();
                txb_ApePa.Text = leer["ape_pa"].ToString();
                txb_ApeMa.Text = leer["ape_ma"].ToString();
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
                // Calcular edad
                
                //

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
            }
            else
            {
                MessageBox.Show("No se encontro alumno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ficha_Tecnica_Load(object sender, EventArgs e)
        {

        }
    }
}
