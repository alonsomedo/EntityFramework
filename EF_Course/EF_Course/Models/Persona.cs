using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EF_Course.Models
{
    public class Persona
    {
        public int Id { get; set; }
        [StringLength(120)]
        public string Nombre { get; set; }

        [Index("Ix_Nacimiento_Edad",Order =1)]
        public int Edad { get; set; }

        [Index("Ix_Nacimiento_Edad", Order = 2)]
        public DateTime Nacimiento { get; set; }

        public Sexo Sexo { get; set; }

        public string   Resumen { get; set; }

        public virtual List<Direccion> Direcciones { get; set; }
    }
}