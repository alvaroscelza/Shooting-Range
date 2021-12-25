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
    public class ReservasController : Controller
    {
        // GET: Reservas
        public ActionResult Index()
        {
            int varrol = MySession.Current.Rol;

            if (varrol == 2)
            {
                return Redirect("~/Profile/");
            }
            else
            {
                List<Listareservaweb> lst;
                using (Context db = new Context())
                {
                    lst = (from d in db.Reserva_web
                           join t in db.Usuario on d.id_usuario equals t.id_usuario
                           join l in db.Linea_tiro on d.id_linea_tiro equals l.id_linea_tiro
                           select new Listareservaweb
                           {
                               Id = d.id_reserva_web,
                               idusuario = d.id_usuario,
                               fecha = d.fecha,
                               horainicio = d.hora_inicio,
                               cantidadminutos = d.cantidad_minutos,
                               horafin = d.hora_fin,
                               costo = (int)d.costo,
                               idlineatiro = d.id_linea_tiro,
                               nombreusuario = t.nombre_usuario,
                               costopack = (int)l.costo_pack,
                               nombrelinea = l.nombre_linea_tiro
                           }).ToList();
                }


                //DROPDOWN DE LINEAS DE TIRO

                List<ListaLineaTiro> lst1;
                using (Context db1 = new Context())
                {
                    lst1 = (from d1 in db1.Linea_tiro
                            select new ListaLineaTiro
                            {
                                Idlineatiro = d1.id_linea_tiro,
                                nombre = d1.nombre_linea_tiro
                            }).ToList();
                }

                List<SelectListItem> items = lst1.ConvertAll(d1 =>
                {
                    return new SelectListItem()
                    {
                        Text = d1.nombre.ToString(),
                        Value = d1.Idlineatiro.ToString(),
                        Selected = false
                    };
                });

                ViewBag.items = items;



                //DROPDOWN DE COSTOS DE PACK


                List<ListaLineaTiro> lst2;
                using (Context db1 = new Context())
                {
                    lst2 = (from d1 in db1.Linea_tiro
                            select new ListaLineaTiro
                            {
                                Idlineatiro = d1.id_linea_tiro,
                                costopack = (decimal)d1.costo_pack
                            }).ToList();
                }

                List<SelectListItem> items1 = lst2.ConvertAll(d2 =>
                {
                    return new SelectListItem()
                    {
                        Text = d2.costopack.ToString(),
                        Value = d2.costopack.ToString(),
                        Selected = false
                    };
                });

                ViewBag.items1 = items1;



                return View(lst);
            }
        }


        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(Listareservaweb model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Context db = new Context())
                    {
                        var oReserva = new reserva_web();
                        oReserva.id_usuario = (int)Session["IDUSUARIO"];
                        oReserva.fecha = model.fecha;
                        oReserva.hora_inicio = model.horainicio;
                        oReserva.cantidad_minutos = model.cantidadminutos;
                        oReserva.hora_fin = model.horafin;
                        oReserva.costo = model.costo;
                        oReserva.id_linea_tiro = model.idlineatiro;

                        db.Reserva_web.Add(oReserva);
                        db.SaveChanges();
                    }
                    return Redirect("~/Reservas/");
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
            Listareservaweb model = new Listareservaweb();

            using (Context db = new Context())
            {
                var oReserva = db.Reserva_web.Find(idactualiza);
                model.fecha = oReserva.fecha;
                model.horainicio = oReserva.hora_inicio;
                model.cantidadminutos = oReserva.cantidad_minutos;
                model.horafin = oReserva.hora_fin;
                model.costo = oReserva.costo;
                model.idlineatiro = oReserva.id_linea_tiro;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Editar(Listareservaweb model)
        {

            if (ModelState.IsValid)
            {
                using (Context db = new Context())
                {
                    var oReserva = db.Reserva_web.Find(model.Idactualiza);
                    oReserva.fecha = model.fecha;
                    oReserva.hora_inicio = model.horainicio;
                    oReserva.cantidad_minutos = model.cantidadminutos;
                    oReserva.hora_fin = model.horafin;
                    oReserva.costo = model.costo;
                    oReserva.id_linea_tiro = model.idlineatiroact;

                    db.Entry(oReserva).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Redirect("~/Reservas/");
            }
            return View(model);

        }

        [HttpGet]
        public ActionResult Eliminar(int idelimina)
        {
            using (Context db = new Context())
            {
                var oReserva = db.Reserva_web.Find(idelimina);
                db.Reserva_web.Remove(oReserva);
                db.SaveChanges();
            }
            return Redirect("~/Reservas/");
        }


    }
}
