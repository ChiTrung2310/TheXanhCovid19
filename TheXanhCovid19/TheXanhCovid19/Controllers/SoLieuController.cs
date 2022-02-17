using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheXanhCovid19.Controllers
{
    public class SoLieuController : Controller
    {

        // GET: SoLieu
        //public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        //{
        //    var dao = new TTDAO();
        //    var model = dao.ListAllPaing(searchString, page, pageSize);
        //    ViewBag.SearchString = searchString; // giữ lại nội dung tìm kiếm trên ô textbox
        //    return View(model);
        //}

        public ActionResult Index()
        {
            var dao = new DLDAO();
            var dao1 = new TTDAO();
            ViewBag.DLDAO = dao.ttTC();
            ViewBag.TTDAO = dao1.TT();

            return View();
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
            TheXanhCovid19DBContext context = new TheXanhCovid19DBContext();
            int thexanh = context.tblNguoiDung.Where(x => x.LoaiThe  == "Thẻ Xanh").Count();
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