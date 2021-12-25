using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligonotiro.Models;
using poligonotiro.Models.ViewModels;


public class MySession
{
    // private constructor
    private MySession()
    {
        if(HttpContext.Current.Session["IDUSUARIO"] == null)
        {

        }
        else
        {
            int var = (int)HttpContext.Current.Session["IDUSUARIO"];
            Property1 = var;

            int var1 = (int)HttpContext.Current.Session["IDROL"];
            Rol = var1;
        }

    }

    // Gets the current session.
    public static MySession Current
    {
        get
        {
            MySession session = new MySession();

            if (session == null)
            {
                session = new MySession();
                HttpContext.Current.Session["IDUSUARIO"] = session;
            }
            return session;
        }
    }

    // **** add your session properties here, e.g like this:
    public int Property1 { get; set; }

    public int Rol { get; set; }
    public DateTime MyDate { get; set; }
    public int LoginId { get; set; }
}