using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligonotiro.Models;
using poligonotiro.Models.ViewModels;

namespace poligonotiro.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            int varrol = MySession.Current.Rol;

            if (varrol == 2)
            {
                return Redirect("~/Profile/");
            }
            else
            {
                List<ListaRoles> lst;

                using (Context db = new Context())

                {
                    lst = (from d in db.Rol
                           select new ListaRoles
                           {
                               Id = d.idrol,
                               descripcion = d.descripcion
                           }).ToList();
                }

                return View(lst);
            }

        }


        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(InsertRol model)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    using (Context db = new Context())

                    {
                        var oRol = new rol();
                        oRol.descripcion = model.descripcion;

                        db.Rol.Add(oRol);
                        db.SaveChanges();
                    }
                    return Redirect("~/Rol/");
                }
                return View(model);
                //return Redirect("/Rol");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}