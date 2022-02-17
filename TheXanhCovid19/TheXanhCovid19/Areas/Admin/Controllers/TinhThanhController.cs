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
    public class TinhThanhController : BaseController
    {
        // GET: Admin/TinhThanh
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new TTDAO();
            var model = dao.ListAllPaing(searchString, page, pageSize);
            ViewBag.SearchString = searchString; // giữ lại nội dung tìm kiếm trên ô textbox
            return View(model);
        }

        /// <summary>
        /// View cho Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id) // có thể lỗi kiểu int ở đây
        {
            var tt = new TTDAO().ViewDetail(id);
            return View(tt);
        }


        /// <summary>
        /// Hàm cập nhật dữ liệu cho tỉnh thành
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(tblTinhThanh tt)
        {
            if (ModelState.IsValid)
            {
                var dao = new TTDAO();

                var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                result = dao.Update(tt);

                if (result)
                {
                    SetAlert("Cập nhật dữ liệu thành công", "success");
                    return RedirectToAction("Index", "TinhThanh");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật dữ liệu không thành công");
                }
            }
            return View("Edit");

        }

    }
}