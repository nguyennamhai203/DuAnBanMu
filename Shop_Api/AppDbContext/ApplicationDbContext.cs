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
        public virtual DbSet<SanPhamYeuThichViewModel> SanPhamYeuThichViewModels { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer("Server =.\\SQLEXPRESS; Database =DATN_Website_SellLHat3; Trusted_Connection = True;TrustServerCertificate=True"));
            optionsBuilder.EnableSensitiveDataLogging();
        }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {

                var spid = Guid.NewGuid();
                builder.Entity<SanPham>().HasData(
                    new SanPham
                    {
                        IdSanPham = spid,
                        MaSanPham = "SP" + random.Next(i, i + 1),
                        TenSanPham = "TSP" + random.Next(i, i + 1),
                        TrangThai = random.Next(0, 2)
                    });

                var lid = Guid.NewGuid();
                builder.Entity<Loai>().HasData(
                    new Loai
                    {
                        Id = lid,
                        MaLoai = "ML" + random.Next(i, i + 1),
                        TenLoai = "TL" + random.Next(i, i + 1),
                        TrangThai = random.Next(0, 2)
                    });

                var thid = Guid.NewGuid();
                builder.Entity<ThuongHieu>().HasData(
                    new ThuongHieu
                    {
                        Guid = thid,
                        MaThuongHieu = "MTH" + random.Next(i, i + 1),
                        TenThuongHieu = "TTH" + random.Next(i, i + 1),
                        TrangThai = random.Next(0, 2)
                    });

                var xxid = Guid.NewGuid();
                builder.Entity<XuatXu>().HasData(
                    new XuatXu
                    {
                        Guid = xxid,
                        MaXuatXu = "MXX" + random.Next(i, i + 1),
                        TenXuatXu = "TXX" + random.Next(i, i + 1),
                        TrangThai = random.Next(0, 2)
                    });

                var msid = Guid.NewGuid();
                builder.Entity<MauSac>().HasData(
                    new MauSac
                    {
                        Guid = msid,
                        MaMauSac = "MMS" + random.Next(i, i + 1),
                        TenMauSac = "TMS" + random.Next(i, i + 1),
                        TrangThai = random.Next(0, 2)
                    });

                var clid = Guid.NewGuid();
                builder.Entity<ChatLieu>().HasData(
                    new ChatLieu
                    {
                        Guid = clid,
                        MaChatLieu = "MCL" + random.Next(i, i + 1),
                        TenChatLieu = "TCL" + random.Next(i, i + 1),
                        TrangThai = random.Next(0, 2)
                    });

                var ctspid = Guid.NewGuid();
                builder.Entity<ChiTietSanPham>().HasData(
                    new ChiTietSanPham
                    {
                        Id = ctspid,
                        MaSanPham = "SP" + random.Next(i, i + 1),
                        GiaNhap = random.Next(100000, 1000000),
                        GiaBan = random.Next(100000, 1000000),
                        GiaThucTe = random.Next(100000, 1000000),
                        SoLuongTon = random.Next(i, i + 1),
                        SoLuongDaBan = random.Next(i, i + 1),
                        TrangThai = random.Next(0, 2),
                        Mota = "mo ta" + random.Next(i, i + 1),
                        TrangThaiKhuyenMai = random.Next(0, 2),
                        SanPhamId = spid,
                        LoaiId = lid,
                        ThuongHieuId = thid,
                        XuatXuId = xxid,
                        MauSacId = msid,
                        ChatLieuId = clid
                    });

                var aid = Guid.NewGuid();
                var imageUrl = "URL" + random.Next(i, i + 1);
                builder.Entity<Anh>().HasData(
                    new Anh
                    {
                        Guid = aid,
                        MaAnh = "MA" + random.Next(i, i + 1),
                        URL = imageUrl,
                        ChiTietSanPhamId = ctspid
                    });

                var kmid = Guid.NewGuid();
                builder.Entity<Khuyenmai>().HasData(
                    new Khuyenmai
                    {
                        Id = kmid,
                        MaKhuyenMai = "MKM" + random.Next(i, i + 1),
                        TenKhuyenMai = "TKM" + random.Next(i, i + 1),
                        NgayBatDau = DateTime.UtcNow,
                        NgayKetThuc = DateTime.UtcNow.AddDays(random.Next(1, 30)),
                        LoaiHinhKhuyenMai = "LHKM" + random.Next(i, i + 1),
                        MucGiam = random.Next(10, 100),
                        TrangThai = random.Next(0, 2)
                    });

                var ctkmid = Guid.NewGuid();
                builder.Entity<ChiTietKhuyenMai>().HasData(
                    new ChiTietKhuyenMai
                    {
                        Id = ctkmid,
                        Mota = "MTCTKM" + random.Next(i, i + 1),
                        TrangThai = random.Next(0, 2),
                        KhuyenMaiId = kmid,
                        ChiTietSanPhamId = ctspid
                    });

                var ndid = Guid.NewGuid();
                builder.Entity<NguoiDung>().HasData(
                    new NguoiDung
                    {
                        Id = ndid,
                        MaNguoiDung = "ND" + random.Next(i, i + 1),
                        TenNguoiDung = "TND" + random.Next(i, i + 1),
                        SoDienThoai = "Null" + random.Next(i, i + 1),
                        DiaChi = "DC" + random.Next(i, i + 1),
                        GioiTinh = random.Next(0, 2) == 0,
                        TrangThai = random.Next(0, 2),
                        VerificationCode = "VC" + random.Next(i, i + 1),
                        VerificationCodeExpiry = DateTime.UtcNow.AddHours(random.Next(1, 48))
                    });

                var ghid = Guid.NewGuid();
                builder.Entity<GioHang>().HasData(
                    new GioHang
                    {
                        IdGh = ghid,
                        TrangThai = random.Next(0, 2),
                        IdNguoiDung = ndid
                    });

                var hdid = Guid.NewGuid();
                builder.Entity<HoaDon>().HasData(
                    new HoaDon
                    {
                        Id = hdid,
                        MaHoaDon = "HD" + random.Next(i, i + 1),
                        NgayTao = DateTime.UtcNow,
                        NgayThanhToan = DateTime.UtcNow.AddDays(random.Next(1, 7)),
                        NgayShip = DateTime.UtcNow.AddDays(random.Next(1, 7)),
                        NgayNhan = DateTime.UtcNow.AddDays(random.Next(7, 14)),
                        MoTa = "mo ta" + random.Next(i, i + 1),
                        TienGiam = random.Next(10000, 100000),
                        TienShip = random.Next(10000, 50000),
                        TongTien = random.Next(100000, 1000000),
                        TrangThaiThanhToan = random.Next(0, 2),
                        TrangThaiGiaoHang = random.Next(0, 2),
                        NgayGiaoDuKien = DateTime.UtcNow.AddDays(random.Next(7, 14)),
                        LiDoHuy = "ly do huy" + random.Next(i, i + 1)
                    });

                var vcid = Guid.NewGuid();
                builder.Entity<Voucher>().HasData(
                    new Voucher
                    {
                        Guid = vcid,
                        MaVoucher = "MVC" + random.Next(i, i + 1),
                        TenVoucher = "TVC" + random.Next(i, i + 1),
                        PhanTramGiam = random.Next(10, 100),
                        SoLuong = random.Next(0, 10),
                        NgayBatDau = DateTime.UtcNow,
                        NgayHetHan = DateTime.UtcNow.AddDays(random.Next(1, 30)),
                        TrangThai = random.Next(0, 2)
                    });

                var ptttid = Guid.NewGuid();
                builder.Entity<PhuongThucThanhToan>().HasData(
                    new PhuongThucThanhToan
                    {
                        Id = ptttid,
                        MaPTThanhToan = "MPTTT" + random.Next(i, i + 1),
                        TenMaPTThanhToan = "TPTTT" + random.Next(i, i + 1),
                        MoTa = "MTPTTT" + random.Next(i, i + 1),
                        TrangThai = random.Next(0, 2)
                    });

                var ptttctid = Guid.NewGuid();
                builder.Entity<PhuongThucTTChiTiet>().HasData(
                    new PhuongThucTTChiTiet
                    {
                        Id = ptttctid,
                        SoTien = random.Next(100000, 1000000),
                        TrangThai = random.Next(0, 2),
                        HoaDonId = hdid,
                        PTTToanId = ptttid
                    });

                builder.Entity<HoaDonChiTiet>().HasData(
                    new HoaDonChiTiet
                    {
                        Guid = Guid.NewGuid(),
                        SoLuong = random.Next(1, 100),
                        GiaBan = random.Next(100000, 1000000),
                        HoaDonId = hdid,
                        ChiTietSanPhamId = ctspid
                    });

                builder.Entity<SanPhamYeuThich>().HasData(
                    new SanPhamYeuThich
                    {
                        Id = Guid.NewGuid(),
                        TrangThai = random.Next(0, 2),
                        NguoiDungId = ndid,
                        ChiTietSanPhamId = ctspid
                    });

                var spytvmid = Guid.NewGuid();
                var randomImageUrl = "URL" + random.Next(i, i + 1);
                builder.Entity<SanPhamYeuThichViewModel>().HasData(
                    new SanPhamYeuThichViewModel
                    {
                        Id = spytvmid,
                        MaNguoiDung = "ND" + random.Next(i, i + 1),
                        TrangThaiND = random.Next(0, 1),
                        TenNguoiDung = "TND" + random.Next(i, i + 1),
                        SoDienThoai = "Null" + random.Next(i, i + 1),
                        DiaChi = "DC" + random.Next(i, i + 1),
                        GioiTinh = random.Next(0, 1) == 0,
                        VerificationCode = "VC" + random.Next(i, i + 1),
                        ChiTietSanPhamId = ctspid,
                        MaAnh = "MA" + random.Next(i, i + 1),
                        URL = randomImageUrl,
                        MaSanPhamCT = "MSPCT" + random.Next(i, i + 1),
                        GiaNhap = random.Next(100000, 1000000),
                        GiaBan = random.Next(100000, 1000000),
                        GiaThucTe = random.Next(100000, 1000000),
                        AnhSanPham = randomImageUrl,
                        SoLuongTon = random.Next(i, i + 1),
                        SoLuongDaBan = random.Next(i, i + 1),
                        Mota = "mo ta" + random.Next(i, i + 1),
                        TrangThaiKhuyenMai = random.Next(0, 2),
                        MaSanPham = "MSP" + random.Next(i, i + 1),
                        TenSanPham = "TSP" + random.Next(i, i + 1),
                        TrangThai = random.Next(0, 2),
                    });
            }
        }*/
    }
}