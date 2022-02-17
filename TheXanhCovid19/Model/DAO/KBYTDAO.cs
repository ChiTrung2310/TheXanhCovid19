using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class KBYTDAO
    {
        TheXanhCovid19DBContext db = null;


        public KBYTDAO()
        {
            db = new TheXanhCovid19DBContext();
        }

        public int Insert(tblKhaiBaoYTe entity)
        {
            db.tblKhaiBaoYTe.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }


        /// <summary>
        /// Lấy ra tất cả tờ khai có trong bảng khai báo y tế với điều kiện chưa duyệt
        /// </summary>
        /// <returns></returns>
        public IEnumerable<tblKhaiBaoYTe> ListAllPaing(string searchString, int page, int pageSize)
        {
            IQueryable<tblKhaiBaoYTe> model = db.tblKhaiBaoYTe;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTen.Contains(searchString) || x.SoCMND.Contains(searchString) || x.ProvinceID.Contains(searchString) || x.DistrictID.Contains(searchString) || x.WardsID.Contains(searchString) ||  x.DiaDiemCuThe.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).Where(x => x.Status == false).ToPagedList(page, pageSize);// mấy chỗ khác nếu muốn hiển thị đã duyệt hay chưa thì kiểm tra chỗ này
        }



        /// <summary>
        /// Lấy ra tất cả tờ khai có trong bảng khai báo y tế với điều kiện đã duyệt
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<tblKhaiBaoYTe> ListAllPaingCheck(string searchString, int page, int pageSize)
        {
            IQueryable<tblKhaiBaoYTe> model = db.tblKhaiBaoYTe;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTen.Contains(searchString) || x.SoCMND.Contains(searchString) || x.ProvinceID.Contains(searchString) || x.DistrictID.Contains(searchString) || x.WardsID.Contains(searchString) || x.DiaDiemCuThe.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).Where(x => x.Status == true).ToPagedList(page, pageSize);// mấy chỗ khác nếu muốn hiển thị đã duyệt hay chưa thì kiểm tra chỗ này
        }


        /// <summary>
        /// Lấy ID để Update dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tblKhaiBaoYTe ViewDetail(int id)
        {
            return db.tblKhaiBaoYTe.Find(id);
        }



        public bool Update(tblKhaiBaoYTe entity)
        {
            try
            {
                var KB = db.tblKhaiBaoYTe.Find(entity.ID);
                KB.HoTen = entity.HoTen;
                KB.SoCMND = entity.SoCMND;
                KB.GioiTinh = entity.GioiTinh;

                KB.NgaySinh = entity.NgaySinh;
                KB.DienThoai = entity.DienThoai;
                KB.SoTheBHYT = entity.SoTheBHYT;
                KB.Email = entity.Email;
                KB.ProvinceID = entity.ProvinceID;
                KB.DistrictID = entity.DistrictID;
                KB.DiaDiemCuThe = entity.DiaDiemCuThe;
                KB.TiepXucKhuVuc = entity.TiepXucKhuVuc;
                KB.TiepXucNguoiBenh = entity.TiepXucNguoiBenh;
                KB.Status = entity.Status;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        // --- Dùng cho duyệt nhanh khai báo
        public bool UpdateFast(tblKhaiBaoYTe entity)
        {
            try
            {
                var KB = db.tblKhaiBaoYTe.Find(entity.ID);
                KB.HoTen = entity.HoTen;
                KB.SoCMND = entity.SoCMND;
                KB.GioiTinh = entity.GioiTinh;

                KB.NgaySinh = entity.NgaySinh;
                KB.DienThoai = entity.DienThoai;
                KB.SoTheBHYT = entity.SoTheBHYT;
                KB.Email = entity.Email;
                KB.ProvinceID = entity.ProvinceID;
                KB.DistrictID = entity.DistrictID;
                KB.DiaDiemCuThe = entity.DiaDiemCuThe;
                KB.TiepXucKhuVuc = entity.TiepXucKhuVuc;
                KB.TiepXucNguoiBenh = entity.TiepXucNguoiBenh;
                KB.Status = true; // có thể lỗi chổ này

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}
