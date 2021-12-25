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
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            
            int varrol = MySession.Current.Rol;

            if (varrol == 2)
            {
                return Redirect("~/Profile/");
            }
            else
            {
                List<ListaUsuarios> lst;
                using (Context db = new Context())
                {
                    lst = (from d in db.Usuario
                           join t in db.Rol on d.id_rol equals t.idrol

                           select new ListaUsuarios
                           {
                               Id = d.id_usuario,
                               usuario = d.nombre_usuario,
                               correo = d.email,
                               password = d.contrasena,
                               rol = d.id_rol,
                               nombrerol = t.descripcion

                           }).ToList();
                }


                //DROPDOWN DE ROLES

                List<ListaRoles> lst1;
                using (Context db1 = new Context())
                {
                    lst1 = (from d1 in db1.Rol
                            select new ListaRoles
                            {
                                Id = d1.idrol,
                                descripcion = d1.descripcion
                            }).ToList();
                }

                List<SelectListItem> items = lst1.ConvertAll(d1 =>
                {
                    return new SelectListItem()
                    {
                        Text = d1.descripcion.ToString(),
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
        public ActionResult Nuevo(InsertUsuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Context db = new Context())
                    {
                        //string ePass = Encrypt.GetSHA256(contrasena);
                        var oUsuario = new usuario();
                        oUsuario.nombre_usuario = model.nombre;
                        oUsuario.email = model.correo;
                        //oUsuario.contrasena = Encrypt.GetSHA256(model.contrasena);
                        oUsuario.id_rol = model.idrol;

                        db.Usuario.Add(oUsuario);
                        db.SaveChanges();
                    }
                    return Redirect("~/Usuarios/");
                }
                return View(model);
                //return Redirect("/Rol");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public ActionResult Editar(int idactualiza)
        {
            InsertUsuario model = new InsertUsuario();

            using (Context db = new Context())
            {
                var oUsuario = db.Usuario.Find(idactualiza);
                model.nombre = oUsuario.nombre_usuario;
                model.correo = oUsuario.email;

                //model.contrasena = oUsuario.contrasena;

                model.idrolact = oUsuario.id_rol;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Editar(InsertUsuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Context db = new Context())
                    {
                        //string ePass = Encrypt.GetSHA256(contrasena);
                        var oUsuario = db.Usuario.Find(model.idactualiza);
                        oUsuario.nombre_usuario = model.nombre;
                        oUsuario.email = model.correo;


                        //oUsuario.contrasena = Encrypt.GetSHA256(model.contrasena);

                        oUsuario.id_rol = model.idrolact;

                        db.Entry(oUsuario).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Usuarios/");
                }
                return View(model);
                //return Redirect("/Rol");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int idelimina)
        {
            using (Context db = new Context())
            {
                var oCliente = new cliente();
                var idusu = db.Cliente.FirstOrDefault(e => e.id_usuario == idelimina);

                if (idusu != null)
                {
                    return RedirectToAction("Index", new { message = "No se puede eliminar el usuario debido a que tiene un cliente asociado, Por favor verifique." });
                }
                else
                {
                    var oUsuario = db.Usuario.Find(idelimina);
                    db.Usuario.Remove(oUsuario);
                    db.SaveChanges();
                }

                
            }
            return Redirect("~/Usuarios/");
        }


    }
}
