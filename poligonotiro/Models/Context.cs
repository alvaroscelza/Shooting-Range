namespace poligonotiro.Models
{
    using System.Data.Entity;

    public partial class Context : DbContext
    {
        public Context() : base("name=Context") { }
    
        public virtual DbSet<arma> Arma { get; set; }
        public virtual DbSet<cliente> Cliente { get; set; }
        public virtual DbSet<linea_tiro> Linea_tiro { get; set; }
        public virtual DbSet<reserva_web> Reserva_web { get; set; }
        public virtual DbSet<rol> Rol { get; set; }
        public virtual DbSet<supervisor> Supervisor { get; set; }
        public virtual DbSet<sysdiagrams> Sysdiagrams { get; set; }
        public virtual DbSet<temporizador_linea> Temporizador_linea { get; set; }
        public virtual DbSet<usuario> Usuario { get; set; }
    }
}
