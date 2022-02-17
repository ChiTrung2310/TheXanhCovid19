using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class PADAO
    {

        TheXanhCovid19DBContext db = null;


        public PADAO()
        {
            db = new TheXanhCovid19DBContext();
        }

        public int Insert(tblPhanAnh entity)
        {
            db.tblPhanAnh.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }


        public IEnumerable<tblPhanAnh> ListAllPaing(string searchString, int page, int pageSize)
        {
            IQueryable<tblPhanAnh> model = db.tblPhanAnh;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.NoiDung.Contains(searchString) || x.TieuDe.Contains(searchString) );
            }
            return model.OrderBy(x => x.NgayPhanAnh).Where(x => x.Status == false).ToPagedList(page, pageSize);// mấy chỗ khác nếu muốn hiển thị đã duyệt hay chưa thì kiểm tra chỗ này
        }


        /// <summary>
        /// Lấy ID để duyệt phản ánh của người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tblPhanAnh ViewDetail(int id)
        {
            return db.tblPhanAnh.Find(id);
        }


        /// <summary>
        /// Duyệt phản ánh từ phía người dùng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(tblPhanAnh entity)
        {
            try
            {
                var KB = db.tblPhanAnh.Find(entity.ID);

                KB.HoTen = entity.HoTen;
                KB.SoCMND = entity.SoCMND;
                KB.DienThoai = entity.DienThoai;
                KB.Email = entity.Email;
                KB.TieuDe = entity.TieuDe;
                KB.NoiDung = entity.NoiDung;
                KB.NgayPhanAnh = entity.NgayPhanAnh;
                KB.Status = entity.Status;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public bool UpdateFast(tblPhanAnh entity)
        {
            try
            {
                var KB = db.tblPhanAnh.Find(entity.ID);

                KB.HoTen = entity.HoTen;
                KB.SoCMND = entity.SoCMND;
                KB.DienThoai = entity.DienThoai;
                KB.Email = entity.Email;
                KB.TieuDe = entity.TieuDe;
                KB.NoiDung = entity.NoiDung;
                KB.NgayPhanAnh = entity.NgayPhanAnh;
                KB.Status = true;

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
