using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class DLDAO
    {
        TheXanhCovid19DBContext db = null;


        public DLDAO()
        {
            db = new TheXanhCovid19DBContext();
        }


        public IEnumerable<tblThongTinTiemChung> ListAllPaing(int page, int pageSize)
        {
            
            return db.tblThongTinTiemChung.OrderByDescending(x => x.MaTC).ToPagedList(page, pageSize);
        }





        public bool Update(tblThongTinTiemChung entity)
        {
            try
            {
                var tc = db.tblThongTinTiemChung.Find(entity.MaTC);
                tc.DoiTuongDKTiem = entity.DoiTuongDKTiem;
                tc.SoMuiTiemHomQua = entity.SoMuiTiemHomQua;
                tc.SoMuiTiemToanQuoc = entity.SoMuiTiemToanQuoc;
                tc.DoBaoPhu = entity.DoBaoPhu;
                tc.NgayCapNhat = DateTime.Now;
                tc.NguoiCapNhat = entity.NguoiCapNhat;
                tc.GhiChu = entity.GhiChu;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<tblThongTinTiemChung> ttTC()
        {
            return db.tblThongTinTiemChung.ToList();
        }



        /// <summary>
        /// Lấy ID để Update dữ liệu cho thông tin tiêm chủng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tblThongTinTiemChung ViewDetail(int id)
        {
            return db.tblThongTinTiemChung.Find(id);
        }

    }
}
