using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace poligonotiro.Models.ViewModels
{
    public class Listareservaweb
    {
        public int Id { get; set; }
        public int idusuario { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha { get; set; }
        public TimeSpan horainicio { get; set; }
        public int cantidadminutos { get; set; }
        public TimeSpan horafin { get; set; }
        public decimal costo { get; set; }
        public int idlineatiro { get; set; }
        public int Idactualiza { get; set; }
        public int idlineatiroact { get; set; }

        public string nombreusuario { get; set; }

        public string nombrelinea { get; set; }

        public decimal costopack { get; set; }

        public DateTime fechaini { get; set; }
        public DateTime fechafin { get; set; }


    }

}