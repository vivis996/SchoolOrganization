using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization
{
    class Variables
    {
        static int matricula;
        static string nombre;
        static int idgrupo;
        static int idAlumno;
        static int idMateria;

        public static int IdMateria
        {
            get { return Variables.idMateria; }
            set { Variables.idMateria = value; }
        }

        public static int IdAlumno
        {
            get { return Variables.idAlumno; }
            set { Variables.idAlumno = value; }
        }
        public static int Idgrupo
        {
            get { return Variables.idgrupo; }
            set { Variables.idgrupo = value; }
        }

        public static string Nombre
        {
            get { return Variables.nombre; }
            set { Variables.nombre = value; }
        }

        public static int Matricula
        {
            get { return Variables.matricula; }
            set { Variables.matricula = value; }
        }
        public int Calcular_Edad(int dia, int mes, int añoedad)
        {
            int dia1 = 0, dia2 = 0, mes1 = 0, mes2 = 0, año1 = 0, año2 = 0, año = 0;
            try
            {
                año1 = añoedad;
                año2 = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                mes1 = mes;
                mes2 = Convert.ToInt32(DateTime.Now.ToString("MM"));
                dia1 = dia;
                dia2 = Convert.ToInt32(DateTime.Now.ToString("dd"));
                año = año2 - año1;
                if (mes1 > mes2)
                {
                    año--;
                }
                else
                {
                    if (mes1 == 1 || mes2 == 1)
                    {
                    }
                    else
                    {
                        if (dia1 > dia2 && mes1 > mes2)
                        {
                            año--;
                        }
                    }
                }
            }
            catch
            {

            }
            return año;
        }
    }
}