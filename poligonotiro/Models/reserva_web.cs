namespace poligonotiro.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class reserva_web
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public reserva_web()
        {
            this.temporizador_linea = new HashSet<temporizador_linea>();
        }

        [Key]
        public int id_reserva_web { get; set; }
        public int id_usuario { get; set; }
        public System.DateTime fecha { get; set; }
        public System.TimeSpan hora_inicio { get; set; }
        public int cantidad_minutos { get; set; }
        public System.TimeSpan hora_fin { get; set; }
        public decimal costo { get; set; }
        public int id_linea_tiro { get; set; }
    
        public virtual linea_tiro linea_tiro { get; set; }
        public virtual usuario usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<temporizador_linea> temporizador_linea { get; set; }
    }
}
