namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblNguoiDung")]
    public partial class tblNguoiDung
    {
        public DateTime? NgayTao { get; set; }

        [StringLength(30)]
        public string NguoiTao { get; set; }

        [StringLength(30)]
        public string NguoiCapNhat { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên đăng nhập:")]
        public string UserName { get; set; }

        [Key]
        public int MaSo { get; set; }


        [Display(Name = "Mật khẩu hiện tại:")]
        [StringLength(50)]
        //[Required(ErrorMessage = "Hãy nhập mật khẩu cũ của bạn")]
        [DataType(DataType.Password)]
        public string OldPass { get; set; }


        [StringLength(150)]
        [Display(Name = "Mật khẩu mới:")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }


        [Display(Name = "Nhập lại mật khẩu mới:")]
        [StringLength(50)]
        //[Required(ErrorMessage = "Hãy xác nhận mật khẩu mới của bạn")]
        [Compare(otherProperty: "MatKhau", ErrorMessage = "Xác nhận mật khẩu không khớp! Hãy nhập lại.")]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }



        [Required]
        [StringLength(10)]
        [Display(Name = "Họ:")]
        public string Ho { get; set; }

        [StringLength(20)]
        [Display(Name = "Tên Đệm:")]
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

        [StringLength(30)]
        [EmailAddress]    // ràng buộc nhập đúng mail để phục vụ việc gửi mail trả về khi thay đổi mật khẩu.
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [StringLength(15)]
        [Display(Name = "Số CMND:")]
        public string SoCMND { get; set; }

        [Display(Name = "Số mũi tiêm:")]
        public int? SoMuiTiem { get; set; }

        [StringLength(30)]
        [Display(Name = "Loại thẻ:")]
        public string LoaiThe { get; set; }

        public string ResetPasswordCode { get; set; }
    }
}
