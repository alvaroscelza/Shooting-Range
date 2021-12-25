namespace poligonotiro.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class arma
    {
        [Key]
        public int id_arma { get; set; }
        public string nombre_arma { get; set; }
        public string modelo { get; set; }
        public string foto { get; set; }
        public string numero_serie { get; set; }
    }
}
