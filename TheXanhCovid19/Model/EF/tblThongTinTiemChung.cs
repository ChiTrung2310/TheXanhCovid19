namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblThongTinTiemChung")]
    public partial class tblThongTinTiemChung
    {
        [Key]
        public int MaTC { get; set; }

        [Display(Name = "Đối tượng đăng ký tiêm:")]
        public int? DoiTuongDKTiem { get; set; }

        [Display(Name = "Số mũi tiêm hôm qua:")]
        public int? SoMuiTiemHomQua { get; set; }

        [Display(Name = "Số mũi tiêm toàn quốc:")]
        public int? SoMuiTiemToanQuoc { get; set; }

        [Display(Name = "Độ bao phủ:")]
        public double? DoBaoPhu { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày cập nhật:")]
        public DateTime? NgayCapNhat { get; set; }

        [StringLength(50)]
        [Display(Name = "Người cập nhật:")]
        public string NguoiCapNhat { get; set; }

        [Column(TypeName = "text")]
        [Display(Name = "Ghi chú:")]
        public string GhiChu { get; set; }
    }
}
