using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheXanhCovid19.Areas.Admin.Controllers
{
    public class DuLieuController : BaseController
    {
        // GET: Admin/DuLieu
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var dao = new DLDAO();
            var model = dao.ListAllPaing(page, pageSize);
           
            return View(model);
        }

        /// <summary>
        /// View cho Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var tt = new DLDAO().ViewDetail(id);
            return View(tt);
        }


        /// <summary>
        /// Hàm cập nhật dữ liệu cho tỉnh thành
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(tblThongTinTiemChung tc)
        {
            if (ModelState.IsValid)
            {
                var dao = new DLDAO();

                var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                result = dao.Update(tc);

                if (result)
                {
                    SetAlert("Cập nhật dữ liệu thành công", "success");
                    return RedirectToAction("Index", "DuLieu");
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