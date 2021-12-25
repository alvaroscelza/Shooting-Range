namespace poligonotiro.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class temporizador_linea
    {
        [Key]
        public int id_temporizador { get; set; }
        public int id_reserva_web { get; set; }
    
        public virtual reserva_web reserva_web { get; set; }
    }
}
