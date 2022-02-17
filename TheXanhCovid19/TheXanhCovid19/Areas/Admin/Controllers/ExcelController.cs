using LinqToExcel;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheXanhCovid19.Common;

namespace TheXanhCovid19.Areas.Admin.Controllers
{
    public class ExcelController : BaseController
    {

        // GET: Admin/Excel
        private TheXanhCovid19DBContext db = new TheXanhCovid19DBContext();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// This function is used to download excel format.
        /// </summary>
        /// <param name="Path"></param>
        /// <returns>file</returns>
        public FileResult DownloadExcel()
        {
            string path = "/Doc/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }

        [HttpPost]
        public ActionResult UploadExcel(tblNguoiDung users, HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Areas/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Sheet1";
                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<tblNguoiDung>(sheetName) select a;

                    var dao = new UserDAO(); // có thể lỗi 

                   

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.UserName != "" && a.MatKhau != "" && a.Ho != "" && a.Ten != "")
                            {
                                tblNguoiDung TU = new tblNguoiDung();

                                TU.NgayTao = a.NgayTao;
                                TU.NguoiTao = a.NguoiTao;
                                TU.NguoiCapNhat = a.NguoiCapNhat;


                                if (dao.CheckUserName(a.UserName))
                                {
                                    SetAlert("Lỗi do trùng tên đăng nhập! Vui lòng kiểm tra lại file", "error");
                                   
                                }
                                else
                                {

                                    TU.UserName = a.UserName;

                                    // Phần này có thể lỗi
                                    TU.OldPass = a.OldPass;

                                    TU.MatKhau = a.MatKhau;           

                                    TU.ConfirmPass = a.ConfirmPass;

                                    TU.Ho = a.Ho;
                                    TU.TenDem = a.TenDem;
                                    TU.Ten = a.Ten;
                                    TU.NgaySinh = a.NgaySinh;
                                    TU.GioiTinh = a.GioiTinh;
                                    TU.QueQuan = a.QueQuan;
                                    TU.DiaChi = a.DiaChi;
                                    TU.DienThoai = a.DienThoai;
                                    TU.Email = a.Email;
                                    TU.SoCMND = a.SoCMND;
                                    TU.SoMuiTiem = a.SoMuiTiem;
                                    TU.LoaiThe = a.LoaiThe;

                                    db.tblNguoiDung.Add(TU);

                                    db.SaveChanges();
                                }

                            }
                            else
                            {
                               
                                if (a.UserName == "" || a.UserName == null) data.Add("<li> Tên đăng nhập is required </li>");
                                if (a.MatKhau == "" || a.MatKhau == null) data.Add("<li> Mật Khẩu is required </li>");
                                if (a.Ho == "" || a.Ho == null) data.Add("<li> Họ is required </li>");
                                if (a.Ten == "" || a.Ten == null) data.Add("<li> Tên is required </li>");

                                SetAlert("Cập nhật người dùng bằng file excel không thành công do lỗi từ file", "error");
                                return RedirectToAction("Index", "Excel");
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                           
                        }
                    }


                    //deleting excel file from folder
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }

                    SetAlert("Cập nhật thêm người dùng bằng file excel thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    //alert message for invalid file format                  

                    SetAlert("Chỉ nhận dữ liệu từ file Excel !!! Vui lòng chọn file Excel", "warning");
                    return RedirectToAction("Index", "Excel");
                }
            }
            else
            {
                if (FileUpload == null)

                SetAlert("Bạn chưa chọn file !!! Vui lòng chọn file Excel", "warning");
                return RedirectToAction("Index", "Excel");
            }
        }

    }
}
