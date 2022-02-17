using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing.QrCode;
using System.Dynamic;
using TheXanhCovid19.Models;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using TheXanhCovid19.Common;


namespace TheXanhCovid19.Controllers
{
    public class InformationController : Controller
    {
        TheXanhCovid19DBContext mydb = new TheXanhCovid19DBContext();


        // GET: Information
        public ActionResult Index()
        {
            var mysession = (UserLogin)Session[TheXanhCovid19.Common.CommonConstants.USER_SESSION];

            //nếu chưa đăng nhập thì không thể vào được trang thông tin người dùng
            if (mysession == null)
            {
                return RedirectToAction("Login", "User");
            }
            string tendn = mysession.UserName;
            var tbl_ND = mydb.tblNguoiDung.SingleOrDefault(m => m.UserName == tendn);


            return View(tbl_ND);
        }


        /// <summary>
        /// Hàm hiển thị thông báo cho các hành động từ phía người dùng
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }


        [HttpPost]
        public ActionResult Index(string qrcode)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qRCodeGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                using(Bitmap bitmap = qrCode.GetGraphic(20))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
 ;           }
            return View();
        }




        /// <summary>
        /// View cho Edit thông tin người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var user = new UserDAO().ViewDetail(id);
            return View(user);
        }


        [HttpPost]
        public ActionResult Edit(tblNguoiDung user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();

                var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                result = dao.UpdateND(user);

                if (result)
                {
                    SetAlert("Cập nhật thông tin thành công", "success");
                    return RedirectToAction("Index", "Information");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin người dùng không thành công");
                }
            }
            SetAlert("Cập nhật thông tin thất bại", "error");
            return View("Edit");

        }


        /// <summary>
        /// Hàm thay đổi mật khẩu người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangePassword(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Login", "User");
            }
            var mysession = (UserLogin)Session[TheXanhCovid19.Common.CommonConstants.USER_SESSION];

            //nếu chưa đăng nhập thì không thể vào được trang đổi mật khẩu
            if (mysession == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = new UserDAO().ViewDetailMK(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult ChangePassword(tblNguoiDung ND)
        {
            if (ModelState.IsValid)

            {

                var dao = new UserDAO();


                if (!string.IsNullOrEmpty(ND.MatKhau))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(ND.MatKhau);
                    ND.MatKhau = encryptedMD5Pas;
                }

                if (!string.IsNullOrEmpty(ND.ConfirmPass))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(ND.ConfirmPass);
                    ND.ConfirmPass = encryptedMD5Pas;
                }

                if (!string.IsNullOrEmpty(ND.OldPass))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(ND.OldPass);
                    ND.OldPass = encryptedMD5Pas;
                }

                if (!dao.CheckPassND(ND.OldPass))
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                    result = dao.UpdateMKND(ND);

                    if (result)
                    {
                        SetAlert("Đổi mật khẩu thành công. Sẽ áp dụng mật khẩu mới cho lần đăng nhập sau", "success");
                        return RedirectToAction("Index", "Information");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thay đổi mật khẩu không thành công");
                    }
                }

            }
            SetAlert("Đổi mật khẩu thất bại", "error");
            return View();
        }


    }
}