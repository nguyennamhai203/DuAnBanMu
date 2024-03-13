using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    TenNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationCodeExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatLieu",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaChatLieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenChatLieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLieu", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Khuyenmai",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaKhuyenMai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKhuyenMai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoaiHinhKhuyenMai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucGiam = table.Column<double>(type: "float", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khuyenmai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaLoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MauSac",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaMauSac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenMauSac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSac", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucThanhToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaPTThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenMaPTThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongThucThanhToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.IdSanPham);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieu",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaThuongHieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenThuongHieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieu", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaVoucher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenVoucher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "XuatXu",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaXuatXu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenXuatXu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatXu", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    IdNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    NguoiDungId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.IdNguoiDung);
                    table.ForeignKey(
                        name: "FK_GioHang_AspNetUsers_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayShip = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TienGiam = table.Column<double>(type: "float", nullable: false),
                    TienShip = table.Column<double>(type: "float", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false),
                    TrangThaiThanhToan = table.Column<int>(type: "int", nullable: false),
                    TrangThaiGiaoHang = table.Column<int>(type: "int", nullable: false),
                    NgayGiaoDuKien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LiDoHuy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VouchersGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NguoiDungId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDon_AspNetUsers_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HoaDon_Voucher_VouchersGuid",
                        column: x => x.VouchersGuid,
                        principalTable: "Voucher",
                        principalColumn: "Guid");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietSanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaNhap = table.Column<double>(type: "float", nullable: true),
                    GiaBan = table.Column<double>(type: "float", nullable: true),
                    GiaThucTe = table.Column<double>(type: "float", nullable: true),
                    SoLuongTon = table.Column<int>(type: "int", nullable: true),
                    SoLuongDaBan = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    SanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LoaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThuongHieuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    XuatXuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MauSacId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChatLieuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietSanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_ChatLieu_ChatLieuId",
                        column: x => x.ChatLieuId,
                        principalTable: "ChatLieu",
                        principalColumn: "Guid");
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_Loai_LoaiId",
                        column: x => x.LoaiId,
                        principalTable: "Loai",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_MauSac_MauSacId",
                        column: x => x.MauSacId,
                        principalTable: "MauSac",
                        principalColumn: "Guid");
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_SanPham_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPham",
                        principalColumn: "IdSanPham");
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_ThuongHieu_ThuongHieuId",
                        column: x => x.ThuongHieuId,
                        principalTable: "ThuongHieu",
                        principalColumn: "Guid");
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_XuatXu_XuatXuId",
                        column: x => x.XuatXuId,
                        principalTable: "XuatXu",
                        principalColumn: "Guid");
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucTTChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoaDonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PTTToanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoTien = table.Column<double>(type: "float", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    PhuongThucThanhToanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongThucTTChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhuongThucTTChiTiet_HoaDon_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhuongThucTTChiTiet_PhuongThucThanhToan_PhuongThucThanhToanId",
                        column: x => x.PhuongThucThanhToanId,
                        principalTable: "PhuongThucThanhToan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Anh",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChiTietSanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anh", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Anh_ChiTietSanPham_ChiTietSanPhamId",
                        column: x => x.ChiTietSanPhamId,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietKhuyenMai",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhuyenMaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChiTietSanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietKhuyenMai", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietKhuyenMai_ChiTietSanPham_ChiTietSanPhamId",
                        column: x => x.ChiTietSanPhamId,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietKhuyenMai_Khuyenmai_KhuyenMaiId",
                        column: x => x.KhuyenMaiId,
                        principalTable: "Khuyenmai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GioHangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChiTietSanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<double>(type: "float", nullable: false),
                    GiaGoc = table.Column<double>(type: "float", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_ChiTietSanPham_ChiTietSanPhamId",
                        column: x => x.ChiTietSanPhamId,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_GioHang_GioHangId",
                        column: x => x.GioHangId,
                        principalTable: "GioHang",
                        principalColumn: "IdNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonChiTiet",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChiTietSanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoaDonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonChiTiet", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_ChiTietSanPham_ChiTietSanPhamId",
                        column: x => x.ChiTietSanPhamId,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_HoaDon_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamYeuThich",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NguoiDungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChiTietSanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamYeuThich", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPhamYeuThich_AspNetUsers_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamYeuThich_ChiTietSanPham_ChiTietSanPhamId",
                        column: x => x.ChiTietSanPhamId,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongKe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoaDonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SanPhamChiTietId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ngay = table.Column<int>(type: "int", nullable: false),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    ChiTietSanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThongKe_ChiTietSanPham_ChiTietSanPhamId",
                        column: x => x.ChiTietSanPhamId,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ThongKe_HoaDon_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anh_ChiTietSanPhamId",
                table: "Anh",
                column: "ChiTietSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKhuyenMai_ChiTietSanPhamId",
                table: "ChiTietKhuyenMai",
                column: "ChiTietSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKhuyenMai_KhuyenMaiId",
                table: "ChiTietKhuyenMai",
                column: "KhuyenMaiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_ChatLieuId",
                table: "ChiTietSanPham",
                column: "ChatLieuId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_LoaiId",
                table: "ChiTietSanPham",
                column: "LoaiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MauSacId",
                table: "ChiTietSanPham",
                column: "MauSacId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_SanPhamId",
                table: "ChiTietSanPham",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_ThuongHieuId",
                table: "ChiTietSanPham",
                column: "ThuongHieuId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_XuatXuId",
                table: "ChiTietSanPham",
                column: "XuatXuId");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_NguoiDungId",
                table: "GioHang",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_ChiTietSanPhamId",
                table: "GioHangChiTiet",
                column: "ChiTietSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_GioHangId",
                table: "GioHangChiTiet",
                column: "GioHangId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_NguoiDungId",
                table: "HoaDon",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_VouchersGuid",
                table: "HoaDon",
                column: "VouchersGuid");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_ChiTietSanPhamId",
                table: "HoaDonChiTiet",
                column: "ChiTietSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_HoaDonId",
                table: "HoaDonChiTiet",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhuongThucTTChiTiet_HoaDonId",
                table: "PhuongThucTTChiTiet",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhuongThucTTChiTiet_PhuongThucThanhToanId",
                table: "PhuongThucTTChiTiet",
                column: "PhuongThucThanhToanId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamYeuThich_ChiTietSanPhamId",
                table: "SanPhamYeuThich",
                column: "ChiTietSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamYeuThich_NguoiDungId",
                table: "SanPhamYeuThich",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongKe_ChiTietSanPhamId",
                table: "ThongKe",
                column: "ChiTietSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongKe_HoaDonId",
                table: "ThongKe",
                column: "HoaDonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anh");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ChiTietKhuyenMai");

            migrationBuilder.DropTable(
                name: "GioHangChiTiet");

            migrationBuilder.DropTable(
                name: "HoaDonChiTiet");

            migrationBuilder.DropTable(
                name: "PhuongThucTTChiTiet");

            migrationBuilder.DropTable(
                name: "SanPhamYeuThich");

            migrationBuilder.DropTable(
                name: "ThongKe");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Khuyenmai");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "PhuongThucThanhToan");

            migrationBuilder.DropTable(
                name: "ChiTietSanPham");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "ChatLieu");

            migrationBuilder.DropTable(
                name: "Loai");

            migrationBuilder.DropTable(
                name: "MauSac");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "ThuongHieu");

            migrationBuilder.DropTable(
                name: "XuatXu");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Voucher");
        }
    }
}
