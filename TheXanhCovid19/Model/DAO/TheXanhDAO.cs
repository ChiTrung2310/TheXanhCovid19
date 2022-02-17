using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class TheXanhDAO
    {
        TheXanhCovid19DBContext db = null;

        public TheXanhDAO()
        {
            db = new TheXanhCovid19DBContext();
        }

        public List<tblLoaiThe> ListAll()
        {
            return db.tblLoaiThe.ToList();
        }



        /// <summary>
        /// Phần này dùng cho Drop Tỉnh thành của khai báo y tế
        /// </summary>
        /// <returns></returns>
        public List<tblTinhThanh> ListAll1()
        {
            return db.tblTinhThanh.ToList();
        }
    }
}
