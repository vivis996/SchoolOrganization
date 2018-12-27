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
    public partial class Iniciar_sesion : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;

        int intentos = 0, veces = 0;
        public Iniciar_sesion()
        {
            int cantidad = 0;
        Inicio:
            conectar.Crear_Conexion();
            string selecciona = "SELECT count(*) FROM `director`";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDR = MSQLC.ExecuteReader();
            if (MSQLDR.Read())
            {
                cantidad = Convert.ToInt32(MSQLDR["count(*)"].ToString());
            }
            conectar.Cerrar_Conexion();
        terminar:
            if (cantidad == 0)
            {
                Instalación inst = new Instalación();
                inst.ShowDialog();
                conectar.Crear_Conexion();
                selecciona = "SELECT count(*) FROM `director`";
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read())
                {
                    cantidad = Convert.ToInt32(MSQLDR["count(*)"].ToString());
                }
                conectar.Cerrar_Conexion();
                if (cantidad == 0)
                {
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("Debe agregar un administrador", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                    veces++;
                    if (veces == 4)
                    {
                        cantidad = 1;
                        goto terminar;
                    }
                }
                goto Inicio;
            }
            else
            {
                InitializeComponent();
            }
        }
        private void RadForm1_Load(object sender, EventArgs e)
        {
            if (veces == 4)
            {
                this.Close();
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            intentos = 0;
            string selecciona = "";
            this.Hide();
        Intento:
            conectar.Crear_Conexion();
            if (intentos == 0)
                selecciona = "SELECT `matricula`,`contra_alum`,`nombres`,`ape_pa`,`ape_ma`,grupo_idgrupo FROM `alumnos` WHERE matricula=" + txbMatricula.Text + ";";
            if (intentos == 1)
                selecciona = "SELECT `idprofesor`,`contra_prof`,`nombre_profesor`,`ape_pa`,`grupo_idgrupo`,`materia_idmateria` FROM `profesor` WHERE idprofesor=" + txbMatricula.Text + ";";
            if (intentos == 2)
                selecciona = "SELECT `iddirector`, `contra` FROM `director` WHERE iddirector=" + txbMatricula.Text + ";";
            try
            {
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read() == true)
                {
                    switch (intentos)
                    {
                        case 0:
                            if (MSQLDR["contra_alum"].ToString() == txbContraseña.Text)
                            {
                                Variables.Matricula = Convert.ToInt32(txbMatricula.Text);
                                Variables.Idgrupo = Convert.ToInt32(MSQLDR["grupo_idgrupo"].ToString());
                                Variables.Nombre = MSQLDR["nombres"].ToString() + " " + MSQLDR["ape_pa"].ToString() + " " + MSQLDR["ape_ma"].ToString();
                                Menu_alumnos alumno = new Menu_alumnos();
                                RadMessageBox.SetThemeName(this.ThemeName);
                                RadMessageBox.Show("Bienvenido " + Variables.Nombre, "Bienvenido", MessageBoxButtons.OK, RadMessageIcon.Info);
                                alumno.ShowDialog();
                                Variables.Matricula = 0;
                                Variables.Nombre = "";
                                Variables.Idgrupo = 0;
                            }
                            else
                            {
                                RadMessageBox.SetThemeName(this.ThemeName);
                                RadMessageBox.Show("Verifique datos", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                            }
                            break;
                        case 1:
                            if (MSQLDR["contra_prof"].ToString() == txbContraseña.Text)
                            {
                                Variables.Matricula = Convert.ToInt32(txbMatricula.Text);
                                Variables.Idgrupo = Convert.ToInt32(MSQLDR["grupo_idgrupo"].ToString());
                                Variables.IdMateria = Convert.ToInt32(MSQLDR["materia_idmateria"].ToString());
                                Variables.Nombre = MSQLDR["nombre_profesor"].ToString() + " " + MSQLDR["ape_pa"].ToString();
                                Menu_profesor profesor = new Menu_profesor();
                                RadMessageBox.SetThemeName(this.ThemeName);
                                RadMessageBox.Show("Bienvenido " + Variables.Nombre, "Bienvenido", MessageBoxButtons.OK, RadMessageIcon.Info);
                                profesor.ShowDialog();
                                Variables.Matricula = 0;
                                Variables.Nombre = "";
                            }
                            else
                            {
                                RadMessageBox.SetThemeName(this.ThemeName);
                                RadMessageBox.Show("Verifique datos", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                            }
                            break;
                        case 2:
                            //if (txbMatricula.Text == "125")
                            //{
                                if (MSQLDR["contra"].ToString() == txbContraseña.Text)
                                {
                                    Menu_director director = new Menu_director();
                                    director.ShowDialog();
                                    Variables.Matricula = 0;
                                    Variables.Nombre = "";
                                }
                                else
                                {
                                    RadMessageBox.SetThemeName(this.ThemeName);
                                    RadMessageBox.Show("Verifique datos", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                                }
                            //}
                            //if (txbMatricula.Text == "2")
                            //{
                            //    Menu_psicologa psicologa = new Menu_psicologa();
                            //    psicologa.ShowDialog();
                            //}
                            break;
                    }
                    txbMatricula.Text = "";
                    txbContraseña.Text = "";
                    intentos = 0;
                    conectar.Cerrar_Conexion();
                    this.ShowDialog();
                }
                else
                {
                    if (intentos < 2)
                    {
                        intentos++;
                        conectar.Cerrar_Conexion();
                        goto Intento;
                    }
                    else
                    {
                        RadMessageBox.SetThemeName(this.ThemeName);
                        RadMessageBox.Show("Verifique datos", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                        this.ShowDialog();
                    }
                }
            }
            catch (MySqlException)
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Verifique datos", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                this.Show();
            }
            catch (System.InvalidOperationException)
            {
                this.Show();
            }
        }
    }
}