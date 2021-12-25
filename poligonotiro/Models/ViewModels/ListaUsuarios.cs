using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace poligonotiro.Models.ViewModels
{
    public class ListaUsuarios
    {
        public int Id { get; set; }
        public string usuario { get; set; }

        public string correo { get; set; }

        public string password { get; set; }

        public int rol { get; set; }


        public string nombrerol { get; set; }
        public string token { get; set; }

        public string correorecupera { get; set; }



        [Required]

        public string Password1 { get; set; }

        [Compare("Password1")]
        [Required]
        public string passwordconfirm { get; set; }

    }

}