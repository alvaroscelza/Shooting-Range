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
    public class ReportemesController : Controller
    {
        // GET: Reportemes
        public ActionResult Index(DateTime fechaini, DateTime fechafin)
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
                           where d.fecha >= fechaini && d.fecha <= fechafin
                           select new Listareservaweb
                           {
                               Id = d.id_reserva_web,
                               idusuario = d.id_usuario,
                               fecha = d.fecha,
                               horainicio = d.hora_inicio,
                               cantidadminutos = d.cantidad_minutos,
                               horafin = d.hora_fin,
                               costo = d.costo,
                               idlineatiro = d.id_linea_tiro,
                               nombreusuario = t.nombre_usuario,
                               costopack = (decimal)l.costo_pack,
                               nombrelinea = l.nombre_linea_tiro,
                               fechaini = fechaini,
                               fechafin = fechafin
                           }).ToList();
                }
                ViewData["fechaini"] = fechaini.ToString("dd/MM/yyyy");
                ViewData["fechafin"] = fechafin.ToString("dd/MM/yyyy");

                return View(lst);
            }

        }
    }
}