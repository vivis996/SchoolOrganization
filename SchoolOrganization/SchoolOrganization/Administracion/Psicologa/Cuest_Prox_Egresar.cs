using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolOrganization
{
    public partial class Cuest_Prox_Egresar : Form
    {
        bool[] panels = { true, true, true, true, true, true, true };
        public Cuest_Prox_Egresar()
        {
            InitializeComponent();
        }
        private void lb_A_DatosGenerales_Click(object sender, EventArgs e)
        {
            flp_Principal.Size = new Size(705, 189);
            if (panels[0])
            {
                pn_A.Size = new Size(690, 258);
                flp_Principal.Size = new Size(705, 447);
                panels[0] = false;
            }
            else
            {
                pn_A.Size = new Size(690, 20);
                panels[0] = true;
            }
            Panel_Seleccionado(0);
        }       
        private void lb_B_Referencias_Click(object sender, EventArgs e)
        {
            flp_Principal.Size = new Size(705, 189);
            if (panels[1])
            {
                pn_B.Size = new Size(690, 450);
                flp_Principal.Size = new Size(705, 639);
                panels[1] = false;
            }
            else
            {
                pn_B.Size = new Size(690, 20);
                panels[1] = true;
            }
            Panel_Seleccionado(1);
        }
        private void lb_C_Historial_Click(object sender, EventArgs e)
        {
            flp_Principal.Size = new Size(705, 189);
            if (panels[2])
            {
                pn_C.Size = new Size(690, 174);
                flp_Principal.Size = new Size(705, 382);
                panels[2] = false;
            }
            else
            {
                pn_C.Size = new Size(690, 20);
                panels[2] = true;
            }
            Panel_Seleccionado(2);
        }
        private void lb_D_Formacion_Click(object sender, EventArgs e)
        {
            flp_Principal.Size = new Size(705, 189);
            if (panels[3])
            {
                pn_D.Size = new Size(690, 94);
                flp_Principal.Size = new Size(705, 288);
                panels[3] = false;
            }
            else
            {
                pn_D.Size = new Size(690, 20);
                panels[3] = true;
            }
            Panel_Seleccionado(3);
        }
        private void lb_E_Aspiraciones_Click(object sender, EventArgs e)
        {
            flp_Principal.Size = new Size(705, 189);
            if (panels[4])
            {
                pn_E.Size = new Size(690, 266);
                flp_Principal.Size = new Size(705, 455);
                panels[4] = false;
            }
            else
            {
                pn_E.Size = new Size(690, 20);
                panels[4] = true;
            }
            Panel_Seleccionado(4);
        }
        private void lb_F_Condiciones_Click(object sender, EventArgs e)
        {
            flp_Principal.Size = new Size(705, 189);
            if (panels[5])
            {
                pn_F.Size = new Size(690, 235);
                flp_Principal.Size = new Size(705, 424);
                panels[5] = false;
            }
            else
            {
                pn_F.Size = new Size(690, 20);
                panels[5] = true;
            }
            Panel_Seleccionado(5);
        }
        private void lb_G_Insercion_Click(object sender, EventArgs e)
        {
            flp_Principal.Size = new Size(705, 189);
            if (panels[6])
            {
                pb_G.Size = new Size(690, 540);
                flp_Principal.Size = new Size(705, 729);
                panels[6] = false;
            }
            else
            {
                pb_G.Size = new Size(690, 20);
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
                    switch (i)
                    {
                        case 0:
                            pn_A.Size = new Size(690,20);
                            panels[0] = true;
                            break;
                        case 1:
                            pn_B.Size = new Size(690, 20);
                            panels[1] = true;
                            break;
                        case 2:
                            pn_C.Size = new Size(690, 20);
                            panels[2] = true;
                            break;
                        case 3:
                            pn_D.Size = new Size(690, 20);
                            panels[3] = true;
                            break;
                        case 4:
                            pn_E.Size = new Size(690, 20);
                            panels[4] = true;
                            break;
                        case 5:
                            pn_F.Size = new Size(690, 20);
                            panels[5] = true;
                            break;
                        case 6:
                            pb_G.Size = new Size(690, 20);
                            panels[6] = true;
                            break;
                    }
                }
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}