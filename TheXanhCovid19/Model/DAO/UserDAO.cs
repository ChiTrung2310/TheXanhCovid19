using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
    public class UserDAO
    {
        TheXanhCovid19DBContext db = null;


        public UserDAO()
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
        /// Lấy ra tất cả người dùng có trong bảng người dùng
        /// </summary>
        /// <returns></returns>
        public IEnumerable<tblNguoiDung> ListAllPaing(string searchString, int page, int pageSize)
        {
            IQueryable<tblNguoiDung> model = db.tblNguoiDung;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Ten.Contains(searchString) || x.SoCMND.Contains(searchString));
            }
            return model.OrderByDescending(x => x.NguoiTao).ToPagedList(page, pageSize); // - có thể lỗi chỗ này
        }


        /// <summary>
        /// Thêm mới người dùng === có thể lỗi ở đây
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert1(tblNguoiDung entity)
        {
            db.tblNguoiDung.Add(entity);
            db.SaveChanges();
            return entity.MaSo;
        }


        /// <summary>
        /// Hàm cập nhật người dùng sử dụng từ phía Admin vì hàm này có thể reset mật khẩu cho người dùng khi quên mật khẩu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(tblNguoiDung entity)
        {
            try
            {
                var user = db.tblNguoiDung.Find(entity.MaSo);
                user.Ho = entity.Ho;
                user.TenDem = entity.TenDem;
                user.Ten = entity.Ten;

                if (!string.IsNullOrEmpty(entity.OldPass))
                {
                    user.OldPass = entity.MatKhau;  // có thể lỗi chổ này vì khi update mật khẩu xác nhận sẽ tự nhận giá trị là mật khẩu dùng cho việc đổi mật khẩu
                }
       

                if (!string.IsNullOrEmpty(entity.MatKhau))
                {
                    user.MatKhau = entity.MatKhau;
                }

                if (!string.IsNullOrEmpty(entity.ConfirmPass))
                {
                    user.ConfirmPass = entity.ConfirmPass;
                }
            

                user.GioiTinh = entity.GioiTinh;
                user.NgaySinh = entity.NgaySinh;
                user.QueQuan = entity.QueQuan;
                user.DiaChi = entity.DiaChi;
                user.DienThoai = entity.DienThoai;
                user.Email = entity.Email;
                user.SoCMND = entity.SoCMND;

                user.SoMuiTiem = entity.SoMuiTiem;
                user.LoaiThe = entity.LoaiThe;

                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }



        /// <summary>
        /// Hàm dùng để cập nhật thông tin người dùng || Không cập nhật mật khẩu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateND(tblNguoiDung entity)
        {
            try
            {
                var user = db.tblNguoiDung.Find(entity.MaSo);
                user.Ho = entity.Ho;
                user.TenDem = entity.TenDem;
                user.Ten = entity.Ten;
              
                user.GioiTinh = entity.GioiTinh;
                user.NgaySinh = entity.NgaySinh;
                user.QueQuan = entity.QueQuan;
                user.DiaChi = entity.DiaChi;
                user.DienThoai = entity.DienThoai;
                user.Email = entity.Email;
                user.SoCMND = entity.SoCMND;

                user.SoMuiTiem = entity.SoMuiTiem;
                user.LoaiThe = entity.LoaiThe;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        /// <summary>
        /// Hàm dùng để update thông tin cho Admin
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateAD(tblQuanTri entity)
        {
            try
            {
                var user = db.tblQuanTri.Find(entity.MaQT);
                user.Ho = entity.Ho;
                user.TenDem = entity.TenDem;
                user.Ten = entity.Ten;

                user.GioiTinh = entity.GioiTinh;
                user.NgaySinh = entity.NgaySinh;
                user.QueQuan = entity.QueQuan;
                user.DiaChi = entity.DiaChi;
                user.DienThoai = entity.DienThoai;
                user.Email = entity.Email;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        /// <summary>
        /// Hàm dùng để thay đổi mật khẩu cho Admin
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateMKAD(tblQuanTri entity)
        {
            try
            {
                var user = db.tblQuanTri.Find(entity.MaQT);
                user.Ho = entity.Ho;
                user.TenDem = entity.TenDem;
                user.Ten = entity.Ten;
                user.OldPass = entity.MatKhau;
                user.MatKhau = entity.MatKhau;
                user.ConfirmPass = entity.ConfirmPass;
                user.GioiTinh = entity.GioiTinh;
                user.NgaySinh = entity.NgaySinh;
                user.QueQuan = entity.QueQuan;
                user.DiaChi = entity.DiaChi;
                user.DienThoai = entity.DienThoai;
                user.Email = entity.Email;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        /// <summary>
        /// Hàm thay đổi mật khẩu cho người dùng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateMKND(tblNguoiDung entity)
        {
            try
            {
                var user = db.tblNguoiDung.Find(entity.MaSo);
                user.Ho = entity.Ho;
                user.TenDem = entity.TenDem;
                user.Ten = entity.Ten;
                user.OldPass = entity.MatKhau;
                user.MatKhau = entity.MatKhau;
                user.ConfirmPass = entity.ConfirmPass;
                user.GioiTinh = entity.GioiTinh;
                user.NgaySinh = entity.NgaySinh;
                user.QueQuan = entity.QueQuan;
                user.DiaChi = entity.DiaChi;
                user.DienThoai = entity.DienThoai;
                user.Email = entity.Email;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        /// <summary>
        /// Lấy ra ID đăng nhập cho admin
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public tblQuanTri GetById(string userName)
        {
            return db.tblQuanTri.SingleOrDefault(x=>x.UserName == userName);
        }


        /// <summary>
        /// Lấy ra ID người dùng để đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public tblNguoiDung GetByIdND(string userName)
        {
            return db.tblNguoiDung.SingleOrDefault(x => x.UserName == userName);
        }


        /// <summary>
        /// Lấy ID để Update dữ liệu cho người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tblNguoiDung ViewDetail(int id)
        {
            return db.tblNguoiDung.Find(id);
        }

        /// <summary>
        /// Lấy ID để Update mật khẩu cho người dùng | Khác biệt ở chỗ nếu không truyền tham số gì vào thì không thể mở trang
        /// và nếu đã truyền tham số nhưng chưa đăng nhập thì cũng không thể mở trang.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tblNguoiDung ViewDetailMK(int? id) // cần chỉnh lại chỗ này
        {
            return db.tblNguoiDung.Find(id);
        }



        /// <summary>
        /// Lấy ID để Update dữ liệu cho Admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tblQuanTri ViewDetailAD(int id)
        {
            return db.tblQuanTri.Find(id);
        }


        public int Login(string userName, string passWord)
        {
            var result = db.tblQuanTri.SingleOrDefault(x => x.UserName == userName);
            if(result == null )
            {
                return 0;
            }
            else
            {
                if (result.MatKhau == passWord)
                    return 1;
                
                else
                    return -2;
            }
        }


        public int LoginND(string userName, string passWord)
        {
            var result = db.tblNguoiDung.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.MatKhau == passWord)
                    return 1;

                else
                    return -2;
            }
        }


        public bool Delete(int id)
        {
            try
            {
                var user = db.tblNguoiDung.Find(id);
                db.tblNguoiDung.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }


        /// <summary>
        /// Hàm kiểm tra xem UserName có tồn tại hay chưa
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckUserName(string userName)
        {
            return db.tblNguoiDung.Count(x => x.UserName == userName) > 0;
        }



        /// <summary>
        /// Hàm kiểm tra xem mật khẩu có đúng khi đổi mật khẩu cho admin
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckPass(string pass)
        {
            return db.tblQuanTri.Count(x => x.MatKhau == pass) > 0;
        }



        /// <summary>
        /// Hàm kiểm tra xem mật khẩu có đúng khi đổi mật khẩu cho người dùng
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckPassND(string pass)
        {
            return db.tblNguoiDung.Count(x => x.MatKhau == pass) > 0;
        }



        /// <summary>
        /// Hàm kiểm tra xem email có tồn tại hay chưa
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckEmail(string SDT)
        {
            return db.tblNguoiDung.Count(x => x.DienThoai == SDT) > 0;
        }


    }
}
