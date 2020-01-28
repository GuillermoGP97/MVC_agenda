using MVC_agenda.Models;
using MVC_agenda.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_agenda.Controllers
{
    public class HomeController : Controller
    {
        private AgendaRepository _repocitasdia;
        private AgendaMuestra _agenda;
        private int _intervalo, _columnas;

        public HomeController ()
        {
            _repocitasdia = new AgendaRepository();
            _agenda = new AgendaMuestra();
            _intervalo = 10;
            _columnas = 1;
        }

        public ActionResult Index()
        {
            _agenda.horas = RellenaHoras();
            _agenda.listadecolumnas = new List<List<CitaDatos>>();
            _agenda.columna1 = InicializaLista();
            _agenda.columna2 = InicializaLista();
            _agenda.columna3 = InicializaLista();
            _agenda.columna4 = InicializaLista();
            _agenda.listadecolumnas.Add(_agenda.columna1);
            _agenda.listadecolumnas.Add(_agenda.columna2);

            RellenaCitas();
            // Pone las columnas
            _agenda.ncolumnas = _columnas;
            switch (_columnas)
            {
                case 2:
                    _agenda.classcolumna = "col-md-6";
                    break;
                case 3:
                    _agenda.classcolumna = "col-md-4";
                    break;
                case 4:
                    _agenda.classcolumna = "col-md-3";
                    break;
                default:
                    _agenda.classcolumna = "col-md-12";
                    break;
            }

            return View(_agenda);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public List<Horario> RellenaHoras()
        {
            List<Horario> lista = new List<Horario>();
            DateTime temp = Convert.ToDateTime("01/01/0001 10:00:00");

            for (int i = 0; i < 48; i += 1)
            {
                var elemento = new Horario()
                {
                    hora = temp,
                    horastr = temp.ToString("HH:mm")
                };
                lista.Add(elemento);
                temp = temp.AddMinutes(_intervalo);
            }

            return lista;
        }

        public List<CitaDatos> InicializaLista()
        {
            List<CitaDatos> lista = new List<CitaDatos>();
            DateTime temp = Convert.ToDateTime("01/01/0001 10:00:00");

            // Crea la lista de citas vacias para rellenar
            for (int i = 0; i < 24; i += 1)
            {
                var elemento = new CitaDatos()
                {
                    hora = temp,
                    horastr = temp.ToString("HH:mm"),
                    estado = "L",
                    tramtamiento = "",
                    nombre = "",
                    duracion = 0,
                    objcalse = "cita-vacia",
                    objalto = "25px"
                };
                lista.Add(elemento);
                temp = temp.AddMinutes(_intervalo);
            }

            return lista;
        }

        public void RellenaColumnaCita(CitaDatos pr_cita, agenda pr_datos, List<CitaDatos> pr_columna)
        {
            int s_partes;

            pr_cita.tramtamiento = pr_datos.tratamiento;
            pr_cita.nombre = pr_datos.nombre;
            pr_cita.objcalse = "citas";
            pr_cita.duracion = pr_datos.duracion;
            s_partes = pr_cita.duracion / _intervalo;
            pr_cita.objalto = (25 * s_partes).ToString() + "px";

            // Reserva las siguiente
            for (int j = 1; j < s_partes; j++)
            {
                var s_vacia = pr_columna.Find(x => x.hora == pr_datos.horafecha.AddMinutes(_intervalo * j));
                if (s_vacia != null)
                {
                    s_vacia.tramtamiento = "--";
                    s_vacia.nombre = pr_datos.nombre;
                    s_vacia.objcalse = "ocupada";
                    s_vacia.duracion = _intervalo;
                }
            }
        }

        public void RellenaCitas()
        {
            int s_partes;

            // Revisa las horas para ver si hay citas
            List<agenda> s_citas = _repocitasdia.ListaCitasDia();

            //foreach (var elemento in lista)
            //{
            //    var s_res = s_citas.Find(x => x.hora.Equals(elemento.horastr));
            //    if (s_res != null)
            //    {
            //        elemento.tramtamiento = s_res.tratamiento;
            //        elemento.nombre = s_res.nombre;
            //        elemento.objcalse = "citas";
            //        elemento.duracion = s_res.duracion;
            //        s_partes = s_res.duracion / s_intervalo;
            //        elemento.objalto = (25 * s_partes).ToString() + "px";
            //    }
            //}

            //for (int i = 0; i < lista.Count(); i++)
            //{
            //    var s_res = s_citas.Find(x => x.horastr.Equals(lista[i].horastr));
            //    if (s_res != null)
            //    {
            //        lista[i].tramtamiento = s_res.tratamiento;
            //        lista[i].nombre = s_res.nombre;
            //        lista[i].objcalse = "citas";
            //        lista[i].duracion = s_res.duracion;
            //        s_partes = s_res.duracion / s_intervalo;
            //        // lista[i].objalto = (25 * s_partes).ToString() + "px";
            //        // Rellena las zonas que ocupa la cita
            //    }
            //}

            for (int i = 0; i < s_citas.Count(); i++)
            {
                var s_res1 = _agenda.columna1.Find(x => x.hora == s_citas[i].horafecha);
                if (s_res1 != null)
                {
                    if (s_res1.objcalse == "cita-vacia")
                    {
                        RellenaColumnaCita(s_res1, s_citas[i], _agenda.columna1);
                        if (_columnas < 1)
                            _columnas = 1;
                    }
                    else
                    {
                        var s_res2 = _agenda.columna2.Find(x => x.hora == s_citas[i].horafecha);
                        if (s_res2 != null)
                        { 
                            if (s_res2.objcalse == "cita-vacia")
                            {
                                RellenaColumnaCita(s_res2, s_citas[i],_agenda.columna2);
                                if (_columnas < 2)
                                    _columnas = 2;
                            }
                            else
                            {
                                var s_res3 = _agenda.columna3.Find(x => x.hora == s_citas[i].horafecha);
                                if (s_res3 != null)
                                {
                                    if (s_res3.objcalse == "cita-vacia")
                                    {
                                        RellenaColumnaCita(s_res3, s_citas[i], _agenda.columna3);
                                        if (_columnas < 3)
                                            _columnas = 3;
                                    }
                                    else
                                    {
                                        var s_res4 = _agenda.columna4.Find(x => x.hora == s_citas[i].horafecha);
                                        if (s_res4 != null)
                                        {
                                            if (s_res4.objcalse == "cita-vacia")
                                            {
                                                RellenaColumnaCita(s_res4, s_citas[i], _agenda.columna3);
                                                if (_columnas < 4)
                                                    _columnas = 4;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }



            //// Revisa cada cita y ve si se puede hay otras en el mismo rango de fechas.
            //for (int i=0;i < s_citas.Count(); i++)
            //{
            //    // Mira si alguna de la siguienes esta en el mismo rango
            //    for (int j=i+1;j < s_citas.Count();j++)
            //    {
            //        if ((s_citas[j].horafecha >= s_citas[i].horafecha && s_citas[j].horafecha <= s_citas[i].horafecha.AddMinutes(s_citas[i].duracion)) ||
            //            (s_citas[j].horafecha.AddMinutes(s_citas[j].duracion) >= s_citas[i].horafecha && s_citas[j].horafecha.AddMinutes(s_citas[j].duracion) <= s_citas[i].horafecha.AddMinutes(s_citas[i].duracion)))
            //        {
            //            break;
            //        }
            //    }
            //}

            return;
        }
    }
}