using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

using System.IO;
using System.Web.Helpers;

using System.Web.Mvc;
using poligonotiro.Models;
using poligonotiro.Models.ViewModels;

namespace poligonotiro.Controllers
{
    [Authorize]
    public class ArmasController : Controller
    {
        // GET: Armas

        public ActionResult Index()
        {
            int varrol = MySession.Current.Rol;

            if (varrol == 2)
            {
                return Redirect("~/Profile/");
            }
            else
            {
                List<ListaArmas> lst;

                using (Context db = new Context())

                {
                    lst = (from d in db.Arma
                           select new ListaArmas
                           {
                               Idarma = d.id_arma,
                               nombrearma = d.nombre_arma,
                               modelo = d.modelo,
                               foto = d.foto,
                               numeroserie = d.numero_serie,
                           }).ToList();
                }

                return View(lst);
            }


        }



        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase imagen)
        {
            if (imagen.ContentLength > 0)
            {
                string relativePath = "~/Temp/" + Path.GetFileName(imagen.FileName);
                string physicalPath = Server.MapPath(relativePath);
                imagen.SaveAs(physicalPath);
                return View((object)relativePath);
            }
            return View();
        }



        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]

        public ActionResult Nuevo(ListaArmas model, HttpPostedFileBase foto)

        {
            try
            {
                if (ModelState.IsValid)
                {


                    /*if (imagen.ContentLength > 0)
                    {
                        string relativePath = "~/Temp/" + Path.GetFileName(imagen.FileName);
                        string physicalPath = Server.MapPath(relativePath);
                        imagen.SaveAs(physicalPath);
                        //return View((object)relativePath);
                    }*/

                    if (foto != null)
                    {
                        string image1 = foto.FileName;
                        model.foto = image1;
                        var image1Path = Path.Combine(Server.MapPath("~/Temp"), image1);
                        foto.SaveAs(image1Path);
                    }

                    using (Context db = new Context())

                    {
                        var oArma = new arma();
                        oArma.nombre_arma = model.nombrearma;
                        oArma.modelo = model.modelo;
                        oArma.foto = model.foto;
                        oArma.numero_serie = model.numeroserie;

                        db.Arma.Add(oArma);
                        db.SaveChanges();
                    }
                    return Redirect("~/Armas/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public ActionResult Editar(int Idarma)
        {
            ListaArmas model = new ListaArmas();


            using (Context db = new Context())

            {
                var oArma = db.Arma.Find(Idarma);
                model.nombrearma = oArma.nombre_arma;
                model.modelo = oArma.modelo;
                model.foto = oArma.foto;
                model.numeroserie = oArma.numero_serie;
            }
            return View(model);
        }


        [HttpPost]


        public ActionResult Editar(ListaArmas model, HttpPostedFileBase foto)

        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (Context db = new Context())

                        if (foto != null)
                        {
                            string image1 = foto.FileName;
                            model.foto = image1;
                            var image1Path = Path.Combine(Server.MapPath("~/Temp"), image1);
                            foto.SaveAs(image1Path);
                        }

                    using (Context db = new Context())

                    {
                        var oArma = db.Arma.Find(model.Idarma);
                        oArma.nombre_arma = model.nombrearma;
                        oArma.modelo = model.modelo;


                        if (foto != null)
                        {
                            oArma.foto = model.foto;
                        }

                        oArma.numero_serie = model.numeroserie;

                        db.Entry(oArma).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Armas/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int idarma)
        {

            using (Context db = new Context())

            {
                var oArma = db.Arma.Find(idarma);
                db.Arma.Remove(oArma);
                db.SaveChanges();
            }
            return Redirect("~/Armas/");
        }
    }
}