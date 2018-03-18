using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EF_Relations.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
            :base("DefaultConnection")
        {

        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<TarjetaDeCredito> Tarjetas { get; set; }
        public DbSet<Persona_Curso> Persona_Curso { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().HasKey(p => p.Cedula);
            modelBuilder.Entity<Direccion>().HasKey(d => d.CodigoDireccion);
            modelBuilder.Entity<Curso>().HasKey(c => c.CursoId);
            modelBuilder.Entity<TarjetaDeCredito>().HasKey(t => t.TCreditoId);

            //Toda direcccion tiene una persona, no toda persona tiene una direccion
            //modelBuilder.Entity<Direccion>().HasRequired(x => x.Persona).WithOptional(d => d.Direccion);

            //Toda direccion se relaciona con una persona, toda persona tiene una direccion
            modelBuilder.Entity<Direccion>().HasRequired(d => d.Persona).WithRequiredPrincipal(p => p.Direccion);

            //Toda tarjeta tiene una persona, no siempre una persona tiene tarjeta
            modelBuilder.Entity<TarjetaDeCredito>().HasRequired(p => p.Persona);

            //Todo curso se relaciona con varias personas, toda persona se relacion varios cursos
            //modelBuilder.Entity<Curso>().HasMany(x => x.Personas).WithMany(p => p.Cursos);

            //modelBuilder.Entity<Curso>().HasMany(x => x.Personas).WithMany(p => p.Cursos)
            //    .Map(m =>
            //    {
            //        m.ToTable("Persona_Curso");
            //        m.MapLeftKey("CursoId");
            //        m.MapRightKey("PersonaId");
            //    }
            //    );


            modelBuilder.Entity<Persona_Curso>().HasKey(pc => new {pc.PersonaId,pc.CursoId });

            base.OnModelCreating(modelBuilder);
        }

    }
}