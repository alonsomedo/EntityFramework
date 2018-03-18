using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_Relations.Models
{
    public class Direccion
    {
        public int CodigoDireccion { get; set; }

        public string Calle { get; set; }
        public virtual Persona Persona { get; set; }
    }
}