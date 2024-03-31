﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop_Api.AppDbContext;

#nullable disable

namespace Shop_Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240330180501_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Shop_Models.Entities.Anh", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChiTietSanPhamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("ChiTietSanPhamId");

                    b.ToTable("Anh");
                });

            modelBuilder.Entity("Shop_Models.Entities.ChatLieu", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaChatLieu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenChatLieu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Guid");

                    b.ToTable("ChatLieu");
                });

            modelBuilder.Entity("Shop_Models.Entities.ChiTietKhuyenMai", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChiTietSanPhamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KhuyenMaiId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mota")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChiTietSanPhamId");

                    b.HasIndex("KhuyenMaiId");

                    b.ToTable("ChiTietKhuyenMai");
                });

            modelBuilder.Entity("Shop_Models.Entities.ChiTietSanPham", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChatLieuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("GiaBan")
                        .HasColumnType("float");

                    b.Property<double?>("GiaNhap")
                        .HasColumnType("float");

                    b.Property<double?>("GiaThucTe")
                        .HasColumnType("float");

                    b.Property<Guid?>("LoaiId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaSanPham")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MauSacId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mota")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SanPhamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("SoLuongDaBan")
                        .HasColumnType("int");

                    b.Property<int?>("SoLuongTon")
                        .HasColumnType("int");

                    b.Property<Guid?>("ThuongHieuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.Property<int?>("TrangThaiKhuyenMai")
                        .HasColumnType("int");

                    b.Property<Guid?>("XuatXuId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChatLieuId");

                    b.HasIndex("LoaiId");

                    b.HasIndex("MauSacId");

                    b.HasIndex("SanPhamId");

                    b.HasIndex("ThuongHieuId");

                    b.HasIndex("XuatXuId");

                    b.ToTable("ChiTietSanPham");
                });

            modelBuilder.Entity("Shop_Models.Entities.ChucVu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaChucVu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("TenChucVu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Shop_Models.Entities.GioHang", b =>
                {
                    b.Property<Guid>("IdNguoiDung")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("NguoiDungId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IdNguoiDung");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("GioHang");
                });

            modelBuilder.Entity("Shop_Models.Entities.GioHangChiTiet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChiTietSanPhamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("GiaBan")
                        .HasColumnType("float");

                    b.Property<double>("GiaGoc")
                        .HasColumnType("float");

                    b.Property<Guid>("GioHangId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChiTietSanPhamId");

                    b.HasIndex("GioHangId");

                    b.ToTable("GioHangChiTiet");
                });

            modelBuilder.Entity("Shop_Models.Entities.HoaDon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LiDoHuy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaHoaDon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayGiaoDuKien")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayNhan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayShip")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayThanhToan")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("NguoiDungId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("TienGiam")
                        .HasColumnType("float");

                    b.Property<double>("TienShip")
                        .HasColumnType("float");

                    b.Property<double>("TongTien")
                        .HasColumnType("float");

                    b.Property<int>("TrangThaiGiaoHang")
                        .HasColumnType("int");

                    b.Property<int>("TrangThaiThanhToan")
                        .HasColumnType("int");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NguoiDungId");

                    b.HasIndex("VoucherId");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("Shop_Models.Entities.HoaDonChiTiet", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChiTietSanPhamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("GiaBan")
                        .HasColumnType("float");

                    b.Property<Guid>("HoaDonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("Guid");

                    b.HasIndex("ChiTietSanPhamId");

                    b.HasIndex("HoaDonId");

                    b.ToTable("HoaDonChiTiet");
                });

            modelBuilder.Entity("Shop_Models.Entities.Khuyenmai", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoaiHinhKhuyenMai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaKhuyenMai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("MucGiam")
                        .HasColumnType("float");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenKhuyenMai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Khuyenmai");
                });

            modelBuilder.Entity("Shop_Models.Entities.Loai", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaLoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenLoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Loai");
                });

            modelBuilder.Entity("Shop_Models.Entities.MauSac", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaMauSac")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenMauSac")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Guid");

                    b.ToTable("MauSac");
                });

            modelBuilder.Entity("Shop_Models.Entities.NguoiDung", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool?>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MaNguoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenNguoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VerificationCodeExpiry")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Shop_Models.Entities.PhuongThucThanhToan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaPTThanhToan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenMaPTThanhToan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PhuongThucThanhToan");
                });

            modelBuilder.Entity("Shop_Models.Entities.PhuongThucTTChiTiet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HoaDonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PTTToanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PhuongThucThanhToanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("SoTien")
                        .HasColumnType("float");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HoaDonId");

                    b.HasIndex("PhuongThucThanhToanId");

                    b.ToTable("PhuongThucTTChiTiet");
                });

            modelBuilder.Entity("Shop_Models.Entities.SanPham", b =>
                {
                    b.Property<Guid>("IdSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaSanPham")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenSanPham")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IdSanPham");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("Shop_Models.Entities.SanPhamYeuThich", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChiTietSanPhamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NguoiDungId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChiTietSanPhamId");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("SanPhamYeuThich");
                });

            modelBuilder.Entity("Shop_Models.Entities.ThongKe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChiTietSanPhamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HoaDonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Nam")
                        .HasColumnType("int");

                    b.Property<int>("Ngay")
                        .HasColumnType("int");

                    b.Property<Guid>("SanPhamChiTietId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Thang")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChiTietSanPhamId");

                    b.HasIndex("HoaDonId");

                    b.ToTable("ThongKe");
                });

            modelBuilder.Entity("Shop_Models.Entities.ThuongHieu", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaThuongHieu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenThuongHieu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Guid");

                    b.ToTable("ThuongHieu");
                });

            modelBuilder.Entity("Shop_Models.Entities.Voucher", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaVoucher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHetHan")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PhanTramGiam")
                        .HasColumnType("int");

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenVoucher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Guid");

                    b.ToTable("Voucher");
                });

            modelBuilder.Entity("Shop_Models.Entities.XuatXu", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaXuatXu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenXuatXu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Guid");

                    b.ToTable("XuatXu");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Shop_Models.Entities.ChucVu", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Shop_Models.Entities.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Shop_Models.Entities.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Shop_Models.Entities.ChucVu", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop_Models.Entities.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Shop_Models.Entities.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shop_Models.Entities.Anh", b =>
                {
                    b.HasOne("Shop_Models.Entities.ChiTietSanPham", "ChiTietSanPham")
                        .WithMany("Anhs")
                        .HasForeignKey("ChiTietSanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiTietSanPham");
                });

            modelBuilder.Entity("Shop_Models.Entities.ChiTietKhuyenMai", b =>
                {
                    b.HasOne("Shop_Models.Entities.ChiTietSanPham", "ChiTietSanPham")
                        .WithMany()
                        .HasForeignKey("ChiTietSanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop_Models.Entities.Khuyenmai", "Khuyenmai")
                        .WithMany("ChiTietKhuyenMais")
                        .HasForeignKey("KhuyenMaiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiTietSanPham");

                    b.Navigation("Khuyenmai");
                });

            modelBuilder.Entity("Shop_Models.Entities.ChiTietSanPham", b =>
                {
                    b.HasOne("Shop_Models.Entities.ChatLieu", "ChatLieu")
                        .WithMany("ChiTietSanPhams")
                        .HasForeignKey("ChatLieuId");

                    b.HasOne("Shop_Models.Entities.Loai", "Loai")
                        .WithMany("ChiTietSanPhams")
                        .HasForeignKey("LoaiId");

                    b.HasOne("Shop_Models.Entities.MauSac", "MauSac")
                        .WithMany("ChiTietSanPhams")
                        .HasForeignKey("MauSacId");

                    b.HasOne("Shop_Models.Entities.SanPham", "SanPham")
                        .WithMany("ChiTietSanPhams")
                        .HasForeignKey("SanPhamId");

                    b.HasOne("Shop_Models.Entities.ThuongHieu", "ThuongHieu")
                        .WithMany("ChiTietSanPhams")
                        .HasForeignKey("ThuongHieuId");

                    b.HasOne("Shop_Models.Entities.XuatXu", "XuatXu")
                        .WithMany("ChiTietSanPhams")
                        .HasForeignKey("XuatXuId");

                    b.Navigation("ChatLieu");

                    b.Navigation("Loai");

                    b.Navigation("MauSac");

                    b.Navigation("SanPham");

                    b.Navigation("ThuongHieu");

                    b.Navigation("XuatXu");
                });

            modelBuilder.Entity("Shop_Models.Entities.GioHang", b =>
                {
                    b.HasOne("Shop_Models.Entities.NguoiDung", "NguoiDung")
                        .WithMany()
                        .HasForeignKey("NguoiDungId");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("Shop_Models.Entities.GioHangChiTiet", b =>
                {
                    b.HasOne("Shop_Models.Entities.ChiTietSanPham", "ChiTietSanPham")
                        .WithMany("GioHangChiTiets")
                        .HasForeignKey("ChiTietSanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop_Models.Entities.GioHang", "GioHang")
                        .WithMany("GioHangChiTiets")
                        .HasForeignKey("GioHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiTietSanPham");

                    b.Navigation("GioHang");
                });

            modelBuilder.Entity("Shop_Models.Entities.HoaDon", b =>
                {
                    b.HasOne("Shop_Models.Entities.NguoiDung", "NguoiDung")
                        .WithMany()
                        .HasForeignKey("NguoiDungId");

                    b.HasOne("Shop_Models.Entities.Voucher", "Vouchers")
                        .WithMany("HoaDon")
                        .HasForeignKey("VoucherId");

                    b.Navigation("NguoiDung");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("Shop_Models.Entities.HoaDonChiTiet", b =>
                {
                    b.HasOne("Shop_Models.Entities.ChiTietSanPham", "ChiTietSanPham")
                        .WithMany("HoaDonChiTiets")
                        .HasForeignKey("ChiTietSanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop_Models.Entities.HoaDon", "HoaDon")
                        .WithMany()
                        .HasForeignKey("HoaDonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiTietSanPham");

                    b.Navigation("HoaDon");
                });

            modelBuilder.Entity("Shop_Models.Entities.PhuongThucTTChiTiet", b =>
                {
                    b.HasOne("Shop_Models.Entities.HoaDon", "HoaDon")
                        .WithMany("PhuongThucTTChiTiet")
                        .HasForeignKey("HoaDonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop_Models.Entities.PhuongThucThanhToan", "PhuongThucThanhToan")
                        .WithMany("PhuongThucTTChiTiets")
                        .HasForeignKey("PhuongThucThanhToanId");

                    b.Navigation("HoaDon");

                    b.Navigation("PhuongThucThanhToan");
                });

            modelBuilder.Entity("Shop_Models.Entities.SanPhamYeuThich", b =>
                {
                    b.HasOne("Shop_Models.Entities.ChiTietSanPham", "ChiTietSanPham")
                        .WithMany("SanPhamYeuThich")
                        .HasForeignKey("ChiTietSanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop_Models.Entities.NguoiDung", "NguoiDung")
                        .WithMany()
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiTietSanPham");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("Shop_Models.Entities.ThongKe", b =>
                {
                    b.HasOne("Shop_Models.Entities.ChiTietSanPham", "ChiTietSanPham")
                        .WithMany("ThongKes")
                        .HasForeignKey("ChiTietSanPhamId");

                    b.HasOne("Shop_Models.Entities.HoaDon", "HoaDon")
                        .WithMany("ThongKes")
                        .HasForeignKey("HoaDonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiTietSanPham");

                    b.Navigation("HoaDon");
                });

            modelBuilder.Entity("Shop_Models.Entities.ChatLieu", b =>
                {
                    b.Navigation("ChiTietSanPhams");
                });

            modelBuilder.Entity("Shop_Models.Entities.ChiTietSanPham", b =>
                {
                    b.Navigation("Anhs");

                    b.Navigation("GioHangChiTiets");

                    b.Navigation("HoaDonChiTiets");

                    b.Navigation("SanPhamYeuThich");

                    b.Navigation("ThongKes");
                });

            modelBuilder.Entity("Shop_Models.Entities.GioHang", b =>
                {
                    b.Navigation("GioHangChiTiets");
                });

            modelBuilder.Entity("Shop_Models.Entities.HoaDon", b =>
                {
                    b.Navigation("PhuongThucTTChiTiet");

                    b.Navigation("ThongKes");
                });

            modelBuilder.Entity("Shop_Models.Entities.Khuyenmai", b =>
                {
                    b.Navigation("ChiTietKhuyenMais");
                });

            modelBuilder.Entity("Shop_Models.Entities.Loai", b =>
                {
                    b.Navigation("ChiTietSanPhams");
                });

            modelBuilder.Entity("Shop_Models.Entities.MauSac", b =>
                {
                    b.Navigation("ChiTietSanPhams");
                });

            modelBuilder.Entity("Shop_Models.Entities.PhuongThucThanhToan", b =>
                {
                    b.Navigation("PhuongThucTTChiTiets");
                });

            modelBuilder.Entity("Shop_Models.Entities.SanPham", b =>
                {
                    b.Navigation("ChiTietSanPhams");
                });

            modelBuilder.Entity("Shop_Models.Entities.ThuongHieu", b =>
                {
                    b.Navigation("ChiTietSanPhams");
                });

            modelBuilder.Entity("Shop_Models.Entities.Voucher", b =>
                {
                    b.Navigation("HoaDon");
                });

            modelBuilder.Entity("Shop_Models.Entities.XuatXu", b =>
                {
                    b.Navigation("ChiTietSanPhams");
                });
#pragma warning restore 612, 618
        }
    }
}
