using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_Relations.Models
{
    public class Curso
    {

        public Curso()
        {
            //Personas = new List<Persona>();
            Persona_Curso = new List<Persona_Curso>();
        }
        public int CursoId { get; set; }
        public string Descripcion { get; set; }

       // public virtual List<Persona> Personas { get; set; }
        public virtual List<Persona_Curso> Persona_Curso { get; set; }
    }
}