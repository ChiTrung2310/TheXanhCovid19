using Model.DAO;
using Model.EF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheXanhCovid19.Areas.Admin.Controllers
{
    public class ThongTinKhaiBaoYTeController : BaseController
    {
        // GET: Admin/ThongTinKhaiBaoYTe

        TheXanhCovid19DBContext db = null;


        public ThongTinKhaiBaoYTeController()
        {
            db = new TheXanhCovid19DBContext();
        }

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new KBYTDAO();
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
                    SetAlert("Đã duyệt khai báo y tế thành công", "success");
                    return RedirectToAction("Index", "ThongTinKhaiBaoYTe");
                }
                else
                {
                    ModelState.AddModelError("", "Duyệt khai báo y tế không thành công");
                }
            }
            return View("Edit");

        }


        [HttpPost]
        public ActionResult Index(tblKhaiBaoYTe KB)
        {
            if (ModelState.IsValid)
            {
                var dao = new KBYTDAO();
  
                var result = true;  // -- cần chỉnh như này tại vì mặc định result nó là false nên sẽ không chạy update được
                result = dao.Update(KB);

                if (result)
                {
                    SetAlert("Đã duyệt khai báo y tế thành công", "success");
                    return RedirectToAction("Index", "ThongTinKhaiBaoYTe");
                }
                else
                {
                    ModelState.AddModelError("", "Duyệt khai báo y tế không thành công");
                }
            }
            return View("Edit");

        }


        //---Hàm mới dùng cho hiển thị và duyệt khai báo y tế hàng loạt
        public ActionResult List()
        {
            List<tblKhaiBaoYTe> tblKBYT = new List<tblKhaiBaoYTe>();

            using(TheXanhCovid19DBContext db = new TheXanhCovid19DBContext())
            {
                tblKBYT = db.tblKhaiBaoYTe.Where(x => x.Status == false).ToList();
            }
            return View(tblKBYT);
        }


        [HttpPost]
        public ActionResult DeleteSelected(string[] ids)
        {
            int[] id = null;
            if(ids != null)
            {
                id = new int[ids.Length];
                int j = 0;
                foreach(string i in ids)
                {
                    int.TryParse(i, out id[j++]);
                }

            }
            if(id != null && id.Length > 0)
            {
                var dao = new KBYTDAO();
                var result = true; 
              
                List<tblKhaiBaoYTe> allSe = new List<tblKhaiBaoYTe>();
                using(TheXanhCovid19DBContext db = new TheXanhCovid19DBContext())
                {
                    allSe = db.tblKhaiBaoYTe.Where(a => id.Contains(a.ID)).ToList();
                    foreach(var i in allSe)
                    {
                        result = dao.UpdateFast(i);
                    }
                    db.SaveChanges();
                }
            }
            SetAlert("Đã duyệt khai báo y tế thành công", "success");
            return RedirectToAction("List");
        }

    }
}