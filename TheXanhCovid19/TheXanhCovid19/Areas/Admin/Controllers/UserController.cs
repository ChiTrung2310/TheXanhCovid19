using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using TheXanhCovid19.Common;
using PagedList;
using System.Configuration;
using Common;

namespace TheXanhCovid19.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDAO();
            var model = dao.ListAllPaing(searchString, page, pageSize);
            ViewBag.SearchString = searchString; // giữ lại nội dung tìm kiếm trên ô textbox
        
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            else if (TempData["MKError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["MKError"].ToString());
            }

            SetViewBag(); // Load dữ liệu các loại thẻ ra Dropdown
            return View();
        }


        /// <summary>
        /// View cho Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var user = new UserDAO().ViewDetail(id);
            return View(user);
        }


        [HttpPost]
        public ActionResult Create(tblNguoiDung user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                 
                // Mã hóa 3 dòng mật khẩu thành mã MD5 tăng độ bảo mật
                var encryptedMD5Pas = Encryptor.MD5Hash(user.MatKhau);
                user.MatKhau = encryptedMD5Pas;

                //var encryptedMD5Pas1 = Encryptor.MD5Hash(user.MatKhau); // có thể lỗi ở chổ này
                user.OldPass = user.MatKhau;  // gán cho mật khẩu cũ là mật khẩu hiện tại dùng để thay đổi mật khẩu sau này

                var encryptedMD5Pas2 = Encryptor.MD5Hash(user.ConfirmPass);
                user.ConfirmPass = encryptedMD5Pas2;


                if (dao.CheckUserName(user.UserName))  // Kiểm tra xem username có tồn tại trên hệ thống hay không! Nếu có tồn tại sẽ không thêm được
                {

                    TempData["CustomError"] = "Tên đăng nhập này đã tồn tại trong hệ thống vui lòng thử tên đăng nhập khác!!!";
                    //ModelState.AddModelError("", "Tên đăng nhập này đã tồn tại trong hệ thống vui lòng thử tên đăng nhập khác!!!");
                    
                }
                else
                {
                    int id = dao.Insert1(user);

                    if (id > 0)
                    {
                        SetAlert("Thêm người dùng thành công", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm người dùng không thành công");
                    }
                }
                
            }
            TempData["MKError"] = "Xác nhận mật khẩu không khớp!!!"; // gán thông báo lỗi 
            SetAlert("Thêm mới người dùng thất bại", "error");
            return RedirectToAction("Create", "User");

        }



        [HttpPost]
        public ActionResult Edit(tblNguoiDung user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();

                if (!string.IsNullOrEmpty(user.MatKhau))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(user.MatKhau);
                    user.MatKhau = encryptedMD5Pas;
                }

                if (!string.IsNullOrEmpty(user.OldPass))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(user.OldPass);
                    user.OldPass = encryptedMD5Pas;
                }

                if (!string.IsNullOrEmpty(user.ConfirmPass))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(user.ConfirmPass);
                    user.ConfirmPass = encryptedMD5Pas;
                }


                var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                result  = dao.Update(user);

                if (result)
                {
                    SetAlert("Cập nhật người dùng thành công", "success");

                    // Khi nào có thay đổi mật khẩu mới thực hiện gửi mail thông báo cho người dùng. Nếu không thay đổi mật khẩu thì không gửi
                    if (!string.IsNullOrEmpty(user.ConfirmPass) && !string.IsNullOrEmpty(user.MatKhau) && !string.IsNullOrEmpty(user.OldPass))
                    {
                        string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/newmk.html"));

                        content = content.Replace("{{CustomerName}}", user.UserName);
                        content = content.Replace("{{Phone}}", user.DienThoai);
                        content = content.Replace("{{Email}}", user.Email);
                        content = content.Replace("{{CMND}}", user.SoCMND);

                        var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                        new MailHelper().SendMail(user.Email, "Đặt lại mật khẩu", content);
                        new MailHelper().SendMail(toEmail, "Đặt lại mật khẩu", content);
                    }
                  
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật người dùng không thành công");
                }
            }
            SetAlert("Cập nhật người dùng thất bại", "error");
            return View("Edit");

        }


        public void SetViewBag(int selectedID = 0)
        {
            var dao = new TheXanhDAO();
            ViewBag.LoaiThe = new SelectList(dao.ListAll(), "LoaiThe", "LoaiThe", selectedID);
        }


        /// <summary>
        /// Hàm xóa người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDAO().Delete(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Hàm đăng xuất của admin
        /// </summary>
        /// <returns></returns>
        public ActionResult LogoutAD()
        {
            Session[CommonConstantsAD.ADMIN_SESSION] = null;
            return Redirect("/");

        }

    }
}