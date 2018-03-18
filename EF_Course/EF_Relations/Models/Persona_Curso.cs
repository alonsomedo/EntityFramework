using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_Relations.Models
{
    public class Persona_Curso
    {
        public string PersonaId { get; set; }
        public int CursoId { get; set; }

        public virtual Persona Persona { get; set; }
        public virtual Curso Curso { get; set; }
        public bool Abandonado { get; set; }
    }
}