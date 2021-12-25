﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using poligonotiro.Models;
using poligonotiro.Models.ViewModels;
using System.Net.Mail;

namespace poligonotiro.Controllers
{
    public class HomeController : Controller
    {

        string urlDomain = "https://shooting-range.azurewebsites.net/";

        public ActionResult Index(string message = "", string message1 = "")
        {
            ViewBag.Message = message;
            ViewBag.Message1 = message1;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                using (Context db = new Context())
                {
                    var oUsuario = new usuario();
                    string ePass = Encrypt.GetSHA256(password);
                    var usermail = db.Usuario.FirstOrDefault(e => e.email == email && e.contrasena == ePass);

                    if (usermail != null)
                    {
                        //se encontraron datos
                        FormsAuthentication.SetAuthCookie(usermail.email, true);
                        Session["IDUSUARIO"] = usermail.id_usuario;
                        Session["IDROL"] = usermail.id_rol;
                        return RedirectToAction("Index", "Profile");
                    }
                    else
                    {
                        return RedirectToAction("Index", new { message = "No se reconocen los datos, Intentelo de nuevo." });
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", new { message = "Llene los campos para iniciar sesión." });
            }
        }

        public ActionResult Registro(string emailregistro, string userregistro, string passwordregistro, string passwordconfirmregistro)
        {

            if (!string.IsNullOrEmpty(emailregistro) && !string.IsNullOrEmpty(userregistro) && !string.IsNullOrEmpty(passwordregistro) && !string.IsNullOrEmpty(passwordconfirmregistro))
            {
                if (passwordregistro != passwordconfirmregistro)
                {
                    return RedirectToAction("Index", new { message = "Las contraseñas no coinciden, intentelo de nuevo." });
                }
                else
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            using (Context db1 = new Context())
                            {
                                var oUsuario1 = new usuario();
                                oUsuario1.nombre_usuario = userregistro;
                                oUsuario1.email = emailregistro;
                                oUsuario1.contrasena = Encrypt.GetSHA256(passwordregistro);
                                oUsuario1.id_rol = 2;

                                //para desencriptar
                                //string pass = "123";
                                //string ePass = Encrypt.GetSHA256(pass);

                                db1.Usuario.Add(oUsuario1);
                                db1.SaveChanges();
                            }
                            return RedirectToAction("Index", new { message1 = "Felicidades, su registro se ha realizado éxitosamente." });
                        }
                        //return View(model);
                        return Redirect("/Home");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", new { message = "Llene todos los campos para poder registrarse" });
            }
        }



        [HttpPost]

        public ActionResult Recuperacion(ListaUsuarios model)
        {
            string token = Encrypt.GetSHA256(Guid.NewGuid().ToString());
            try
            {
                //if (ModelState.IsValid)
                //{
                using (Context db1 = new Context())
                {
                    var oUser = db1.Usuario.Where(d => d.email == model.correorecupera).FirstOrDefault();


                    if (oUser != null)
                    {
                        oUser.token = token;
                        db1.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db1.SaveChanges();

                        //enviar mail

                        SendEmail(oUser.email, token);
                    }


                }
                return RedirectToAction("Index", new { Message1 = "Se ha enviado un correo electrónico a la dirección proporcionada para la recuperación de contraseña." });
                //}
                //return Redirect("/Home");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        public ActionResult Recuperacioncon(string token)
        {
            ListaUsuarios model = new ListaUsuarios();
            model.token = token;

            using (Context db1 = new Context())
            {
                if (model.token == null || model.token.Trim().Equals(""))
                {
                    return View("Index");
                }
                var oUser = db1.Usuario.Where(d => d.token == model.token).FirstOrDefault();
                if (oUser == null)
                {
                    ViewBag.Message = "Tu Token ha expirado";
                    return View("Index");
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Recuperacioncon(ListaUsuarios model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Context db1 = new Context())
                    {
                        var oUser = db1.Usuario.Where(d => d.token == model.token).FirstOrDefault();

                        if (oUser != null)
                        {
                            oUser.contrasena = Encrypt.GetSHA256(model.Password1);
                            oUser.token = null;
                            db1.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                            db1.SaveChanges();
                        }
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            ViewBag.Message1 = "Contraseña modificada con éxito";
            return View("Index");
        }






        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }


        private void SendEmail(string emaildestino, string token)
        {
            string emailorigen = "noreply.poligonotiro@gmail.com";
            string contraseña = "poligonotiro*123";
            string url = urlDomain + "Home/Recuperacioncon/?token=" + token;

            MailMessage oMailMessage = new MailMessage(emailorigen, emaildestino, "Recuperación de contraseña",
                "<p>Correo para recuperación de contraseña - Polígono de Tiro</p><br />" +
                "<a href='" + url + "'>Click para recuperación de contraseña</a>"
                );

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;

            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(emailorigen, contraseña);

            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();
        }




    }
}