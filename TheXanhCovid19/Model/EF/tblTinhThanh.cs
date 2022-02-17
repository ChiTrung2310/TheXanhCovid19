namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblTinhThanh")]
    public partial class tblTinhThanh
    {
        [Key]
        [StringLength(50)]
        public string MaTT { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tỉnh / Thành Phố:")]
        public string TinhTP { get; set; }

        [Display(Name = "Dự kiến tiêm chủng:")]
        public int? DuKienTC { get; set; }

        [Display(Name = "Phân bổ thực tế:")]
        public int? PhanBoTT { get; set; }

        [Display(Name = "Dân số:")]
        public int? DanSo { get; set; }

        [Display(Name = "Số liều đã tiêm:")]
        public int? SoLieuDaTiem { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCapNhat { get; set; }

        [Display(Name = "Tỉ lệ:")]
        public double? TiLe { get; set; }
    }
}
