using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class TTDAO
    {
        TheXanhCovid19DBContext db = null;


        public TTDAO()
        {
            db = new TheXanhCovid19DBContext();
        }

        public int Insert(tblQuanTri entity)
        {
            db.tblQuanTri.Add(entity);
            db.SaveChanges();
            return entity.MaQT;
        }


        /// <summary>
        /// Lấy ra tất cả tỉnh thành có trong bảng tỉnh thành
        /// </summary>
        /// <returns></returns>
        public IEnumerable<tblTinhThanh> ListAllPaing(string searchString, int page, int pageSize)
        {
            IQueryable<tblTinhThanh> model = db.tblTinhThanh;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TinhTP.Contains(searchString));
            }
            return model.OrderBy(x => x.MaTT).ToPagedList(page, pageSize);
        }




        public bool Update(tblTinhThanh entity)
        {
            try
            {
                var tt = db.tblTinhThanh.Find(entity.MaTT);
                tt.DuKienTC = entity.DuKienTC;
                tt.PhanBoTT = entity.PhanBoTT;
                tt.DanSo = entity.DanSo;
                tt.NgayCapNhat = DateTime.Now;
                tt.SoLieuDaTiem = entity.SoLieuDaTiem;
                tt.TiLe = entity.TiLe;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        /// <summary>
        /// Lấy ID để Update dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tblTinhThanh ViewDetail(string id) // -- có thể lỗi kiểu int ở đây
        {
            return db.tblTinhThanh.SingleOrDefault(x => x.MaTT == id); 
        }

        public List<tblTinhThanh> TT()
        {
            return db.tblTinhThanh.ToList();
        }
    }

}
