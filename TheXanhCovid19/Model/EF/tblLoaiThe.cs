namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblLoaiThe")]
    public partial class tblLoaiThe
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string LoaiThe { get; set; }

        [Column(TypeName = "text")]
        public string GhiChu { get; set; }
    }
}
