using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing.QrCode;
using TheXanhCovid19.Common;

namespace TheXanhCovid19.Models
{
    public class THController : Controller
    {

        TheXanhCovid19DBContext mydb = new TheXanhCovid19DBContext();

        // GET: TH
        public ActionResult Index()
        {
            //var mysession = (UserLogin) Session[TheXanhCovid19.Common.CommonConstants.USER_SESSION];
            ////nếu chưa đăng nhập thì không thể vào được trang thông tin người dùng
            //if (mysession == null)
            //{
            //    return RedirectToAction("Login", "User");
            //}
            //string tendn = mysession.UserName;
            var tbl_ND = mydb.tblNguoiDung.SingleOrDefault(m => m.UserName == "Nga");

            return View(tbl_ND);
        }

    }
}