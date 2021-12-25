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
    public class LineaTiroController : Controller
    {
        // GET: LineaTiro
        public ActionResult Index()
        {
            int varrol = MySession.Current.Rol;

            if (varrol == 2)
            {
                return Redirect("~/Profile/");
            }
            else
            {
                List<ListaLineaTiro> lst;
                using (Context db = new Context())
                {
                    lst = (from d in db.Linea_tiro
                           select new ListaLineaTiro
                           {
                               Idlineatiro = d.id_linea_tiro,
                               nombre = d.nombre_linea_tiro,


                               costopack = (decimal)d.costo_pack

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
        public ActionResult Nuevo(ListaLineaTiro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Context db = new Context())
                    {
                        var oLineaTiro = new linea_tiro();
                        oLineaTiro.nombre_linea_tiro = model.nombre;
                        oLineaTiro.costo_pack = model.costopack;


                        db.Linea_tiro.Add(oLineaTiro);
                        db.SaveChanges();
                    }
                    return Redirect("~/LineaTiro/");
                }
                return View(model);
                //return Redirect("/Rol");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public ActionResult Editar(int Idlineatiro)
        {
            ListaLineaTiro model = new ListaLineaTiro();

            using (Context db = new Context())
            {
                var oLineaTiro = db.Linea_tiro.Find(Idlineatiro);
                model.nombre = oLineaTiro.nombre_linea_tiro;
                model.costopack = (decimal)oLineaTiro.costo_pack;

            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Editar(ListaLineaTiro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Context db = new Context())
                    {
                        var oLineaTiro = db.Linea_tiro.Find(model.Idlineatiro);
                        oLineaTiro.nombre_linea_tiro = model.nombre;
                        oLineaTiro.costo_pack = model.costopack;


                        db.Entry(oLineaTiro).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/LineaTiro/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int idlineatiro)
        {
            using (Context db = new Context())
            {
                var oLineaTiro = db.Linea_tiro.Find(idlineatiro);
                db.Linea_tiro.Remove(oLineaTiro);
                db.SaveChanges();
            }
            return Redirect("~/LineaTiro/");
        }
    }
}