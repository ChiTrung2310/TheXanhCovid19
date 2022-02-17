using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheXanhCovid19.Areas.Admin.Data;
using TheXanhCovid19.Common;

namespace TheXanhCovid19.Areas.Admin.Controllers
{
    public class InformationADController : BaseController
    {
        TheXanhCovid19DBContext mydbAD = new TheXanhCovid19DBContext();

        // GET: Admin/InformationAD
        public ActionResult Index()
        {
            var mysessionAD = (UserLogin) Session[TheXanhCovid19.Common.CommonConstantsAD.ADMIN_SESSION];
            //nếu chưa đăng nhập thì không thể vào được trang thông tin của Admin
            if (mysessionAD == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string tendn = mysessionAD.UserName;

            var tbl_ND = mydbAD.tblQuanTri.SingleOrDefault(m => m.UserName == tendn);
            return View(tbl_ND);
        }


        /// <summary>
        /// View cho Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var user = new UserDAO().ViewDetailAD(id);
            return View(user);
        }


        [HttpPost]
        public ActionResult Edit(tblQuanTri user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();

                if (!string.IsNullOrEmpty(user.MatKhau))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(user.MatKhau);
                    user.MatKhau = encryptedMD5Pas;
                }


                var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                result = dao.UpdateAD(user);

                if (result)
                {
                    SetAlert("Cập nhật thông tin người quản trị thành công", "success");
                    return RedirectToAction("Index", "InformationAD");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin AD không thành công");
                }
            }
            SetAlert("Cập nhật thông tin người quản trị thất bại", "error");
            return View("Edit");

        }


        /// <summary>
        /// Đã bỏ Authorize nếu cần hãy thêm lại
        /// </summary>
        /// <returns></returns>
        
        public ActionResult ChangePassword(int id)
        {
            var user = new UserDAO().ViewDetailAD(id);
            return View(user);
            
        }

        [HttpPost]
        public ActionResult ChangePassword(tblQuanTri qT)
        {
            if (ModelState.IsValid)
                
            {

                var dao = new UserDAO();

               
                if (!string.IsNullOrEmpty(qT.MatKhau))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(qT.MatKhau);
                    qT.MatKhau = encryptedMD5Pas;
                }

                if (!string.IsNullOrEmpty(qT.ConfirmPass))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(qT.ConfirmPass);
                    qT.ConfirmPass = encryptedMD5Pas;
                }

                if (!string.IsNullOrEmpty(qT.OldPass))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(qT.OldPass);
                    qT.OldPass = encryptedMD5Pas;
                }

                if (!dao.CheckPass(qT.OldPass))
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                    result = dao.UpdateMKAD(qT);

                    if (result)
                    {
                        SetAlert("Đã thay đổi mật khẩu! Sẽ áp dụng mật khẩu mới cho lần đăng nhập sau.", "success");
                        return RedirectToAction("Index", "InformationAD");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thay đổi mật khẩu không thành công");
                    }
                }
                
            }
            SetAlert("Thay đổi mật khẩu thất bại !!! Hãy thử lại", "error");
            return View();
        }
    }
}