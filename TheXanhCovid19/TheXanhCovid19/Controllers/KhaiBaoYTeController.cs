using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheXanhCovid19.Areas.Admin.Controllers;
using Model.DAO;
using System.Xml.Linq;
using TheXanhCovid19.Models;

namespace TheXanhCovid19.Controllers
{
    public class KhaiBaoYTeController : Controller
    {
        // GET: KhaiBaoYTe
        public ActionResult Index()
        {
            var mysession = (UserLogin)Session[TheXanhCovid19.Common.CommonConstants.USER_SESSION];
            // nếu chưa đăng nhập thì không thể vào được trang khai báo y tế
            if (mysession == null)
            {
                return RedirectToAction("Login", "User");
            }
            SetViewBag();
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

        public void SetViewBag(string selectedID = null)
        {
            var dao = new TheXanhDAO();
            ViewBag.TinhThanh = new SelectList(dao.ListAll1(), "TinhTP", "TinhTP", selectedID);
        }



        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(tblKhaiBaoYTe KB)
        {
            if (ModelState.IsValid)
            {
                var dao = new KBYTDAO();

                var user = new tblKhaiBaoYTe();
                user.HoTen = KB.HoTen;
                user.SoCMND = KB.SoCMND;
                user.GioiTinh = KB.GioiTinh;
                user.NgaySinh = KB.NgaySinh;
                user.DienThoai = KB.DienThoai;
                user.SoTheBHYT = KB.SoTheBHYT;
                user.Email = KB.Email;

                if (!string.IsNullOrEmpty(KB.ProvinceID))
                {
                    user.ProvinceID = KB.ProvinceID;
                }

                if (!string.IsNullOrEmpty(KB.DistrictID))
                {
                    user.DistrictID = KB.DistrictID;
                }

                if (!string.IsNullOrEmpty(KB.WardsID))
                {
                    user.WardsID = KB.WardsID;
                }

                user.DiaDiemCuThe = KB.DiaDiemCuThe;
                user.TiepXucKhuVuc = KB.TiepXucKhuVuc;
                user.TiepXucNguoiBenh = KB.TiepXucNguoiBenh;
                user.TiepXucNguoiBenh = KB.TrieuChungNhiemBenh;


                int id = dao.Insert(KB);

                if (id > 0)
                {
                    SetAlert("Khai báo y tế thành công", "success");
                    return RedirectToAction("Index", "Information");
                }
                else
                {
                    ModelState.AddModelError("", "Khai báo y tế không thành công");
                }
            }
            SetAlert("Khai báo y tế không thành công do chưa điền các trường bắt buộc", "error");
            return RedirectToAction("Index", "KhaiBaoYTe");

        }


        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_Data.xml"));

            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel province = null;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }


        public JsonResult LoadDistrict(int provinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_Data.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceID);

            var list = new List<DistrictModel>();
            DistrictModel district = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                district = new DistrictModel();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = int.Parse(xElement.Attribute("id").Value);
                list.Add(district);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }


        //public JsonResult LoadDistrict(string provinceName)
        //{
        //    var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_Data.xml"));

        //    var xElement = xmlDoc.Element("Root").Elements("Item")
        //        .Single(x => x.Attribute("type").Value == "province" && x.Attribute("value").Value == provinceName);

        //    var list = new List<DistrictModel>();
        //    DistrictModel district = null;
        //    foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
        //    {
        //        district = new DistrictModel();
        //        district.ID = int.Parse(item.Attribute("id").Value);
        //        district.Name = item.Attribute("value").Value;
        //        district.ProvinceID = int.Parse(xElement.Attribute("id").Value);
        //        list.Add(district);

        //    }
        //    return Json(new
        //    {
        //        data = list,
        //        status = true
        //    });
        //}



        //public JsonResult LoadWards(string DistrictName)
        //{
        //    var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_Data.xml"));

        //    var xElement = xmlDoc.Element("Root").Elements("Item").Elements("Item")
        //        .Single(x => x.Attribute("type").Value == "district" && x.Attribute("value").Value == DistrictName);

        //    var list = new List<WardsModel>();
        //    WardsModel Wards = null;
        //    foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "precinct"))
        //    {
        //        Wards = new WardsModel();
        //        Wards.ID = int.Parse(item.Attribute("id").Value);
        //        Wards.Name = item.Attribute("value").Value;
        //        Wards.DistrictID = int.Parse(xElement.Attribute("id").Value);
        //        list.Add(Wards);

        //    }
        //    return Json(new
        //    {
        //        data = list,
        //        status = true
        //    });
        //}



        public JsonResult LoadWards(int DistrictID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_Data.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item").Elements("Item")
                .Single(x => x.Attribute("type").Value == "district" && int.Parse(x.Attribute("id").Value) == DistrictID);

            var list = new List<WardsModel>();
            WardsModel Wards = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "precinct"))
            {
                Wards = new WardsModel();
                Wards.ID = int.Parse(item.Attribute("id").Value);
                Wards.Name = item.Attribute("value").Value;
                Wards.DistrictID = int.Parse(xElement.Attribute("id").Value);
                list.Add(Wards);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }


    }
}