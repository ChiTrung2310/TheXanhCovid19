using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheXanhCovid19.Areas.Admin.Data;
using TheXanhCovid19.Common;

namespace TheXanhCovid19.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result ==1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.MaQT;
                    Session.Add(CommonConstantsAD.ADMIN_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if(result == 0)
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

            return View("Index");
        }
            
    }
}