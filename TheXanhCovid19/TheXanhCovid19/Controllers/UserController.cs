using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TheXanhCovid19.Common;
using TheXanhCovid19.Models;

namespace TheXanhCovid19.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.LoginND(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByIdND(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.MaSo;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/Information/Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không đúng");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị vô hiệu hóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");

                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return View(model);
        }


        public ActionResult Login()
        {

            return View();
        }


        /// <summary>
        /// Hàm đăng xuất của người dùng
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
            
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        // Hàm quên mật khẩu và gửi tin nhắn xác thực qua email.
        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            string resetCode = Guid.NewGuid().ToString();

            var verifyUrl = "/User/ResetPassword/" + resetCode;

            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            //get user details from database.
            using (var context = new TheXanhCovid19DBContext())
            {
                var getUser = (from s in context.tblNguoiDung where s.Email == EmailID select s).FirstOrDefault();
                if (getUser != null)
                {
                    getUser.ResetPasswordCode = resetCode;

                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 

                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();

                    var subject = "Yêu cầu đặt lại mật khẩu";
                    var body = "Xin chào " + getUser.Ten + ", <br/> Gần đây bạn đã yêu cầu đặt lại mật khẩu cho tài khoản của mình. Nhấp vào liên kết bên dưới để đặt lại mật khẩu cho bạn " +

                         " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
                         "Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.<br/><br/> Xin cảm ơn";

                    SendEmail(getUser.Email, body, subject);

                    ViewBag.Message = "Liên kết đặt lại mật khẩu đã được gửi đến email của bạn. Vui lòng kiểm tra!";
                    SetAlert("Liên kết đặt lại mật khẩu đã được gửi đến email của bạn. Vui lòng kiểm tra!", "success");
                }
                else
                {
                    ViewBag.Message = "Địa chỉ email không đúng!!!";
                    return View();
                }
            }

            //return View();
            return Redirect("/Home/Index");
        }


        // Chỗ này làm hàm reset mật khẩu cho người dùng
        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                //return HttpNotFound(); Thông báo lỗi
                return RedirectToAction("Login", "User");
            }

            using (var context = new TheXanhCovid19DBContext())
            {
                var user = context.tblNguoiDung.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    //return HttpNotFound(); Thông báo lỗi
                    return RedirectToAction("Login", "User");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (var context = new TheXanhCovid19DBContext())
                {
                    var user = context.tblNguoiDung.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        // mã hóa mật khẩu thành chuỗi MD5
                        if (!string.IsNullOrEmpty(user.MatKhau))
                        {
                            var encryptedMD5Pas = Encryptor.MD5Hash(model.NewPassword);
                            model.NewPassword = encryptedMD5Pas;
                        }
                        //you can encrypt password here, we are not doing it
                        user.OldPass = model.NewPassword; // có thể lỗi chỗ này 
                        user.MatKhau = model.NewPassword;
                        user.ConfirmPass = model.NewPassword; // có thể lỗi chỗ này
                        //make resetpasswordcode empty string now
                        user.ResetPasswordCode = "";
                        //to avoid validation issues, disable it
                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();
                        message = "Đã cập nhật mật khẩu mới thành công!!!";
                        SetAlert("Đã cập nhật mật khẩu mới thành công. Hãy đăng nhập với mật khẩu mới nhất!", "success");
                    }
                }
            }
            else
            {
                message = "Xác nhận mật khẩu không khớp! Hãy nhập lại.";
                SetAlert("Lấy lại mật khẩu không thành công!", "error");
                return View();
            }
            ViewBag.Message = message;
            return Redirect("/Home/Index");
        }



        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage("phanct2310@gmail.com", emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("phanct2310@gmail.com", "TriNguyen123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }

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

    }
}