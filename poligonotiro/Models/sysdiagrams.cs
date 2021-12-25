namespace poligonotiro.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class sysdiagrams
    {
        public string name { get; set; }
        [Key]
        public int principal_id { get; set; }
        public int diagram_id { get; set; }
        public Nullable<int> version { get; set; }
        public byte[] definition { get; set; }
    }
}
