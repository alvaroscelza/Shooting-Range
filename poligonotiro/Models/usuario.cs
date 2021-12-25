namespace poligonotiro.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.cliente = new HashSet<cliente>();
            this.reserva_web = new HashSet<reserva_web>();
        }

        [Key]
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }
        public int id_rol { get; set; }
        public string token { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cliente> cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reserva_web> reserva_web { get; set; }
        public virtual rol rol { get; set; }
    }
}
