namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblQRCode")]
    public partial class tblQRCode
    {
        [Key]
        public int MaSo { get; set; }

        [Required]
        [StringLength(10)]
        public string Ho { get; set; }

        [Required]
        [StringLength(20)]
        public string TenDem { get; set; }

        [Required]
        [StringLength(10)]
        public string Ten { get; set; }

        public byte? GioiTinh { get; set; }

        [Required]
        [StringLength(15)]
        public string SoCMND { get; set; }

        public int? Tuoi { get; set; }

        public byte? MaQR { get; set; }

        public byte? TheXanh { get; set; }
    }
}
