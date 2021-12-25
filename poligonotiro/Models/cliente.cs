namespace poligonotiro.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class cliente
    {
        [Key]
        public int id_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string documento { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string porte_arma { get; set; }
        public string registro_arma { get; set; }
        public Nullable<int> id_usuario { get; set; }
    
        public virtual usuario usuario { get; set; }
    }
}
