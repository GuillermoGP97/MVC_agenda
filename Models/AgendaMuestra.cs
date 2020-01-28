using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_agenda.Models
{
    public class AgendaMuestra
    {
        public DateTime dia { get; set; }
        public List<Horario> horas { get; set; }
        public List<CitaDatos> columna1 { get; set; }
        public List<CitaDatos> columna2 { get; set; }
        public List<CitaDatos> columna3 { get; set; }
        public List<CitaDatos> columna4 { get; set; }
        public List<CitaDatos> columna5 { get; set; }
        public List<List<CitaDatos>> listadecolumnas { get; set; }

        public List<Horario> GetHoras()
        {
            return horas;
        }
        public int ncolumnas { get; set; }
        public string classcolumna { get; set; }
    }
}