using Model.DAO;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TheXanhCovid19.Models;
using ZXing.QrCode;

namespace TheXanhCovid19.Controllers
{
    public class QRController : Controller
    {
        // GET: QR
        public ActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Index(string qrcode, string qrcode1, string qrcode2, string qrcode3, string qrcode4, string qrcode5)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qRCodeGenerator.CreateQrCode(qrcode +" | "+ qrcode1 +" | "+ qrcode2 +" | "+ qrcode3 +" | "+ qrcode4 +" | "+ qrcode5, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                using (Bitmap bitmap = qrCode.GetGraphic(20))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
 ;
            }
            return View();
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