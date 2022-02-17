using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheXanhCovid19.Models;
using Model.EF;
using Model.DAO;

namespace TheXanhCovid19.Controllers
{
    public class WSTheXanhController : ApiController
    {
        

        [HttpGet] 
        [Route("api/WSTheXanh/{ID}")]
       
        public NDCovid GetTblNguoiDung(int ID) 
        {
            TheXanhCovid19DBContext my_db = new TheXanhCovid19DBContext();
            var my_model = from A in my_db.tblNguoiDung where A.MaSo == ID
                           select new NDCovid()
                           {
                               Ho = A.Ho, TenDem = A.TenDem, Ten = A.Ten, SoCMND = A.SoCMND, NgaySinh = A.NgaySinh, DienThoai = A.DienThoai, GioiTinh = A.GioiTinh, Email = A.Email, DiaChi = A.DiaChi, QueQuan = A.QueQuan, SoMuiTiem = A.SoMuiTiem, LoaiThe = A.LoaiThe, UserName = A.UserName, MatKhau = A.MatKhau
                           };

            return my_model.SingleOrDefault();
        }


        [HttpGet]
        [Route("api/WSTheXanh")]
        public List<NDCovid> GetTblNguoiDung1()
        {
            TheXanhCovid19DBContext my_db = new TheXanhCovid19DBContext();
            var my_model = from A in my_db.tblNguoiDung
                           select new NDCovid()
                           {
                               Ho = A.Ho,
                               TenDem = A.TenDem,
                               Ten = A.Ten,
                               SoCMND = A.SoCMND,
                               NgaySinh = A.NgaySinh,
                               DienThoai = A.DienThoai,
                               GioiTinh = A.GioiTinh,
                               Email = A.Email,
                               DiaChi = A.DiaChi,
                               QueQuan = A.QueQuan,
                               SoMuiTiem = A.SoMuiTiem,
                               LoaiThe = A.LoaiThe,
                               UserName = A.UserName,
                               MatKhau = A.MatKhau
                              
                           };

            return my_model.ToList();
        }



        [HttpPost]
        [Route("api/WSTheXanh")]
        public IHttpActionResult AddPhanAnh(tblPhanAnh phanAnh)
        {
            TheXanhCovid19DBContext my_db = new TheXanhCovid19DBContext();
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new Message(0, "Phản ánh thông tin không thành công. Vui lòng thử lại"));
                }

                my_db.tblPhanAnh.Add(phanAnh);
                my_db.SaveChanges();
            }
            catch (Exception)
            {
                return Ok(new Message(0, "Phản ánh thông tin không thành công. Vui lòng thử lại"));
            }

            return Ok(new Message(1, "Phản ánh thông tin thành công"));
        }



        [HttpPost]
        [Route("api/KhaiBaoYTE")]
        public IHttpActionResult KhaiBaoYTE(tblKhaiBaoYTe KBYT)
        {
            var dao = new KBYTDAO();
            TheXanhCovid19DBContext my_db = new TheXanhCovid19DBContext();
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new Message(0, "Khai báo y tế không thành công. Vui lòng thử lại"));
                }

                my_db.tblKhaiBaoYTe.Add(KBYT);
                my_db.SaveChanges();

            }
            catch (Exception)
            {
                return Ok(new Message(0, "Khai báo y tế không thành công. Vui lòng thử lại"));
            }

            return Ok(new Message(1, "Khai báo y tế thành công"));
        }


    }
}
