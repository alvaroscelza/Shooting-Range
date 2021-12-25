namespace poligonotiro.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class supervisor
    {
        [Key]
        public int id_supervisor { get; set; }
        public string nombre_supervisor { get; set; }
    }
}
