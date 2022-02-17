using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheXanhCovid19.Areas.Admin.Controllers
{
    public class DuyetKBYTController : BaseController
    {
        // GET: Admin/DuyetKBYT
        TheXanhCovid19DBContext db = null;


        public DuyetKBYTController()
        {
            db = new TheXanhCovid19DBContext();
        }

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new KBYTDAO();
            var model = dao.ListAllPaingCheck(searchString, page, pageSize);
            ViewBag.SearchString = searchString; // giữ lại nội dung tìm kiếm trên ô textbox
            return View(model);
        }


        /// <summary>
        /// View cho Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var KB = new KBYTDAO().ViewDetail(id);
            return View(KB);
        }

        [HttpPost]
        public ActionResult Edit(tblKhaiBaoYTe KB)
        {
            if (ModelState.IsValid)
            {
                var dao = new KBYTDAO();

                var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                result = dao.Update(KB);

                if (result)
                {
                    SetAlert("Hoàn tất kiểm tra", "success");
                    return RedirectToAction("Index", "ThongTinKhaiBaoYTe");
                }
                else
                {
                    ModelState.AddModelError("", "Duyệt khai báo y tế không thành công");
                }
            }
            return View("Edit");

        }
    }
}