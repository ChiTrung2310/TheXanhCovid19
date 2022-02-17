using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheXanhCovid19.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        TheXanhCovid19DBContext context = new TheXanhCovid19DBContext();
        // GET: Admin/Home
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var dao = new DLDAO();
            var model = dao.ListAllPaing(page, pageSize);

            return View(model);
        }



        public ActionResult GetDataLine()
        {
            TheXanhCovid19DBContext context = new TheXanhCovid19DBContext();


            var query = context.tblTinhThanh.Include("tblTinhThanh")
                   .GroupBy(p => p.TinhTP)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.SoLieuDaTiem), trung = g.Sum(w => w.DuKienTC) }).ToList();

            return Json(query, JsonRequestBehavior.AllowGet);
        }




        public ActionResult GetData()
        {
            int thexanh = context.tblNguoiDung.Where(x => x.LoaiThe == "Thẻ Xanh").Count();
            int thevang = context.tblNguoiDung.Where(x => x.LoaiThe == "Thẻ Vàng").Count();
            int thedo = context.tblNguoiDung.Where(x => x.LoaiThe == "Thẻ Đỏ").Count(); ;

            Ratio obj = new Ratio();
            obj.TheXanh = thexanh;
            obj.TheVang = thevang;
            obj.TheDo = thedo;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public class Ratio
        {
            public int TheXanh { get; set; }

            public int TheVang { get; set; }

            public int TheDo { get; set; }
        }

    }
}