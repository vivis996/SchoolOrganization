using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Containers;
using MySql.Data.MySqlClient;

namespace SchoolOrganization
{
    public partial class Agregar_grupo : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;
        private MySqlDataAdapter MSQLDA;

        int idGrado = 0, idGrupo = 0, numgrado = 0;
        bool Iniciar = false, grupo = false;
        public Agregar_grupo()
        {
            InitializeComponent();
        }

        private void Agregar_grupo_Load(object sender, EventArgs e)
        {
            conectar.Crear_Conexion();
            string selecciona = "SELECT `grado` FROM `grado` where Activo=1;";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDA = new MySqlDataAdapter(MSQLC);
            DataSet tht = new DataSet();
            MSQLC.Connection = conectar.GetConexion();
            MSQLDA.Fill(tht, "grado");
            cbGrado.DataSource = tht.Tables["grado"].DefaultView;
            Iniciar = true;
            cbGrado.ValueMember = "grado";
            cbGrado.Text = "Selecciona";
            conectar.Cerrar_Conexion();
        }

        private void cbGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = "";
            if (Iniciar == true && cbGrado.SelectedIndex >=0)
            {
                conectar.Crear_Conexion();
                string selecciona2 = "SELECT `idgrado`,`grado` FROM `grado` where Activo=1 and grado=" + cbGrado.Text + ";";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read())
                {
                    idGrado = Convert.ToInt32(MSQLDR["idgrado"].ToString());
                    numgrado = Convert.ToInt32(MSQLDR["grado"].ToString());
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
                cbGrupo.ValueMember = "nombre_grupo";
                cbGrupo.Text = "Selecciona";
            }
            Iniciar = true;
        }

        private void btnGrado_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbGrado.Items.Count < 3)
                {
                    string dia = DateTime.Today.ToString("dd/MM/yyyy");
                    string fecha = dia.Substring(6) + "-" + dia.Substring(3, 2) + "-" + dia.Substring(0, 2) + " 00:00:00";
                    conectar.Crear_Conexion();
                    string insertar = "INSERT INTO `grado`( `grado`, `año_ingreso`, `Activo`) VALUES ('" + txbGrado.Text + "','" + fecha + "','" + 1 + "')";
                    MSQLC = new MySqlCommand(insertar);
                    MSQLC.Connection = conectar.GetConexion();
                    try
                    {
                        MSQLC.ExecuteNonQuery();
                        conectar.Cerrar_Conexion();
                        txbGrado.Text = "";
                        txbEspecialidad.Text = "";
                        txbGrupo.Text = "";
                        RadMessageBox.SetThemeName(this.ThemeName);
                        RadMessageBox.Show("Se ha ingresado satisfactoriamente", "Éxito", MessageBoxButtons.OK, RadMessageIcon.Info);
                    }
                    catch (System.InvalidOperationException)
                    {
                        RadMessageBox.SetThemeName(this.ThemeName);
                        RadMessageBox.Show("Error con base de datos", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                    Agregar_grupo_Load(sender, e);
                }
                else
                {
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("Grados llenos", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
            catch
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Ingrese solo números", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void btnGrupo_Click(object sender, EventArgs e)
        {
            if (txbGrupo.Text.Length > 0 && txbGrupo.Text.Length < 2)
            {
                int numero = 0;
                bool letra = false;
                try
                {
                    numero = Convert.ToInt32(txbGrupo.Text);
                }
                catch 
                {
                    letra = true;
                }
                if (numero > 0 || numero < 0 || numero == 0 && letra == false)
                {
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("Ingrese la letra del grado", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                }
                else
                {
                    conectar.Crear_Conexion();
                    string insertar = "INSERT INTO `grupo`(`nombre_grupo`, `especialidad`, `grado_idgrado`) VALUES ('" + txbGrupo.Text.ToUpper().ToString() +"','" + txbEspecialidad.Text + "','" + idGrado + "')";
                    MSQLC = new MySqlCommand(insertar);
                    MSQLC.Connection = conectar.GetConexion();
                    try
                    {
                        MSQLC.ExecuteNonQuery();
                        conectar.Cerrar_Conexion();
                        txbGrado.Text = "";
                        txbEspecialidad.Text = "";
                        txbGrupo.Text = "";
                        RadMessageBox.SetThemeName(this.ThemeName);
                        RadMessageBox.Show("Se ha ingresado satisfactoriamente", "Éxito", MessageBoxButtons.OK, RadMessageIcon.Info);
                    }
                    catch (System.InvalidOperationException)
                    {
                        RadMessageBox.SetThemeName(this.ThemeName);
                        RadMessageBox.Show("Error con base de datos", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                    Agregar_grupo_Load(sender, e);
                }
            }
            else
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Ingrese la letra del grado", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            }
        }

        private void cbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grupo)
            {
                conectar.Crear_Conexion();
                string selecciona2 = "SELECT * FROM `grupo` WHERE `nombre_grupo` LIKE '" + cbGrupo.Text + "' AND `grado_idgrado` = " + idGrado + " ORDER BY `idgrupo` ASC;";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read())
                {
                    idGrupo = Convert.ToInt32(MSQLDR["idgrupo"].ToString());
                }
                conectar.Cerrar_Conexion();
                selecciona2 = "SELECT * FROM `materia` WHERE grupo_idgrupo=" + idGrupo + ";";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDA = new MySqlDataAdapter(MSQLC);
                DataSet tht = new DataSet();
                MSQLC.Connection = conectar.GetConexion();
                MSQLDA.Fill(tht, "materia");
                cbMateria.DataSource = tht.Tables["materia"].DefaultView;
                cbMateria.ValueMember = "nombre_materia";
                cbMateria.Text = "Selecciona";
            }
            grupo = true;
        }

        private void cbGrado_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            grupo = false;
            if (Iniciar == true && cbGrado.SelectedIndex >= 0)
            {
                cbMateria.Text = "";
                conectar.Crear_Conexion();
                string selecciona2 = "SELECT `idgrado` FROM `grado` where Activo=1 and grado=" + cbGrado.Text + ";";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read())
                {
                    idGrado = Convert.ToInt32(MSQLDR["idgrado"].ToString());

                }
                conectar.Cerrar_Conexion();
                string fecha = DateTime.Today.ToString("MM");
                if (Convert.ToInt32(fecha) >= 8)
                {

                }
                selecciona2 = "SELECT `nombre_grupo` FROM `grupo` WHERE grado_idgrado=" + idGrado + ";";
                MSQLC = new MySqlCommand(selecciona2, conectar.GetConexion());
                MSQLDA = new MySqlDataAdapter(MSQLC);
                DataSet tht = new DataSet();
                MSQLC.Connection = conectar.GetConexion();
                MSQLDA.Fill(tht, "grupo");
                cbGrupo.DataSource = tht.Tables["grupo"].DefaultView;
                grupo = false;
                cbGrupo.ValueMember = "nombre_grupo";
                cbGrupo.Text = "Selecciona";
                grupo = true;
                cbMateria.Text = "";
            }
            Iniciar = true;
        }

        private void btnMateria_Click(object sender, EventArgs e)
        {
            if (txbMateria.Text.Length > 2 && txbDescripcion.Text.Length > 2)
            {
                conectar.Crear_Conexion();
                string insertar = "INSERT INTO `materia`( `nombre_materia`, `descripcion`, `cantidad`, `grupo_idgrupo`) VALUES ('" + txbMateria.Text + "','" + txbDescripcion.Text + "','" + 1 + "','" + idGrupo + "')";
                MSQLC = new MySqlCommand(insertar);
                MSQLC.Connection = conectar.GetConexion();
                try
                {
                    MSQLC.ExecuteNonQuery();
                    conectar.Cerrar_Conexion();
                    txbGrado.Text = "";
                    txbEspecialidad.Text = "";
                    txbGrupo.Text = "";
                    txbMateria.Text = "";
                    txbDescripcion.Text = "";
                    
                    int cantidad = 0;
                    conectar.Crear_Conexion();
                    // Se obtiene la cantidad de alumnos
                    string selecciona = "SELECT count(*) FROM `alumnos` WHERE `grupo_idgrupo`=" + idGrupo + ";";
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    MSQLDR = MSQLC.ExecuteReader();
                    if (MSQLDR.Read() == true)
                    {
                        cantidad = Convert.ToInt32(MSQLDR["count(*)"]);
                    }
                    conectar.Cerrar_Conexion();
                    int[] Ids_Alumnos = new int[cantidad];
                    // Se obtiene los ids de los alumnos
                    selecciona = "SELECT min(`matricula`) FROM `alumnos` WHERE `grupo_idgrupo`=" + idGrupo + ";";
                    for (int i = 0; i < cantidad; i++)
                    {
                        conectar.Crear_Conexion();
                        MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                        MSQLDR = MSQLC.ExecuteReader();
                        if (MSQLDR.Read() == true)
                        {
                            Ids_Alumnos[i] = Convert.ToInt32(MSQLDR["min(`matricula`)"]);
                        }
                        conectar.Cerrar_Conexion();
                        selecciona = selecciona.Insert(selecciona.Length - 1, " and `matricula`!=" + Ids_Alumnos[i]);
                    }

                    // Se obtiene el id del grupo
                    int idMateria = 0;
                    selecciona = "SELECT max(`idmateria`) FROM `materia` WHERE `grupo_idgrupo`=" + idGrupo + ";";
                    conectar.Crear_Conexion();
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    MSQLDR = MSQLC.ExecuteReader();
                    if (MSQLDR.Read() == true)
                    {
                        idMateria = Convert.ToInt32(MSQLDR["max(`idmateria`)"]);
                    }
                    conectar.Cerrar_Conexion();



                    // Se agregan el primer parcial de la materia a los alumnos
                    for (int i = 0; i < cantidad; i++)
                    {
                        conectar.Crear_Conexion();
                        insertar = "INSERT INTO `parcial`(`alumnos_matricula`, `materia_idmateria`, `numero`, `calificacion`) VALUES ("
                            + Ids_Alumnos[i].ToString() + "," + idMateria + "," + 1 + "," + 0 + ")";
                        MSQLC = new MySqlCommand(insertar);
                        MSQLC.Connection = conectar.GetConexion();
                        MSQLC.ExecuteNonQuery();
                        conectar.Cerrar_Conexion();
                    }



                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("Se ha ingresado satisfactoriamente", "Éxito", MessageBoxButtons.OK, RadMessageIcon.Info);
                }
                catch (System.InvalidOperationException)
                {
                    RadMessageBox.SetThemeName(this.ThemeName);
                    RadMessageBox.Show("Error con base de datos", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
                Agregar_grupo_Load(sender, e);
            }
            else
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("Ingrese correctamente la materia y/o descripción", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            }
        }
    }
}