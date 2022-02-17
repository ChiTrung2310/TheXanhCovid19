namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;



    [Table("tblPhanAnh")]
    public class tblPhanAnh
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

        [Display(Name = "Điện thoại:")]
        public string DienThoai { get; set; }

        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }


        [StringLength(150)]
        [Display(Name = "Tiêu đề:")]
        public string TieuDe { get; set; }


        [StringLength(350)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Nội dung:")]
        public string NoiDung { get; set; }


        [Column(TypeName = "date")]
        [Display(Name = "Ngày phản ánh:")]
        public DateTime? NgayPhanAnh { get; set; }



        [Display(Name = "Duyệt (Check nếu đã duyệt):")]
        public bool Status { get; set; }
    }
}
