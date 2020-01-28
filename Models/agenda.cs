using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_agenda.Models
{
    public class agenda
    {
        public int id { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }
        [Required] 
        public TimeSpan hora { get; set; }
        [NotMapped]
        public DateTime horafecha { get; set; }
        [NotMapped]
        public string horastr { get; set; }
        [Required]
        public int duracion { get; set; }
        [StringLength(300)]
        [Required]
        public string tratamiento { get; set; }
        [StringLength(60)]
        public string nombre { get; set; }
        [StringLength(25)]
        public string telefono { get; set; }
        [Required]
        public short departamento { get; set; }
        [StringLength(1)]
        [Required]
        public string estado { get; set; }
        [Required]
        public DateTime esfecha { get; set; }
        [Required]
        public short usuario { get; set; }
        [StringLength(15)]
        public string reg_estado { get; set; }
        public DateTime? reg_fecha { get; set; }
        public short? reg_usuario { get; set; }
    }
}