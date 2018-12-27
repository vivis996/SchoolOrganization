using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SchoolOrganization
{
    public partial class Ver_calificaciones : Telerik.WinControls.UI.RadForm
    {
        private MyConection conectar = new MyConection();

        private MySqlCommand MSQLC;
        private MySqlDataReader MSQLDR;

        int cant_materia = 0, max = 0;
        string[] nombre;
        int[] idsmateria, cant_parciales;
        double[] prom;
        double[,] califi;
        public Ver_calificaciones()
        {
            InitializeComponent();
        }

        private void Ver_calificaciones_Load(object sender, EventArgs e)
        {
            conectar.Crear_Conexion();
            string selecciona = "SELECT count(*) FROM materia where grupo_idgrupo=" + Variables.Idgrupo.ToString();
            MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
            MSQLDR = MSQLC.ExecuteReader();
            if (MSQLDR.Read() == true)
            {
                cant_materia = Convert.ToInt32(MSQLDR["count(*)"].ToString());
            }
            conectar.Cerrar_Conexion();
            nombre = new string[cant_materia];
            idsmateria = new int[cant_materia];
            cant_parciales = new int[cant_materia];
            string selec_ids = "SELECT min(`idmateria`) FROM materia where grupo_idgrupo=" + Variables.Idgrupo.ToString();

            for (int i = 0; i < cant_materia; i++)
            {
                conectar.Crear_Conexion();
                MSQLC = new MySqlCommand(selec_ids, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read() == true)
                {
                    idsmateria[i] = Convert.ToInt32(MSQLDR["min(`idmateria`)"].ToString());
                }
                conectar.Cerrar_Conexion();
                conectar.Crear_Conexion();
                selec_ids = selec_ids + " and idmateria!=" + idsmateria[i].ToString();
                selecciona = "SELECT `nombre_materia`,cantidad FROM `materia` WHERE `idmateria`=" + idsmateria[i].ToString();
                MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                MSQLDR = MSQLC.ExecuteReader();
                if (MSQLDR.Read() == true)
                {
                    nombre[i] = MSQLDR["nombre_materia"].ToString();
                    cant_parciales[i] = Convert.ToInt32(MSQLDR["cantidad"].ToString());
                    if (cant_parciales[i] > max)
                        max = cant_parciales[i];
                }
                conectar.Cerrar_Conexion();
            }
            for (int i = 0; i < max; i++)
            {
                dgvCalificacion.Columns.Add("c" + (1 + i), "Parcial " + (i + 1).ToString());
                if (i == max - 1)
                {
                    dgvCalificacion.Columns.Add("cPromedio", "Promedio");
                }
            }
            prom = new double[cant_materia];
            califi = new double[cant_materia, max];
            for (int i = 0; i < cant_materia; i++)
            {
                double promedio = 0;
                dgvCalificacion.Rows.Add(nombre[i]);
                for (int j = 0; j < cant_parciales[i]; j++)
                {
                    double calificacion = 0;
                    conectar.Crear_Conexion();
                    selecciona = "SELECT `calificacion` FROM `parcial` WHERE `materia_idmateria`=" + idsmateria[i].ToString()
                        + " and `alumnos_matricula`=" + Variables.Matricula.ToString() + " and `numero`=" + (j + 1).ToString();
                    MSQLC = new MySqlCommand(selecciona, conectar.GetConexion());
                    MSQLDR = MSQLC.ExecuteReader();
                    if (MSQLDR.Read() == true)
                    {
                        calificacion = Convert.ToDouble(MSQLDR["calificacion"].ToString());
                        califi[i, j] = calificacion;
                    }
                    conectar.Cerrar_Conexion();
                    promedio += calificacion;
                    dgvCalificacion.Rows[i].Cells[j + 1].Value = calificacion;
                }
                promedio = promedio / cant_parciales[i];
                prom[i] = promedio;
                dgvCalificacion.Rows[i].Cells["cPromedio"].Value = promedio.ToString("#.#");
            }

            // Colores
            chart1.Legends[0].ForeColor = Color.White;

            this.chart1.ChartAreas[0].AxisX.LineColor = Color.Red;
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Red;
            this.chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;

            this.chart1.ChartAreas[0].AxisY.LineColor = Color.Red;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Red;
            this.chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;


            this.chart1.Palette = ChartColorPalette.Pastel;
            // Para el título
            this.chart1.Titles.Add("Calificaciones");
            this.chart1.Titles[0].ForeColor = Color.White;
            // Agregando las series
            for (int i = 0; i < nombre.Length; i++)
            {
                // Agregando series
                Series series = this.chart1.Series.Add(nombre[i]);
                series.Points.Add(prom[i]);
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


                Paragraph paragraph = new Paragraph("Secretaria de Ecucación de Quintana Roo\nDirección de Educación Superior y Capacitación para el Trabajo\nDepartamento de Educación Superior",Normal);
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

                paragraph = new Paragraph("Nombre del alumno(a): ", Negrita);
                paragraph.Alignment = Element.ALIGN_LEFT;
                doc.Add(paragraph);
                paragraph = new Paragraph(Variables.Nombre, Normal);
                paragraph.Alignment = Element.ALIGN_LEFT;
                doc.Add(paragraph);

                //Imagen
                iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance("Logo.png");
                PNG.ScalePercent(10f);
                PNG.Alignment = Element.ALIGN_LEFT;
                PNG.SetAbsolutePosition(20f,doc.PageSize.Height -100f);
                doc.Add(PNG);

                doc.Add(Chunk.NEWLINE);

                //Crear tabla con el tamaño de columnas del Data Grid View
                PdfPTable tabla = new PdfPTable(dgvCalificacion.Columns.Count);

                //Se agregan los titulos de la tabla
                PdfPCell celda = new PdfPCell(new Phrase("Materia",Negrita));
                celda.Colspan = 1;
                celda.HorizontalAlignment = 1;
                tabla.AddCell(celda);

                PdfPCell celda2 = new PdfPCell(new Phrase("Parciales ordinarios",Negrita));
                celda2.Colspan = dgvCalificacion.Columns.Count - 1;
                celda2.HorizontalAlignment = 1;
                tabla.AddCell(celda2);

                for (int i = 0; i < dgvCalificacion.Columns.Count; i++)
                {
                    string titulo= dgvCalificacion.Columns[i].HeaderText;
                    string otro = "Parcial " + i.ToString();
                    if (titulo == otro)
                    {
                        tabla.AddCell(new Phrase(i.ToString(), Negrita));
                    }
                    else
                    {
                        if (dgvCalificacion.Columns[i].HeaderText == "Promedio")
                            titulo = "Pro.";

                        if (dgvCalificacion.Columns[i].HeaderText == "Materia")
                            titulo = "";
                        tabla.AddCell(new Phrase(titulo, Negrita));
                    }
                }
                tabla.HeaderRows = 3;
                List lista = new List(List.UNORDERED, 20f);
                //Se agrega el contenido en la tabla
                for (int i = 0; i < dgvCalificacion.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvCalificacion.Columns.Count; j++)
                    {
                        if (dgvCalificacion[j, i].Value != null)
                        {
                            try
                            {
                                if (j == dgvCalificacion.Columns.Count -1)
                                {
                                    double calificacion = Convert.ToDouble(dgvCalificacion[dgvCalificacion.Columns.Count -1, i].Value.ToString());
                                    if (calificacion < 7)
                                        lista.Add(dgvCalificacion[0, i].Value.ToString());
                                }
                            }
                            catch { }
                            tabla.AddCell(new Phrase(dgvCalificacion[j, i].Value.ToString(), Normal));
                        }
                        else
                        {
                            tabla.AddCell(new Phrase("---", Normal));
                        }
                    }
                }
                tabla.HorizontalAlignment = 1;
                doc.Add(tabla);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                if (lista.Items.Count > 0)
                {
                    paragraph = new Paragraph("Materias que requieren atención:\n");
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    lista.IndentationLeft = 20f;
                    doc.Add(paragraph);
                    doc.Add(lista);
                }
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                paragraph = new Paragraph("      Firma del padre o tutor\n\n");
                paragraph.Alignment = Element.ALIGN_LEFT;
                doc.Add(paragraph);
                //Crear tabla con el tamaño de columnas del Data Grid View
                PdfPTable firmas = new PdfPTable(2);

                int can_parciales = dgvCalificacion.Columns.Count - 2;
                if ((can_parciales % 2) == 1)
                    can_parciales++;
                for (int i = 0; i < can_parciales; i++)
                {
                    if (can_parciales > dgvCalificacion.Columns.Count - 2 && i == can_parciales - 1)
                        firmas.AddCell(new Phrase(" ", Normal));                    
                    firmas.AddCell(new Phrase("Parcial " + (i + 1).ToString() + "         ", Normal));
                }
                firmas.HorizontalAlignment = 1;
                doc.Add(firmas);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                paragraph = new Paragraph("___________________________\nFirma del director\n" + DateTime.Today.ToString("dd/MM/yyyy"));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                doc.Add(paragraph);
                doc.Close();
                System.Diagnostics.Process.Start(Variables.Nombre.ToString() + " Boleta.pdf");
            }
            catch(IOException)
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show("El archivo está siendo utilizado\nCierre para guardar el nuevo archivo", "Error", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            }
            btnSalvar.Enabled = true;
        }
    }
}