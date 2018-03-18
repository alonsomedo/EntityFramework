using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EF_Relations.Models
{
    public class Persona
    {
        public Persona()
        {
            //Cursos = new List<Curso>();
            Persona_Curso = new List<Persona_Curso>();
        }

        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public DateTime Nacimiento { get; set; }
        public int Edad { get; set; }
        public Sexo Sexo { get; set; }
        public decimal Salario { get; set; }
        [NotMapped]
        public string Resumen { get; set; }
        public Direccion Direccion { get; set; }
        public virtual List<TarjetaDeCredito> Tarjetas { get; set; }
        //public virtual List<Curso> Cursos { get; set; }
        public virtual List<Persona_Curso> Persona_Curso { get; set; }
    }
}