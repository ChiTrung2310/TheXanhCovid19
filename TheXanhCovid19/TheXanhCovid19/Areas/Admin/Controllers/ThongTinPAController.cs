using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheXanhCovid19.Areas.Admin.Controllers
{
    public class ThongTinPAController : BaseController
    {
        // GET: Admin/ThongTinPA
        // GET: Admin/ThongTinKhaiBaoYTe

        TheXanhCovid19DBContext db = null;


        public ThongTinPAController()
        {
            db = new TheXanhCovid19DBContext();
        }

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new PADAO();
            var model = dao.ListAllPaing(searchString, page, pageSize);
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
            var KB = new PADAO().ViewDetail(id);
            return View(KB);
        }


        [HttpPost]
        public ActionResult Edit(tblPhanAnh KB)
        {
            if (ModelState.IsValid)
            {
                var dao = new PADAO();

                var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                result = dao.Update(KB);

                if (result)
                {
                    SetAlert("Đã duyệt phản ánh người dùng thành công", "success");
                    return RedirectToAction("Index", "ThongTinPA");
                }
                else
                {
                    ModelState.AddModelError("", "Duyệt phản ánh không thành công");
                }
            }
            SetAlert("Đã duyệt phản ánh người dùng không thành công", "error");
            return View("Edit");

        }



        //---Hàm mới dùng cho hiển thị và duyệt phản ánh hàng loạt
        public ActionResult List()
        {
            List<tblPhanAnh> tblPA = new List<tblPhanAnh>();

            using (TheXanhCovid19DBContext db = new TheXanhCovid19DBContext())
            {
                tblPA = db.tblPhanAnh.Where(x => x.Status == false).ToList();
            }
            return View(tblPA);
        }


        [HttpPost]
        public ActionResult UpdateSelected(string[] ids)
        {
            int[] id = null;
            if (ids != null)
            {
                id = new int[ids.Length];
                int j = 0;
                foreach (string i in ids)
                {
                    int.TryParse(i, out id[j++]);
                }

            }
            if (id != null && id.Length > 0)
            {
                var dao = new PADAO();
                var result = true;

                List<tblPhanAnh> allSe = new List<tblPhanAnh>();
                using (TheXanhCovid19DBContext db = new TheXanhCovid19DBContext())
                {
                    allSe = db.tblPhanAnh.Where(a => id.Contains(a.ID)).ToList();
                    foreach (var i in allSe)
                    {
                        result = dao.UpdateFast(i);
                    }
                    db.SaveChanges();
                }
            }
            SetAlert("Đã duyệt phản ánh thành công", "success");
            return RedirectToAction("List");
        }
    }
}