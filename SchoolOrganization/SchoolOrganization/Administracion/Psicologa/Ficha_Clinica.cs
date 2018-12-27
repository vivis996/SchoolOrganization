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
    public partial class Ficha_clinica : Form
    {
        MyConection conectar = new MyConection();
        bool[] panels = { true, true, true, true, true, true, true };
        public Ficha_clinica()
        {
            InitializeComponent();
        }

        private void lb1Condiciones_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 390);
            if (panels[0])
            {
                pn1_Condiciones.Size = new Size(690, 168);
                flpPrincipal.Size = new Size(705, 538);
                panels[0] = false;
            }
            else
            {
                pn1_Condiciones.Size = new Size(690, 20);
                panels[0] = true;
            }
            Panel_Seleccionado(0);
        }

        private void lb2Periodo_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 390);
            if (panels[1])
            {
                pn2_Periodo.Size = new Size(690, 76);
                flpPrincipal.Size = new Size(705, 440);
                panels[1] = false;
            }
            else
            {
                pn2_Periodo.Size = new Size(690, 20);
                panels[1] = true;
            }
            Panel_Seleccionado(1);
        }

        private void lb3Historial_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 390);
            if (panels[2])
            {
                pn3_Historial.Size = new Size(690, 364);
                flpPrincipal.Size = new Size(705, 734);
                panels[2] = false;
            }
            else
            {
                pn3_Historial.Size = new Size(690, 20);
                panels[2] = true;
            }
            Panel_Seleccionado(2);
        }

        private void lb4Sueño_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 390);
            if (panels[3])
            {
                pn4_Sueño.Size = new Size(690, 255);
                flpPrincipal.Size = new Size(705, 625);
                panels[3] = false;
            }
            else
            {
                pn4_Sueño.Size = new Size(690, 20);
                panels[3] = true;
            }
            Panel_Seleccionado(3);
        }

        private void lb5Antecedentes_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 390);
            if (panels[4])
            {
                pn5_Antecedentes.Size = new Size(690, 441);
                flpPrincipal.Size = new Size(705, 811);
                panels[4] = false;
            }
            else
            {
                pn5_Antecedentes.Size = new Size(690, 20);
                panels[4] = true;
            }
            Panel_Seleccionado(4);
        }

        private void lb6Desarrollo_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 390);
            if (panels[5])
            {
                pn6_Desarollo.Size = new Size(690, 638);
                flpPrincipal.Size = new Size(705, 1008);
                panels[5] = false;
            }
            else
            {
                pn6_Desarollo.Size = new Size(690, 20);
                panels[5] = true;
            }
            Panel_Seleccionado(5);
        }

        private void lb7Acontecimientos_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 390);
            if (panels[6])
            {
                pn7_Acontecimientos.Size = new Size(690, 300);
                flpPrincipal.Size = new Size(705, 670);
                panels[6] = false;
            }
            else
            {
                pn7_Acontecimientos.Size = new Size(690, 20);
                panels[6] = true;
            }
            Panel_Seleccionado(6);
        }
        private void Panel_Seleccionado(int OtroPanel)
        {
            for (int i = 0; i < panels.Count(); i++)
            {
                if (i != OtroPanel)
                {
                    switch(i)
                    {
                        case 0:
                            pn1_Condiciones.Size = new Size(690, 20);
                            panels[0] = true;
                            break;
                        case 1:
                            pn2_Periodo.Size = new Size(690, 20);
                            panels[1] = true;
                            break;
                        case 2:
                            pn3_Historial.Size = new Size(690, 20);
                            panels[2] = true;
                            break;
                        case 3:
                            pn4_Sueño.Size = new Size(690, 20);
                            panels[3] = true;
                            break;
                        case 4:
                            pn5_Antecedentes.Size = new Size(690, 20);
                            panels[4] = true;
                            break;
                        case 5:
                            pn6_Desarollo.Size = new Size(690, 20);
                            panels[5] = true;
                            break;
                        case 6:
                            pn7_Acontecimientos.Size = new Size(690, 20);
                            panels[6] = true;
                            break;
                    }
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ficha_clinica_Load(object sender, EventArgs e)
        {
            conectar.Crear_Conexion();
            string selecciona = "SELECT `matricula`, `ape_pa`, `ape_ma`, `nombres`,`grado`,`grupo` FROM `alumnos` WHERE matricula=" + Variables.Matricula.ToString() + ";";
            MySqlCommand buscar_alumnos = new MySqlCommand(selecciona, conectar.GetConexion());
            //MySqlDataAdapter cmc = new MySqlDataAdapter(buscar_alumnos);
            //DataSet tht = new DataSet();
            MySqlDataReader leer = buscar_alumnos.ExecuteReader();
            if( leer.Read() == true)
            {
                txbNombre.Text = leer["ape_pa"].ToString() +" " + leer["ape_ma"].ToString() + " " + leer["nombres"].ToString();
                txbGrado.Text = leer["grado"].ToString();
                txbGrupo.Text = leer["grupo"].ToString();
                txb_Matricula.Text = leer["matricula"].ToString();
            }
            //buscar_alumnos.Connection = buscar.GetConexion();
        }
    }
}