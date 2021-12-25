using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace poligonotiro.Models.ViewModels
{
    public class SubirArchivomodelo
    {
        public string confirmacion { get; set; }
        public Exception error { get; set; }

        public void SubirArchivo(string ruta, HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(ruta);
                this.confirmacion = "Excelente";
            }
            catch (Exception ex)
            {
                this.error = ex;
            }
        }
    }
}