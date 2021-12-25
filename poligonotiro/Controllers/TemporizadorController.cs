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
    public class TemporizadorController : Controller
    {
        // GET: Temporizador
        public ActionResult Index()
        {
            int varrol = MySession.Current.Rol;

            if (varrol == 2)
            {
                return Redirect("~/Profile/");
            }
            else
            {
                List<ListaTemporizador> lst;
                using (Context db = new Context())
                {
                    lst = (from d in db.Temporizador_linea
                           select new ListaTemporizador
                           {
                               Id = d.id_temporizador,
                               idreserva = d.id_reserva_web
                           }).ToList();
                }


                //DROPDOWN DE RESERVAS

                List<Listareservaweb> lst1;
                using (Context db1 = new Context())
                {
                    lst1 = (from d1 in db1.Reserva_web
                            join t in db1.Usuario on d1.id_usuario equals t.id_usuario
                            select new Listareservaweb
                            {
                                Id = d1.id_reserva_web,
                                nombreusuario = t.nombre_usuario,
                                fecha = d1.fecha
                            }).ToList();
                }

                List<SelectListItem> items = lst1.ConvertAll(d1 =>
                {
                    return new SelectListItem()
                    {
                        Text = d1.Id.ToString() + " - " + d1.nombreusuario.ToString() + " - " + d1.fecha.ToShortDateString(),
                        Value = d1.Id.ToString(),
                        Selected = false
                    };
                });

                ViewBag.items = items;

                return View(lst);
            }
        }


        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(ListaTemporizador model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Context db = new Context())
                    {
                        var oTemporizador = new temporizador_linea();
                        oTemporizador.id_temporizador = model.Id;
                        oTemporizador.id_reserva_web = model.idreserva;

                        db.Temporizador_linea.Add(oTemporizador);
                        db.SaveChanges();
                    }
                    return Redirect("~/Temporizador/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public ActionResult Editar(int idactualiza)
        {
            ListaTemporizador model = new ListaTemporizador();

            using (Context db = new Context())
            {
                var oReserva = db.Temporizador_linea.Find(idactualiza);
                model.idreserva = oReserva.id_reserva_web;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Editar(ListaTemporizador model)
        {

            if (ModelState.IsValid)
            {
                using (Context db = new Context())
                {
                    var oTemporizador = db.Temporizador_linea.Find(model.idactualiza);
                    oTemporizador.id_reserva_web = model.idreservaact;

                    db.Entry(oTemporizador).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Redirect("~/Temporizador/");
            }
            return View(model);

        }

        [HttpGet]
        public ActionResult Eliminar(int idelimina)
        {
            using (Context db = new Context())
            {
                var oTemporizador = db.Temporizador_linea.Find(idelimina);
                db.Temporizador_linea.Remove(oTemporizador);
                db.SaveChanges();
            }
            return Redirect("~/Temporizador/");
        }


    }
}