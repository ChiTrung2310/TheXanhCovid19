namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblQuanTri")]
    public partial class tblQuanTri
    {
        [Key]
        public int MaQT { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }


        [Display(Name = "Mật khẩu cũ:")]
        [StringLength(50)]
        [Required(ErrorMessage = "Hãy nhập mật khẩu cũ của bạn")]
        [DataType(DataType.Password)]
        public string OldPass { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "Mật khẩu:")]
        public string MatKhau { get; set; }


        [Display(Name = "Xác nhận mật khẩu mới")]
        [StringLength(50)]
        [Required(ErrorMessage = "Hãy xác nhận mật khẩu mới của bạn")]
        [Compare(otherProperty: "MatKhau", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }


        [Required]
        [StringLength(10)]
        [Display(Name = "Họ:")]
        public string Ho { get; set; }

        [StringLength(20)]
        [Display(Name = "Tên đệm:")]
        public string TenDem { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Tên:")]
        public string Ten { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày sinh:")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        [Display(Name = "Giới tính:")]
        public string GioiTinh { get; set; }

        [StringLength(150)]
        [Display(Name = "Quê quán:")]
        public string QueQuan { get; set; }

        [StringLength(150)]
        [Display(Name = "Địa chỉ:")]
        public string DiaChi { get; set; }

        [StringLength(10)]
        [Display(Name = "Điện thoại:")]
        public string DienThoai { get; set; }

        [Display(Name = "Quyền:")]
        public bool? Role { get; set; }

        [StringLength(30)]
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}
