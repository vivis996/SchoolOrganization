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
    public partial class Calificacion_alumno : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;
        private MySqlDataAdapter MSQLDA;

        DataSet tht = new DataSet();
        BindingSource bindin = new BindingSource(); // enlace para los controles

        public Calificacion_alumno()
        {
            InitializeComponent();
        }

        private void Calificacion_alumno_Load(object sender, EventArgs e)
        {
            if (Variables.IdAlumno > 0)
            {
                txbMatricula.Text = Variables.IdAlumno.ToString();
                btnBuscar.PerformClick();
            }
            Auto_acompletar();
        }

        private void Auto_acompletar()
        {
            txbMatricula.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txbMatricula.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            conectar.Crear_Conexion();
            string selecciona = "SELECT * FROM `alumnos` WHERE grupo_idgrupo=" + Variables.Idgrupo + ";";
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
                Variables.IdAlumno = Convert.ToInt32(txbMatricula.Text);
                conectar.Crear_Conexion();
                string selecciona = "SELECT * FROM `alumnos` WHERE matricula=" + txbMatricula.Text + " and grupo_idgrupo=" + Variables.Idgrupo +";";
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read() == true)
                {
                    lbNombre.Text = MSQLDR["nombres"].ToString() + " " 
                        + MSQLDR["ape_pa"].ToString() + " " + MSQLDR["ape_ma"].ToString();
                    this.Text = "Calificación de " + lbNombre.Text;

                    conectar.Crear_Conexion();
                    selecciona = "SELECT numero,calificacion FROM materia,parcial where idmateria=materia_idmateria and idmateria=" + Variables.IdMateria.ToString() +" and alumnos_matricula=" + Variables.IdAlumno + ";";
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    tht.Clear();
                    conectar.Crear_Conexion();
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
                        try
                        {
                            txbMatricula.DataBindings.Add("Text", bindin, "matricula", true);
                            Variables.Matricula = Convert.ToInt32(txbMatricula.Text);
                        }
                        catch { }
                    }
                    conectar.Cerrar_Conexion();
                    dgvAlumnos.Columns[0].HeaderText = "Parcial";
                    dgvAlumnos.Columns[1].HeaderText = "Calificación";
                }
                else
                {
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("No se encontro alumno", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                }
            }
            catch
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("La base de datos no esta disponible", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvAlumnos.RowCount; i++)
            {
                string selecciona = "UPDATE `parcial` SET `calificacion`=" + dgvAlumnos.Rows[i].Cells[1].Value +
                " WHERE `alumnos_matricula`=" + Variables.Matricula.ToString()
                + " and `materia_idmateria`=" + Variables.IdMateria.ToString() +
                " and `numero`=" + dgvAlumnos.Rows[i].Cells[0].Value + ";";
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                conectar.Crear_Conexion();
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                MSQLC.Connection = conectar.GetConexion();
                MSQLC.ExecuteNonQuery();
                conectar.Cerrar_Conexion();
            }
            txbMatricula.Text = Variables.Matricula.ToString();
            btnBuscar.PerformClick();
            RadMessageBox.SetThemeName(this.ThemeName);
            RadMessageBox.Show("Se ha modificado la información", "Éxito", MessageBoxButtons.OK, RadMessageIcon.Info);
        }
    }
}