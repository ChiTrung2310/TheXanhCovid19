using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheXanhCovid19.Common;

namespace TheXanhCovid19.Areas.Admin.Controllers
{
    public class TheXanhController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDAO();
            var model = dao.ListAllPaing(searchString, page, pageSize);
            ViewBag.SearchString = searchString; // giữ lại nội dung tìm kiếm trên ô textbox
            return View(model);
        }


        public void SetViewBag(int selectedID = 0)
        {
            var dao = new TheXanhDAO();
            ViewBag.LoaiThe = new SelectList(dao.ListAll(), "LoaiThe", "LoaiThe", selectedID);
        }


        [HttpGet]
        /// <summary>
        /// View cho Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        { 
            var user = new UserDAO().ViewDetail(id);
            SetViewBag(); // chỗ này có thể lỗi nè 
            return View(user);
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


                var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                result = dao.UpdateND(user);

                if (result)
                {

                    SetAlert("Cập nhật dữ liệu thẻ xanh thành công", "success");
                    return RedirectToAction("Index", "TheXanh");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật dữ liệu thẻ xanh không thành công");
                }
            }
            return View("Edit");

        }

    }
}