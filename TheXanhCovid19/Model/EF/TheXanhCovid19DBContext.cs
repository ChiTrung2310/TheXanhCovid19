using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EF
{
    public partial class TheXanhCovid19DBContext : DbContext
    {
        public TheXanhCovid19DBContext()
            : base("name=TheXanhCovid19DBContext")
        {
        }

        public virtual DbSet<tblDKTiem> tblDKTiem { get; set; }
        public virtual DbSet<tblKhaiBaoYTe> tblKhaiBaoYTe { get; set; }
        public virtual DbSet<tblLoaiThe> tblLoaiThe { get; set; }
        public virtual DbSet<tblNguoiDung> tblNguoiDung { get; set; }
        public virtual DbSet<tblQRCode> tblQRCode { get; set; }
        public virtual DbSet<tblQuanTri> tblQuanTri { get; set; }
        public virtual DbSet<tblTinhThanh> tblTinhThanh { get; set; }
        public virtual DbSet<tblThongTinTiemChung> tblThongTinTiemChung { get; set; }

        public virtual DbSet<tblPhanAnh> tblPhanAnh { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblDKTiem>()
                .Property(e => e.SoCMND)
                .IsUnicode(false);

            modelBuilder.Entity<tblDKTiem>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<tblKhaiBaoYTe>()
                .Property(e => e.SoCMND)
                .IsUnicode(false);

            modelBuilder.Entity<tblKhaiBaoYTe>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<tblKhaiBaoYTe>()
                .Property(e => e.SoTheBHYT)
                .IsUnicode(false);

            modelBuilder.Entity<tblKhaiBaoYTe>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tblLoaiThe>()
                .Property(e => e.GhiChu)
                .IsUnicode(false);

            modelBuilder.Entity<tblNguoiDung>()
                .Property(e => e.NguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<tblNguoiDung>()
                .Property(e => e.NguoiCapNhat)
                .IsUnicode(false);

            modelBuilder.Entity<tblNguoiDung>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<tblNguoiDung>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<tblNguoiDung>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<tblNguoiDung>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tblNguoiDung>()
                .Property(e => e.SoCMND)
                .IsUnicode(false);

            modelBuilder.Entity<tblQRCode>()
                .Property(e => e.SoCMND)
                .IsUnicode(false);

            modelBuilder.Entity<tblQuanTri>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<tblQuanTri>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<tblQuanTri>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<tblQuanTri>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tblTinhThanh>()
                .Property(e => e.MaTT)
                .IsUnicode(false);

            modelBuilder.Entity<tblThongTinTiemChung>()
                .Property(e => e.NguoiCapNhat)
                .IsUnicode(false);

            modelBuilder.Entity<tblThongTinTiemChung>()
                .Property(e => e.GhiChu)
                .IsUnicode(false);



            modelBuilder.Entity<tblPhanAnh>()  // Hai mục chỗ này có thể sẽ bị lỗi
                .Property(e => e.SoCMND)
                .IsUnicode(false);


            modelBuilder.Entity<tblPhanAnh>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<tblPhanAnh>()
               .Property(e => e.Email)
               .IsUnicode(false);
        }
    }
}
