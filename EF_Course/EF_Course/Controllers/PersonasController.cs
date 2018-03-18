using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EF_Course.Models;

namespace EF_Course.Controllers
{
    public class PersonasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Personas
        public ActionResult Index()
        {
            //Selecciona todas las columnas
            var listadoPersonasTodasLasColumnas = db.Persona.ToList();

            //Seleccionando una columna
            var listadoDeNombres = db.Persona.Select(x => x.Nombre).ToList();

            //Selecciona varias columnas proyectando en un tipo anonimo
            var listadoPersonasVariasColumnasAnonimo = db.Persona.Select(x => new { Nombre = x.Nombre, Edad = x.Edad }).ToList();

            //Seleccionando varias columnas proyectando en persona
            var listadoPersonasVariasColumnas = db.Persona.Select(x => new { Nombre = x.Nombre, Edad = x.Edad }).ToList()
                .Select(x => new Persona() { Nombre = x.Nombre, Edad = x.Edad }).ToList();


            var persona = db.Persona.Where(p => p.Id == 2).FirstOrDefault();

            var personaDireccion = db.Direccion.Join(db.Persona, dir => dir.PersonaId, per => per.Id,(dir,per)=>new { dir,per}).FirstOrDefault(x=>x.dir.CodigoDireccion == 1);

            //var persona = new Persona() { Id = 2 };
            //db.Persona.Attach(persona);
            //db.Direccion.Add(new Direccion() { Calle = "Ejemplo 2", Persona = persona });

            db.SaveChanges();
            return View(db.Persona.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Nacimiento")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Persona.Add(persona);
                //.Addange permite IEnumerable lo que permite que le pasemos una coleccion de personas
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(persona);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Nacimiento")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                //Método 1: Trae el objeto y lo actualiza.
                var personaEditar = db.Persona.FirstOrDefault(p => p.Id == 2);
                personaEditar.Nombre = "EditandoNombre";
                personaEditar.Edad = personaEditar.Edad + 1;
                db.SaveChanges();


                //Metodo 2: Actualizacion Parcial
                var personaEditar2 = new Persona();
                personaEditar2.Id = 3;
                personaEditar2.Nombre = "Editado metodo 2";
                personaEditar2.Edad = 54;
                db.Persona.Attach(personaEditar2);
                db.Entry(personaEditar2).Property(x => x.Nombre).IsModified = true;
                db.SaveChanges();


                // Método 3: Objeto externo actualizado
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Persona.Find(id);
            db.Persona.Remove(persona);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
