namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblKhaiBaoYTe")]
    public partial class tblKhaiBaoYTe
    {
        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Họ và tên (*):")]
        public string HoTen { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Số CMND (*):")]
        public string SoCMND { get; set; }

        [StringLength(10)]
        [Display(Name = "Giới tính:")]
        public string GioiTinh { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày sinh:")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        [Display(Name = "Điện thoại:")]
        public string DienThoai { get; set; }

        [StringLength(15)]
        [Display(Name = "Số thẻ BHYT:")]
        public string SoTheBHYT { get; set; }

        [StringLength(50)]
        [Display(Name = "Email:")]
        public string Email { get; set; }


        [StringLength(50)]
        [Display(Name = "Tỉnh thành:")]
        public string ProvinceID { get; set; }


        [StringLength(50)]
        [Display(Name = "Quận huyện:")]
        public string DistrictID { get; set; }


        [StringLength(50)]
        [Display(Name = "Xã phường:")]
        public string WardsID { get; set; }


        [StringLength(150)]
        [Display(Name = "Địa điểm cụ thể (nếu có tiếp xúc):")]
        public string DiaDiemCuThe { get; set; }

        [StringLength(10)]
        [Display(Name = "Có tiếp xúc khu vực nghi nhiễm:")]
        public string TiepXucKhuVuc { get; set; }  

        [StringLength(10)]
        [Display(Name = "Có tiếp xúc người nhiễm bệnh:")]
        public string TiepXucNguoiBenh { get; set; }

        [StringLength(10)]
        [Display(Name = "Có triệu chứng của bệnh:")]
        public string TrieuChungNhiemBenh { get; set; }

        [Display(Name = "Duyệt (Check nếu đã duyệt):")]
        public bool Status { get; set; }
    }
}
