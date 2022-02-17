using BotDetect.Web.Mvc;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheXanhCovid19.Controllers
{
    public class PhanAnhController : Controller
    {
        // GET: PhanAnh
        public ActionResult Index()
        {
            return View();
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


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Create(tblPhanAnh KB)
        {
            if (ModelState.IsValid)
            {
                var dao = new PADAO();

                var PA = new tblPhanAnh();
                PA.HoTen = KB.HoTen;
                PA.SoCMND = KB.SoCMND;
                PA.DienThoai = KB.DienThoai;
                PA.Email = KB.Email;
                PA.TieuDe = KB.TieuDe;
                PA.NoiDung = KB.NoiDung;
                PA.NgayPhanAnh = DateTime.Now;

                int id = dao.Insert(KB);

                if (id > 0)
                {
                    SetAlert("Đã gửi thông tin phản hồi", "success");
                    return RedirectToAction("Create", "PhanAnh");
                }
                else
                {
                    ModelState.AddModelError("", "Phản hồi không thành công");
                }
            }
            SetAlert("Gửi thông tin phản hồi không thành công !!!", "error");
            return RedirectToAction("Create", "PhanAnh");

        }
    }
}