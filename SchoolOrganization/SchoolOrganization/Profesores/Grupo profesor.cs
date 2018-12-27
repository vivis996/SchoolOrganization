using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SchoolOrganization
{
    public partial class Grupo_profesor : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;
        private MySqlDataAdapter MSQLDA;

        DataSet tht = new DataSet();
        BindingSource bindin = new BindingSource(); // enlace para los controles
        int cant_parciales = 0;
        double[,] calific;
        double[] prom;


        public Grupo_profesor()
        {
            InitializeComponent();
        }

        private void Grupo_profesor_Load(object sender, EventArgs e)
        {
            conectar.Crear_Conexion();
            string selecciona = "SELECT matricula,nombres,ape_pa,ape_ma FROM alumnos where grupo_idgrupo=" + Variables.Idgrupo.ToString() + ";";
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            tht.Clear();
            txbMatricula.Text = "";
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
            dgvAlumnos.Columns[0].HeaderText = "Matricula";
            dgvAlumnos.Columns[1].HeaderText = "Nombre";
            dgvAlumnos.Columns[2].HeaderText = "Apellido paterno";
            dgvAlumnos.Columns[3].HeaderText = "Apellido materno";
            conectar.Cerrar_Conexion();
            conectar.Crear_Conexion();
            selecciona = "SELECT cantidad FROM `materia` WHERE `idmateria`=" + Variables.IdMateria.ToString();
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDR = MSQLC.ExecuteReader();
            if (MSQLDR.Read() == true)
            {
                cant_parciales = Convert.ToInt32(MSQLDR["cantidad"].ToString());
            }
            conectar.Cerrar_Conexion();
            for (int i = 0; i < cant_parciales; i++)
            {
                dgvAlumnos.Columns.Add("cParcial" + (i + 1), "Parcial " + (i + 1));
                if (i == cant_parciales - 1)
                {
                    dgvAlumnos.Columns.Add("cPromedio", "Promedio");
                }
            }
            calific = new double[dgvAlumnos.Rows.Count, cant_parciales];
            prom = new double[dgvAlumnos.Rows.Count];
            for (int i = 0; i < dgvAlumnos.Rows.Count; i++)
            {
                double promedio = 0;
                for (int j = 0; j < cant_parciales; j++)
                {
                    double calificacion = 0;
                    conectar.Crear_Conexion();
                    selecciona = "SELECT `calificacion` FROM `parcial` WHERE `alumnos_matricula`=" + dgvAlumnos.Rows[i].Cells[0].Value + 
                        " and `materia_idmateria`=" + Variables.IdMateria.ToString() + " and `numero`=" + (j+1);
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    MSQLDR = MSQLC.ExecuteReader();
                    if (MSQLDR.Read() == true)
                    {
                        calificacion = Convert.ToDouble(MSQLDR["calificacion"].ToString());
                        calific[i, j] = calificacion;
                    }
                    conectar.Cerrar_Conexion();
                    promedio += calificacion;
                    dgvAlumnos.Rows[i].Cells[j + 4].Value = calificacion;
                }
                promedio = promedio / cant_parciales;
                prom[i] = promedio;
                dgvAlumnos.Rows[i].Cells["cPromedio"].Value = promedio.ToString("#.#");
            }
            chart1.Legends[0].ForeColor = Color.White;

            this.chart1.ChartAreas[0].AxisX.LineColor = Color.Red;
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Red;
            this.chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;

            this.chart1.ChartAreas[0].AxisY.LineColor = Color.Red;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Red;
            this.chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

            chart1.Series.Clear();
            chart1.Titles.Clear();
            this.chart1.Palette = ChartColorPalette.Pastel;
            // Para el título
            this.chart1.Titles.Add("Calificaciones");
            this.chart1.Titles[0].ForeColor = Color.White;
            // Agregando las series
            for (int i = 0; i < dgvAlumnos.Rows.Count; i++)
            {
                // Agregando series
                Series series = this.chart1.Series.Add(dgvAlumnos.Rows[i].Cells[3].Value.ToString());
                series.Points.Add(Convert.ToInt32(dgvAlumnos.Rows[i].Cells[cant_parciales + 3].Value));
            }
        }

        private void btnAlumno_Click(object sender, EventArgs e)
        {
            try
            {
                Variables.IdAlumno = Convert.ToInt32(txbMatricula.Text);
            }
            catch { }
            this.Hide();
            Calificacion_alumno calificar = new Calificacion_alumno();
            calificar.ShowDialog();
            Limpiar();
            Grupo_profesor_Load(sender, e);
            this.Show();
        }

        private void btnParcial_Click(object sender, EventArgs e)
        {
            this.Hide();
            Parciales parcial = new Parciales();
            parcial.ShowDialog();
            Limpiar();
            Grupo_profesor_Load(sender, e);
            this.Show();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvAlumnos.RowCount; i++)
            {
                for (int j = 0; j < cant_parciales; j++)
                {
                    string selecciona = "UPDATE `parcial` SET `calificacion`=" + dgvAlumnos.Rows[i].Cells[j + 4].Value +
                    " WHERE `alumnos_matricula`=" + dgvAlumnos.Rows[i].Cells[0].Value
                    + " and `materia_idmateria`=" + Variables.IdMateria.ToString() +
                    " and `numero`=" + (j + 1) + ";";
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    conectar.Crear_Conexion();
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    MSQLC.Connection = conectar.GetConexion();
                    MSQLC.ExecuteNonQuery();
                    conectar.Cerrar_Conexion();
                }
            }
            Limpiar();
            Grupo_profesor_Load(sender, e);
            RadMessageBox.SetThemeName(this.ThemeName);
            RadMessageBox.Show("Se ha modificado la información", "Éxito", MessageBoxButtons.OK, RadMessageIcon.Info);
        }
        private void Limpiar()
        {
            for (int i = 0; i < cant_parciales + 1; i++)
            {
                int cant = dgvAlumnos.Columns.Count;
                dgvAlumnos.Columns.RemoveAt(4);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            try
            {
                //Se crea archivo con un tamaño y margen
                Document doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(Variables.Nombre.ToString() + " Boleta.pdf", FileMode.Create));
                doc.Open();

                iTextSharp.text.Font Normal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Negrita = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font Cursiva = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);
                iTextSharp.text.Font Titulo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.ITALIC + iTextSharp.text.Font.BOLD, BaseColor.BLACK);


                Paragraph paragraph = new Paragraph("Secretaria de Ecucación de Quintana Roo\nDirección de Educación Superior y Capacitación para el Trabajo\nDepartamento de Educación Superior", Normal);
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);

                paragraph = new Paragraph("Universidad Politécnica de Quintana Roo", Titulo);
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);

                paragraph = new Paragraph("\"Conocimiento, trabajo y virtud\"", Negrita);
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);

                paragraph = new Paragraph("Av. Arco Bincentenario, Mza. 11, Lote 1119-33, Sm. 255\nCancún, Quintana Roo, México C.P. 77500\nTel. y Fax (998) 283 1859", Cursiva);
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                paragraph = new Paragraph("Bolete parcial de calificaciones", Negrita);
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                paragraph = new Paragraph("Nombre del profesor(a): ", Negrita);
                paragraph.Alignment = Element.ALIGN_LEFT;
                doc.Add(paragraph);
                paragraph = new Paragraph(Variables.Nombre, Normal);
                paragraph.Alignment = Element.ALIGN_LEFT;
                doc.Add(paragraph);

                //Imagen
                iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance("Logo.png");
                PNG.ScalePercent(10f);
                PNG.Alignment = Element.ALIGN_LEFT;
                PNG.SetAbsolutePosition(20f, doc.PageSize.Height - 100f);
                doc.Add(PNG);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                //Crear tabla con el tamaño de columnas del Data Grid View
                PdfPTable tabla = new PdfPTable(dgvAlumnos.Columns.Count - 2);

                //Se agregan los titulos de la tabla
                PdfPCell celda = new PdfPCell(new Phrase("Alumnos", Negrita));
                celda.Colspan = 2;
                celda.HorizontalAlignment = 1;
                tabla.AddCell(celda);

                PdfPCell celda2 = new PdfPCell(new Phrase("Parciales ordinarios", Negrita));
                celda2.Colspan = dgvAlumnos.Columns.Count - 4;
                celda2.HorizontalAlignment = 1;
                tabla.AddCell(celda2);

                for (int i = 0; i < dgvAlumnos.Columns.Count - 4; i++)
                {
                    string titulo = dgvAlumnos.Columns[i].HeaderText;
                    if (i == 2)
                    {
                        for (int j = 0; j < dgvAlumnos.Columns.Count - 4; j++)
                        {
                            if (j == dgvAlumnos.Columns.Count - 5)
                            {
                                titulo = "Pro.";
                                tabla.AddCell(new Phrase(titulo, Negrita));
                            }
                            else
                                tabla.AddCell(new Phrase((j + 1).ToString(), Negrita));
                        }
                        break;
                    }
                    else
                    {
                        if (i == 1)
                            titulo = "Nombre";
                        tabla.AddCell(new Phrase(titulo, Negrita));
                    }
                }
                tabla.HeaderRows = 3;
                List lista = new List(List.UNORDERED, 20f);
                //Se agrega el contenido en la tabla
                for (int i = 0; i < dgvAlumnos.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvAlumnos.Columns.Count - 4; j++)
                    {
                        if (dgvAlumnos[j, i].Value != null)
                        {
                            string titulo = dgvAlumnos[j, i].Value.ToString();
                            
                            if (j == 1)
                                titulo = dgvAlumnos[1, i].Value.ToString() + " "
                                                + dgvAlumnos[2, i].Value.ToString() + " "
                                                + dgvAlumnos[3, i].Value.ToString();
                            if (j == 2)
                            {
                                for (int k = 0; k < dgvAlumnos.Columns.Count - 4; k++)
                                {
                                    try
                                    {
                                        double calificacion = Convert.ToDouble(dgvAlumnos[k + 4, i].Value.ToString());
                                        titulo = calificacion.ToString();
                                        if (k == 2)
                                        {
                                            if (calificacion < 7)
                                                lista.Add(dgvAlumnos[1, i].Value.ToString() + " "
                                                    + dgvAlumnos[2, i].Value.ToString() + " "
                                                    + dgvAlumnos[3, i].Value.ToString());
                                        }
                                    }
                                    catch { }
                                    tabla.AddCell(new Phrase(titulo, Normal));
                                }
                                break;
                            }
                            tabla.AddCell(new Phrase(titulo, Normal));
                        }
                        else
                        {
                            tabla.AddCell(new Phrase("0", Normal));
                        }
                    }
                }
                tabla.HorizontalAlignment = 1;
                doc.Add(tabla);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                if (lista.Items.Count > 0)
                {
                    paragraph = new Paragraph("Alumnos que requieren atención:\n");
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    lista.IndentationLeft = 20f;
                    doc.Add(paragraph);
                    doc.Add(lista);
                }
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                paragraph = new Paragraph("___________________________\nFirma del director\n" + DateTime.Today.ToString("dd/MM/yyyy"));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                doc.Add(paragraph);
                doc.Close();
                System.Diagnostics.Process.Start(Variables.Nombre.ToString() + " Boleta.pdf");
            }
            catch (IOException)
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("El archivo está siendo utilizado\nCierre para guardar el nuevo archivo", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            }
            btnSalvar.Enabled = true;
        }
    }
}