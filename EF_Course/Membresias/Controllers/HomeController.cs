using Membresias.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Membresias.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var estaAutenticado = User.Identity.IsAuthenticated;
            if (estaAutenticado)
            {
                var NombreUsuario = User.Identity.Name;
                var id = User.Identity.GetUserId();

                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var usuario = db.Users.Where(x => x.Id == id).FirstOrDefault();
                    var email = usuario.EmailConfirmed;

                    //Creación de usuario

                    var userManager = new UserManager<ApplicationUser>(
                        new UserStore<ApplicationUser>(db));

                    //var user = new ApplicationUser();
                    //user.UserName = "alonsomd";
                    //user.Email = "alonsomd@hotmail.com";

                    //var resultado = userManager.Create(user, "Peru2018.");

                    //Creación de roles

                    var roleManager = new RoleManager<IdentityRole>
                        (new RoleStore<IdentityRole>(db));

                    var resultado = roleManager.Create(new IdentityRole("ADMIN"));

                    //Agregar usuario a rol
                    resultado = userManager.AddToRole(id, "ADMIN");

                    //Obtener roles del usuario
                    var roles = userManager.GetRoles(id);

                    //Remover a usuario de role

                    resultado = userManager.RemoveFromRole(id, "ADMIN");

                    //Borrar Rol
                    var rolvendedor = roleManager.FindByName("ADMIN");
                    roleManager.Delete(rolvendedor);


                }
            }
            return View();
        }

        [Authorize(Users = "alonso2295@hotmail.com")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}