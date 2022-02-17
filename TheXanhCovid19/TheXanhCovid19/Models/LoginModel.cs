using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheXanhCovid19.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name ="Tài khoản")]
        [Required(ErrorMessage = "Yêu cầu nhập tài khoản")]
        public string UserName { set; get; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { set; get; }
    }


}