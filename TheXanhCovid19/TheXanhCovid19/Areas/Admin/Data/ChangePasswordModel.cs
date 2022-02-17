using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheXanhCovid19.Areas.Admin.Data
{
    public class ChangePasswordModel
    {
        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage ="Hãy nhập mật khẩu cũ của bạn")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }


        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Hãy nhập mật khẩu mới của bạn")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Display(Name = "Xác nhận mật khẩu mới")]
        [Required(ErrorMessage = "Hãy xác nhận mật khẩu mới của bạn")]
        [Compare(otherProperty: "NewPassword" , ErrorMessage ="Xác nhận mật khẩu không khớp")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}