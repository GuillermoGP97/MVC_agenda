using MVC_agenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_agenda.Services
{
    public class AgendaRepository
    {
        public List<agenda> ListaCitasDia()
        {
            using (var db = new AgendaDbContext())
            {
                string s_fecha = DateTime.Today.ToString("yyyy-mm-dd");
                string s_select = "select * from agenda order by fecha, hora;";

                List<agenda> s_lista=db.Database.SqlQuery<agenda>(s_select).ToList();
                foreach (var elemento in s_lista)
                {
                    elemento.horafecha = Convert.ToDateTime("01/01/0001 " +elemento.hora.ToString(@"hh\:mm\:ss"));
                    elemento.horastr = elemento.hora.ToString(@"hh\:mm");
                }

                return s_lista;
                
            }
        }
    }
}