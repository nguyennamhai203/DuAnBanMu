using Shop_Models.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Shop_Models.Dto;
using System;

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
        public virtual DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }
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
        public virtual DbSet<ThongKeViewModel> ThongKeViewModels { get; set;}

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer("Server =.\\SQLEXPRESS; Database =DATN_Website_SellLHat3; Trusted_Connection = True;TrustServerCertificate=True"));
        }

        private string GenerateRandomPhoneNumber(Random random)
        {
            string[] prefixes = { "03", "05", "07", "08", "09" };
            string prefix = prefixes[random.Next(0, prefixes.Length)];
            string number = prefix;
            for (int i = 0; i < 7; i++)
            {
                number += random.Next(0, 10);
            }
            return number;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                builder.Entity<NguoiDung>().HasData(
                    new NguoiDung
                    {
                        Id = Guid.NewGuid(),
                        MaNguoiDung = "ND" + random.Next(1, 6),
                        TenNguoiDung = "TND" + random.Next(1, 6),
                        SoDienThoai = GenerateRandomPhoneNumber(random),
                        DiaChi = "DC" + random.Next(1, 6),
                        GioiTinh = random.Next(0, 2) == 0 ? true : false,
                        TrangThai = random.Next(0, 2),
                        VerificationCode = "VC" + random.Next(1, 6),
                        VerificationCodeExpiry = DateTime.UtcNow.AddHours(random.Next(1, 48))
                    }
                );
            }
            for (int i = 0; i < 5; i++)
            {
                builder.Entity<SanPham>().HasData(
                    new SanPham
                    {
                        IdSanPham = Guid.NewGuid(),
                        MaSanPham = "SP" + random.Next(1, 6),
                        TenSanPham = "TSP" + random.Next(1, 6),
                        TrangThai = random.Next(0, 2)
                    }
                );
            }
            for (int i = 0; i < 5; i++)
            {
                var hdid = Guid.NewGuid();
                builder.Entity<HoaDon>().HasData(new HoaDon
                {
                    Id = hdid,
                    MaHoaDon = "HD" + random.Next(1, 6),
                    NgayTao = DateTime.UtcNow,
                    NgayThanhToan = DateTime.UtcNow,
                    NgayShip = DateTime.UtcNow,
                    NgayNhan = DateTime.UtcNow,
                    MoTa = "mo ta" + random.Next(1, 6),
                    TienGiam = random.Next(10000, 100000),
                    TienShip = random.Next(10000, 50000),
                    TongTien = random.Next(10000, 100000) + random.Next(10000, 50000),
                    TrangThaiThanhToan = random.Next(0, 2),
                    TrangThaiGiaoHang = random.Next(0, 2),
                    NgayGiaoDuKien = DateTime.UtcNow,
                    LiDoHuy = "ly do huy" + random.Next(1, 6)
                });
                var ctspid = Guid.NewGuid();
                builder.Entity<ChiTietSanPham>().HasData(new ChiTietSanPham
                {
                    Id = ctspid,
                    MaSanPham = "SP" + random.Next(1, 6),
                    GiaNhap = random.Next(100000, 1000000),
                    GiaBan = random.Next(100000, 1000000),
                    GiaThucTe = random.Next(100000, 1000000),
                    SoLuongTon = random.Next(1, 10),
                    SoLuongDaBan = random.Next(1, 10),
                    TrangThai = random.Next(0, 2),
                    Mota = "mo ta" + random.Next(1, 6),
                    TrangThaiKhuyenMai = random.Next(0, 2),
                    SanPhamId = null, // Replace null with valid foreign key
                    LoaiId = null, // Replace null with valid foreign key
                    ThuongHieuId = null, // Replace null with valid foreign key
                    XuatXuId = null, // Replace null with valid foreign key
                    MauSacId = null, // Replace null with valid foreign key
                    ChatLieuId = null // Replace null with valid foreign key
                });

                builder.Entity<ThongKe>().HasData(new ThongKe
                {
                    Id = Guid.NewGuid(),
                    Ngay = random.Next(1, 32),
                    Thang = random.Next(1, 13),
                    Nam = random.Next(2000, 2024),
                    TrangThai = random.Next(0, 2),
                    HoaDonId = hdid,
                    SanPhamChiTietId = ctspid
                });
            }
        }*/
    }
}