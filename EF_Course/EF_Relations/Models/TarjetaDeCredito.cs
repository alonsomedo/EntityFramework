using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_Relations.Models
{
    public class TarjetaDeCredito
    {
        [Key]
        public int TCreditoId { get; set; }
        public string Numero { get; set; }
        public virtual Persona Persona { get; set; }
    }
}