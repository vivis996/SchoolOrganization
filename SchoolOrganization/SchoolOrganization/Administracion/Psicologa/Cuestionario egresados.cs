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
    public partial class Cuestionario_egresados : Form
    {
        bool[] panels = { true, true, true, true, true, true };
        public Cuestionario_egresados()
        {
            InitializeComponent();
        }

        private void lb_A_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 171);
            if (panels[0])
            {
                PanelA.Size = new Size(690, 283);
                flpPrincipal.Size = new Size(705, 461);
                panels[0] = false;
            }
            else
            {
                PanelA.Size = new Size(690, 20);
                panels[0] = true;
            }
            Panel_Seleccionado(0);
        }

        private void lb_B_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 171);
            if (panels[1])
            {
                PanelB.Size = new Size(690, 442);
                flpPrincipal.Size = new Size(705, 620);
                panels[1] = false;
            }
            else
            {
                PanelB.Size = new Size(690, 20);
                panels[1] = true;
            }
            Panel_Seleccionado(1);
        }

        private void lb_C_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 171);
            if (panels[2])
            {
                PanelC.Size = new Size(690, 396);
                flpPrincipal.Size = new Size(705, 574);
                panels[2] = false;
            }
            else
            {
                PanelC.Size = new Size(690, 20);
                panels[2] = true;
            }
            Panel_Seleccionado(2);
        }

        private void lb_D_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 171);
            if (panels[3])
            {
                PanelD.Size = new Size(690, 223);
                flpPrincipal.Size = new Size(705, 401);
                panels[3] = false;
            }
            else
            {
                PanelD.Size = new Size(690, 20);
                panels[3] = true;
            }
            Panel_Seleccionado(3);
        }

        private void lb_E_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 171);
            if (panels[4])
            {
                PanelE.Size = new Size(690, 256);
                flpPrincipal.Size = new Size(705, 434);
                panels[4] = false;
            }
            else
            {
                PanelE.Size = new Size(690, 20);
                panels[4] = true;
            }
            Panel_Seleccionado(4);
        }
        private void lb_F_Click(object sender, EventArgs e)
        {
            flpPrincipal.Size = new Size(705, 171);
            if (panels[5])
            {
                PanelF.Size = new Size(690, 730);
                flpPrincipal.Size = new Size(705, 908);
                panels[5] = false;
            }
            else
            {
                PanelF.Size = new Size(690, 20);
                panels[5] = true;
            }
            Panel_Seleccionado(5);
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
                            PanelA.Size = new Size(690, 20);
                            panels[0] = true;
                            break;
                        case 1:
                            PanelB.Size = new Size(690, 20);
                            panels[1] = true;
                            break;
                        case 2:
                            PanelC.Size = new Size(690, 20);
                            panels[2] = true;
                            break;
                        case 3:
                            PanelD.Size = new Size(690, 20);
                            panels[3] = true;
                            break;
                        case 4:
                            PanelE.Size = new Size(690, 20);
                            panels[4] = true;
                            break;
                        case 5:
                            PanelF.Size = new Size(690, 20);
                            panels[5] = true;
                            break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region B1 RadioButton Otro
        private void B1_rb_Otro_CheckedChanged(object sender, EventArgs e)
        {
            if (B1_rb_Otro.Checked == true)
                B1otro.Enabled = true;
            else
                B1otro.Enabled = false;
        }

        private void B1_rb_Amigo_CheckedChanged(object sender, EventArgs e)
        {
            if (B1_rb_Otro.Checked == true)
                B1otro.Enabled = true;
            else
                B1otro.Enabled = false;
        }

        private void B1_rb_Familiar_CheckedChanged(object sender, EventArgs e)
        {
            if (B1_rb_Otro.Checked == true)
                B1otro.Enabled = true;
            else
                B1otro.Enabled = false;
        }
        #endregion

        #region B2 RadioButton Otro
        private void B2_rb_Otro_CheckedChanged(object sender, EventArgs e)
        {
            if (B2_rb_Otro.Checked == true)
                B2otro.Enabled = true;
            else
                B2otro.Enabled = false;
        }

        private void B2_rb_Amigo_CheckedChanged(object sender, EventArgs e)
        {
            if (B2_rb_Otro.Checked == true)
                B2otro.Enabled = true;
            else
                B2otro.Enabled = false;
        }

        private void B2_rb_Familiar_CheckedChanged(object sender, EventArgs e)
        {
            if (B2_rb_Otro.Checked == true)
                B2otro.Enabled = true;
            else
                B2otro.Enabled = false;
        }
        #endregion

        #region C6 Panel
        private void C_6_5_Otro_CheckedChanged(object sender, EventArgs e)
        {
            if (C_6_5_Otro.Checked == true)
                C_6_Otro.Enabled = true;
            else
                C_6_Otro.Enabled = false;
        }

        private void C_6_1_Prestigio_CheckedChanged(object sender, EventArgs e)
        {
            if (C_6_5_Otro.Checked == true)
                C_6_Otro.Enabled = true;
            else
                C_6_Otro.Enabled = false;
        }

        private void C_6_4_Cercania_CheckedChanged(object sender, EventArgs e)
        {
            if (C_6_5_Otro.Checked == true)
                C_6_Otro.Enabled = true;
            else
                C_6_Otro.Enabled = false;
        }

        private void C_6_2_Facilidad_CheckedChanged(object sender, EventArgs e)
        {
            if (C_6_5_Otro.Checked == true)
                C_6_Otro.Enabled = true;
            else
                C_6_Otro.Enabled = false;
        }

        private void C_6_3_Ofrece_CheckedChanged(object sender, EventArgs e)
        {
            if (C_6_5_Otro.Checked == true)
                C_6_Otro.Enabled = true;
            else
                C_6_Otro.Enabled = false;
        }
#endregion

        #region C7 Otro
        private void C_7_Otro_CheckedChanged(object sender, EventArgs e)
        {
            if (C_7_Otro.Checked == true)
                C_otro.Enabled = true;
            else
                C_otro.Enabled = false;
        }

        private void C_7_1_CheckedChanged(object sender, EventArgs e)
        {
            if (C_7_Otro.Checked == true)
                C_otro.Enabled = true;
            else
                C_otro.Enabled = false;
        }

        private void C_7_3_CheckedChanged(object sender, EventArgs e)
        {
            if (C_7_Otro.Checked == true)
                C_otro.Enabled = true;
            else
                C_otro.Enabled = false;
        }

        private void C_7_2_CheckedChanged(object sender, EventArgs e)
        {
            if (C_7_Otro.Checked == true)
                C_otro.Enabled = true;
            else
                C_otro.Enabled = false;
        }

        private void C_7_4_CheckedChanged(object sender, EventArgs e)
        {
            if (C_7_Otro.Checked == true)
                C_otro.Enabled = true;
            else
                C_otro.Enabled = false;
        }
        #endregion

        #region D Opcion
        private void D_rb_8_si_CheckedChanged(object sender, EventArgs e)
        {
            if (D_rb_8_no.Checked == true)
                D_opcion.Enabled = true;
            else
                D_opcion.Enabled = false;
        }

        private void D_rb_8_no_CheckedChanged(object sender, EventArgs e)
        {
            if (D_rb_8_no.Checked == true)
                D_opcion.Enabled = true;
            else
                D_opcion.Enabled = false;
        }
        #endregion

        #region D10 Otro
        private void D_10_1_CheckedChanged(object sender, EventArgs e)
        {
            if (D_10_5_otro.Checked == true)
                D_otro.Enabled = true;
            else
                D_otro.Enabled = false;
        }

        private void D_10_4_CheckedChanged(object sender, EventArgs e)
        {
            if (D_10_5_otro.Checked == true)
                D_otro.Enabled = true;
            else
                D_otro.Enabled = false;
        }

        private void D_10_3_CheckedChanged(object sender, EventArgs e)
        {
            if (D_10_5_otro.Checked == true)
                D_otro.Enabled = true;
            else
                D_otro.Enabled = false;
        }

        private void D_10_2_CheckedChanged(object sender, EventArgs e)
        {
            if (D_10_5_otro.Checked == true)
                D_otro.Enabled = true;
            else
                D_otro.Enabled = false;
        }

        private void D_10_5_otro_CheckedChanged(object sender, EventArgs e)
        {
            if (D_10_5_otro.Checked == true)
                D_otro.Enabled = true;
            else
                D_otro.Enabled = false;
        }
#endregion
    }
}