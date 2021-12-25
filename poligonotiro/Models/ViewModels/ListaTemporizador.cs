using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace poligonotiro.Models.ViewModels
{
    public class ListaTemporizador
    {
        public int Id { get; set; }
        public int idreserva { get; set; }

        public int idactualiza { get; set; }
        public int idreservaact { get; set; }

    }

}