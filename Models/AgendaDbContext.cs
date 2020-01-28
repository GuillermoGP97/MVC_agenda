using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_agenda.Models
{
    // Para que funcione code first de EntityFramework
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    // Creamos el dbcontext para  el acceso a la base de datos
    public class AgendaDbContext: DbContext
    {
        public AgendaDbContext() : base("MySqlconexion")
        {
            // Se ponen las operaciones a realizar
        }
        // Definimos las datos a los que vamos a tener acceso desde EntityFramework
        public DbSet<agenda> peliculas { get; set; }
    }
}