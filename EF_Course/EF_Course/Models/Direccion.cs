using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_Course.Models
{
    public class Direccion
    {
        public int CodigoDireccion { get; set; }
        public string Calle { get; set; }

        public int PersonaId { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual List<SubDireccion> SubDirecciones { get; set; }
    }
}