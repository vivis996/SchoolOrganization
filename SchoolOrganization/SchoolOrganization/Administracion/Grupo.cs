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
    public partial class Grupos : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;
        private MySqlDataAdapter MSQLDA;

        DataSet tht = new DataSet();
        BindingSource bindin = new BindingSource(); // enlace para los controles
        int idGrado = 0, idGrupo = 0;
        bool Iniciar = false, grupo = false;
        public Grupos()
        {
            InitializeComponent();
        }

        private void Grupo_Load(object sender, EventArgs e)
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
            conectar.Cerrar_Conexion();
        }
        private void btnAgregarGrupo_Click(object sender, EventArgs e)
        {
            this.Hide();
            Agregar_grupo grupo = new Agregar_grupo();
            grupo.ShowDialog();
            Grupo_Load(sender, e);
            this.Show();
        }

        private void btnAlumno_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Variables.Matricula = int.Parse(txbMatricula.Text);
                Ver_alumno alumno = new Ver_alumno();
                alumno.ShowDialog();
                this.Show();
            }
            catch
            {
                MessageBox.Show("Seleccione un alumno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                cargar_Posicion();
            }
            grupo = true;
        }
        void cargar_Posicion()
        {
            conectar.Crear_Conexion();
            string selecciona = "SELECT `matricula`,`nombres`,`ape_pa`,`ape_ma` FROM `alumnos` WHERE grupo_idgrupo=" + idGrupo + ";";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            tht.Clear();
            txbMatricula.Text = "";
            conectar.Crear_Conexion();
            string valores = "SELECT * FROM `alumnos` WHERE grupo_idgrupo=" + idGrupo + ";";
            // Crea el adaptador
            MSQLDA = new MySqlDataAdapter(MSQLC);
            MSQLDA.Fill(tht, "alumnos");
            // Enlaza el bindingsourse con la tabla
            bindin.DataSource = tht.Tables["alumnos"].DefaultView;
            // Enlaza el datagrid con el bindin sourse
            dgvAlumnos.DataSource = bindin;
            //// Enñaza los textbox a los campos
            if (txbMatricula.DataBindings.Count == 0)
            {
                txbMatricula.DataBindings.Add("Text", bindin, "matricula", true);
                Variables.Matricula = Convert.ToInt32(txbMatricula.Text);
            }
            conectar.Cerrar_Conexion();
            dgvAlumnos.Columns[0].HeaderText = "Matricula";
            dgvAlumnos.Columns[1].HeaderText = "Nombre";
            dgvAlumnos.Columns[2].HeaderText = "Apellido paterno";
            dgvAlumnos.Columns[3].HeaderText = "Apellido materno";
        }
    }
}