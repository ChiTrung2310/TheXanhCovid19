using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheXanhCovid19.Models
{
    public class ResetPasswordModel
    {
        [Display(Name = "Nhập mật khẩu mới:")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu mới", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }


        [Display(Name = "Xác nhận mật khẩu:")]
        [DataType(DataType.Password)]
        [Compare(otherProperty: "NewPassword", ErrorMessage = "Xác nhận mật khẩu không khớp! Hãy nhập lại.")]
        public string ConfirmPassword { get; set; }


        [Required]
        public string ResetCode { get; set; }
    }
}