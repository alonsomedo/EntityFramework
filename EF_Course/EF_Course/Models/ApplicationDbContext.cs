using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace EF_Course.Models
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext()
            :base("DefaultConnection")
        {

        }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<SubDireccion> SubDireccion { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Persona>().ToTable("Personas");
            modelBuilder.Entity<Direccion>().ToTable("Direcciones");
            modelBuilder.Entity<SubDireccion>().ToTable("SubDirecciones");

            modelBuilder.Properties<DateTime>().Configure(x => x.HasColumnType("datetime2"));

            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("Codigo"))
                .Configure(p => p.IsKey());

            //LLave Primaria simple
            modelBuilder.Entity<Persona>().HasKey(x => x.Id);

            //LLave Primaria  compuesta
            modelBuilder.Entity<Direccion>().HasKey(x => new { x.CodigoDireccion, x.Calle });

            //Nosotros asignamos la LLavePrimaria
            modelBuilder.Entity<Direccion>().Property(x => x.CodigoDireccion)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //El campo cedula de la tabla Persona es de longitud fija y tiene longitud maxima de 11
            modelBuilder.Entity<Persona>().Property(x => x.Nombre).IsFixedLength().HasMaxLength(150);

            modelBuilder.Entity<Persona>().Ignore(p => p.Resumen);

            modelBuilder.Entity<Direccion>().Property(d => d.CodigoDireccion).HasColumnName("Codigo");

            //No pluraliza nombres de tablas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //No mapea los decimales a numeric(18,2)
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();

            //Convension personalizada
            modelBuilder.Properties<decimal>().Configure(x => x.HasColumnType("decimal").HasPrecision(16,2));

            modelBuilder.Entity<Direccion>().HasRequired(d => d.Persona)
                .WithMany(p => p.Direcciones)
                .HasForeignKey(d => d.PersonaId);


            modelBuilder.Entity<SubDireccion>().HasRequired(s => s.Direccion)
                .WithMany(d => d.SubDirecciones);

            modelBuilder.Entity<Direccion>().Property(x => x.Calle).HasMaxLength(300);

            base.OnModelCreating(modelBuilder);
        }

        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry)
        {
            if (entityEntry.State == EntityState.Deleted)
            {
                return true;
            }

            return base.ShouldValidateEntity(entityEntry);
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if (entityEntry.Entity is Persona && entityEntry.State == EntityState.Deleted)
            {
                var entidad = entityEntry.Entity as Persona;
                if (entidad.Edad < 18)
                {
                    return new DbEntityValidationResult(entityEntry, new DbValidationError[]
                    {
                        new DbValidationError("Edad","No se puede borrar a un menor de edad.")
                    });
                }
            }
            return base.ValidateEntity(entityEntry,items);
        }

        public override int SaveChanges()
        {
            var entidades = ChangeTracker.Entries();
            if(entidades != null)
            {
                foreach (var entidad in entidades.Where(c => c.State != EntityState.Unchanged))
                {
                    Auditar(entidad);
                }
                
            }
            return base.SaveChanges();
        }

        private void Auditar(DbEntityEntry entity)
        { }
    }
}