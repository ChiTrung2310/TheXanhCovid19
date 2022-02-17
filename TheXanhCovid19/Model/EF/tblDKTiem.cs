namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDKTiem")]
    public partial class tblDKTiem
    {
        [Key]
        public int MaDK { get; set; }

        [Required]
        [StringLength(10)]
        public string Ho { get; set; }

        [StringLength(20)]
        public string TenDem { get; set; }

        [Required]
        [StringLength(10)]
        public string Ten { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayTiem { get; set; }

        public byte? GioiTinh { get; set; }

        [StringLength(150)]
        public string QueQuan { get; set; }

        [Required]
        [StringLength(15)]
        public string SoCMND { get; set; }

        [StringLength(150)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(10)]
        public string DienThoai { get; set; }

        public byte? DuDieuKien { get; set; }
    }
}
