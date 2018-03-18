using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_Course.Models
{
    public class SubDireccion
    {
        public int SubDireccionId { get; set; }

        [StringLength(124)]
        public string SubCalle { get; set; }

        public int Numero { get; set; }
        public virtual Direccion Direccion { get; set; }
    }
}