using Shop_Models.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Shop_Api.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext<NguoiDung,ChucVu,Guid>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Anh> Anhs { get; set; }
        public virtual DbSet<ChatLieu> ChatLieus { get; set; }
        public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
        public virtual DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<GioHangChiTiet> GioHangChiTiets { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<Khuyenmai> KhuyenMais { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<MauSac> MauSacs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }
        public virtual DbSet<PhuongThucTTChiTiet> PhuongThucTTChiTiets { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<SanPhamYeuThich> SanPhamYeuThichs { get; set; }
        public virtual DbSet<ThongKe> ThongKes { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieus { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<XuatXu> XuatXus { get; set; }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Voucher>().HasData(
                new Voucher
                {
                    Guid = Guid.NewGuid(),
                    MaVoucher = "test",
                    TenVoucher = "test",
                    PhanTramGiam = 10,
                    SoLuong = 1,
                    NgayBatDau = new DateTime(2000, 1, 23),
                    NgayHetHan = new DateTime(2024, 2, 25),
                    TrangThai = 1
                }
            );
        }*/
    }
}