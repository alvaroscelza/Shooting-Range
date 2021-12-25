namespace poligonotiro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class linea_tiro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public linea_tiro()
        {
            this.reserva_web = new HashSet<reserva_web>();
        }

        [Key]
        public int id_linea_tiro { get; set; }
        public string nombre_linea_tiro { get; set; }
        public Nullable<decimal> costo_pack { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reserva_web> reserva_web { get; set; }
    }
}
