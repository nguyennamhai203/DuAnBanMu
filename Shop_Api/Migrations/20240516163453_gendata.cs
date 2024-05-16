using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class gendata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHangChiTiet_GioHang_GioHangId",
                table: "GioHangChiTiet");

            migrationBuilder.DropTable(
                name: "ThongKeViewModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang");

            migrationBuilder.AddColumn<Guid>(
                name: "IdGh",
                table: "GioHang",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang",
                column: "IdGh");

            migrationBuilder.CreateTable(
                name: "SanPhamYeuThichViewModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThaiND = table.Column<int>(type: "int", nullable: false),
                    TenNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationCodeExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChiTietSanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSanPhamCT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaNhap = table.Column<double>(type: "float", nullable: true),
                    GiaBan = table.Column<double>(type: "float", nullable: true),
                    GiaThucTe = table.Column<double>(type: "float", nullable: true),
                    SoLuongTon = table.Column<int>(type: "int", nullable: true),
                    SoLuongDaBan = table.Column<int>(type: "int", nullable: true),
                    TrangThaiCTSP = table.Column<int>(type: "int", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThaiKhuyenMai = table.Column<int>(type: "int", nullable: true),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamYeuThichViewModels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DiaChi", "Email", "EmailConfirmed", "GioiTinh", "LockoutEnabled", "LockoutEnd", "MaNguoiDung", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SoDienThoai", "TenNguoiDung", "TrangThai", "TwoFactorEnabled", "UserName", "VerificationCode", "VerificationCodeExpiry" },
                values: new object[,]
                {
                    { new Guid("35172428-03d8-489d-ba34-a35534dcd22b"), 0, "ed0b1dfe-e6a4-4ad1-987b-028de7b53e4c", "DC3", null, false, true, false, null, "ND3", null, null, null, null, false, null, "Null3", "TND3", 1, false, null, "VC3", new DateTime(2024, 5, 18, 4, 34, 52, 687, DateTimeKind.Utc).AddTicks(9096) },
                    { new Guid("55d1353d-42be-4acb-a345-b920295d24dc"), 0, "6b58937d-3cc2-4549-a27a-e11b6c26e6ae", "DC0", null, false, false, false, null, "ND0", null, null, null, null, false, null, "Null0", "TND0", 1, false, null, "VC0", new DateTime(2024, 5, 18, 9, 34, 52, 687, DateTimeKind.Utc).AddTicks(8374) },
                    { new Guid("a3383fbf-0883-4950-bb9f-87cb31040d44"), 0, "09b18bf1-07a0-41cd-af9b-74f9ddd60a8c", "DC2", null, false, false, false, null, "ND2", null, null, null, null, false, null, "Null2", "TND2", 1, false, null, "VC2", new DateTime(2024, 5, 16, 21, 34, 52, 687, DateTimeKind.Utc).AddTicks(8917) },
                    { new Guid("a5a5b1fb-5ab6-49de-b376-9d6cfddc502f"), 0, "7f00b18c-7780-4e23-b1cb-7d7ad6f896b0", "DC1", null, false, false, false, null, "ND1", null, null, null, null, false, null, "Null1", "TND1", 1, false, null, "VC1", new DateTime(2024, 5, 18, 10, 34, 52, 687, DateTimeKind.Utc).AddTicks(8729) },
                    { new Guid("abf2e001-7e62-42f8-b531-6c33b2d38a98"), 0, "9aa0ae64-d8f2-4926-b68a-440986844291", "DC4", null, false, false, false, null, "ND4", null, null, null, null, false, null, "Null4", "TND4", 0, false, null, "VC4", new DateTime(2024, 5, 18, 1, 34, 52, 687, DateTimeKind.Utc).AddTicks(9332) }
                });

            migrationBuilder.InsertData(
                table: "ChatLieu",
                columns: new[] { "Guid", "MaChatLieu", "TenChatLieu", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("584f9f24-001e-4aad-826f-b5f4b710945f"), "MCL1", "TCL1", 1 },
                    { new Guid("a781c9ce-0609-4cf5-9016-57bd3230fb0d"), "MCL2", "TCL2", 1 },
                    { new Guid("b29383d8-b17a-4572-8166-c94fe0382548"), "MCL4", "TCL4", 0 },
                    { new Guid("b84d31ac-9d63-481b-8027-1a074698bb84"), "MCL3", "TCL3", 0 },
                    { new Guid("cb1c36d6-ae8e-4212-ae3c-7b44b6aab686"), "MCL0", "TCL0", 1 }
                });

            migrationBuilder.InsertData(
                table: "GioHang",
                columns: new[] { "IdGh", "IdNguoiDung", "NguoiDungId", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("1c6950d0-f548-404c-84fb-321c059f9629"), new Guid("35172428-03d8-489d-ba34-a35534dcd22b"), null, 1 },
                    { new Guid("7003b01f-0bb5-4100-91f7-68131a86712d"), new Guid("a5a5b1fb-5ab6-49de-b376-9d6cfddc502f"), null, 1 },
                    { new Guid("8cc9ae39-9257-40f8-82c5-145f49b80ba0"), new Guid("55d1353d-42be-4acb-a345-b920295d24dc"), null, 1 },
                    { new Guid("8f849e54-340d-4e0b-b84e-f144747d5313"), new Guid("a3383fbf-0883-4950-bb9f-87cb31040d44"), null, 0 },
                    { new Guid("b0ddbe55-9753-4ba7-8345-e0c7afb8c2d6"), new Guid("abf2e001-7e62-42f8-b531-6c33b2d38a98"), null, 1 }
                });

            migrationBuilder.InsertData(
                table: "HoaDon",
                columns: new[] { "Id", "DiaChiGiaoHang", "LiDoHuy", "MaHoaDon", "MoTa", "NgayGiaoDuKien", "NgayNhan", "NgayShip", "NgayTao", "NgayThanhToan", "NguoiDungId", "SoDienThoai", "TenKhachHang", "TienGiam", "TienShip", "TongTien", "TrangThaiGiaoHang", "TrangThaiThanhToan", "VoucherId" },
                values: new object[,]
                {
                    { new Guid("077d1f6c-349c-4625-9cea-829cf965252b"), null, "ly do huy2", "HD2", "mo ta2", new DateTime(2024, 5, 23, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8933), new DateTime(2024, 5, 25, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8931), new DateTime(2024, 5, 19, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8931), new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8930), new DateTime(2024, 5, 22, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8930), null, null, null, 40611.0, 34904.0, 944161.0, 0, 1, null },
                    { new Guid("14101749-75f7-47f9-99d4-b3f597af039c"), null, "ly do huy0", "HD0", "mo ta0", new DateTime(2024, 5, 27, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8407), new DateTime(2024, 5, 28, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8404), new DateTime(2024, 5, 17, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8403), new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8402), new DateTime(2024, 5, 21, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8402), null, null, null, 85510.0, 48285.0, 236173.0, 1, 1, null },
                    { new Guid("3173edf6-1311-45f1-914b-e9898d9261ed"), null, "ly do huy4", "HD4", "mo ta4", new DateTime(2024, 5, 23, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9353), new DateTime(2024, 5, 28, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9351), new DateTime(2024, 5, 19, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9351), new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9350), new DateTime(2024, 5, 20, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9350), null, null, null, 65141.0, 49737.0, 346804.0, 0, 1, null },
                    { new Guid("470dd866-f29e-4bcd-80b4-29f937cdf060"), null, "ly do huy1", "HD1", "mo ta1", new DateTime(2024, 5, 28, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8744), new DateTime(2024, 5, 27, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8742), new DateTime(2024, 5, 22, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8742), new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8741), new DateTime(2024, 5, 21, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8741), null, null, null, 79327.0, 11909.0, 831937.0, 0, 1, null },
                    { new Guid("749ebe10-465a-4267-899b-dcfd38e49605"), null, "ly do huy3", "HD3", "mo ta3", new DateTime(2024, 5, 25, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9111), new DateTime(2024, 5, 24, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9109), new DateTime(2024, 5, 22, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9109), new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9108), new DateTime(2024, 5, 21, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9108), null, null, null, 62575.0, 28000.0, 476297.0, 0, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Khuyenmai",
                columns: new[] { "Id", "LoaiHinhKhuyenMai", "MaKhuyenMai", "MucGiam", "NgayBatDau", "NgayKetThuc", "TenKhuyenMai", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("07e54863-9fd0-4b0c-987c-6ba69fecf361"), "LHKM4", "MKM4", 44.0, new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9290), new DateTime(2024, 5, 24, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9291), "TKM4", 0 },
                    { new Guid("23830357-bf3e-4ceb-8fb3-551da0f321ab"), "LHKM2", "MKM2", 17.0, new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8897), new DateTime(2024, 6, 9, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8898), "TKM2", 0 },
                    { new Guid("65e73287-17ea-444b-aa88-c5b37c27d2e7"), "LHKM3", "MKM3", 91.0, new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9076), new DateTime(2024, 5, 26, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9077), "TKM3", 1 },
                    { new Guid("6e4c0a24-0a37-4ccf-b913-57f14702a4d2"), "LHKM1", "MKM1", 25.0, new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8709), new DateTime(2024, 6, 10, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8709), "TKM1", 1 },
                    { new Guid("a2dda9d4-ad65-4690-86d4-e001b699ba5f"), "LHKM0", "MKM0", 83.0, new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8322), new DateTime(2024, 6, 2, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8324), "TKM0", 1 }
                });

            migrationBuilder.InsertData(
                table: "Loai",
                columns: new[] { "Id", "MaLoai", "TenLoai", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("1df6af10-f977-4db0-9f50-a135ddcdfd83"), "ML3", "TL3", 0 },
                    { new Guid("1ef043d1-37b2-4fb4-aad9-ea5d00939bc6"), "ML2", "TL2", 1 },
                    { new Guid("5b70e17d-1426-4b52-9de7-8a68b4717bb7"), "ML4", "TL4", 0 },
                    { new Guid("a0b91fbf-bd40-454f-a94e-7c754e818cab"), "ML1", "TL1", 1 },
                    { new Guid("df1d404e-a91c-4f04-b122-8264a508b8e3"), "ML0", "TL0", 0 }
                });

            migrationBuilder.InsertData(
                table: "MauSac",
                columns: new[] { "Guid", "MaMauSac", "TenMauSac", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("15e45203-d243-4659-951c-22b8f1a4dc7d"), "MMS3", "TMS3", 1 },
                    { new Guid("74e0b1cb-a556-4bb7-8f20-eb6608d5f6d3"), "MMS4", "TMS4", 0 },
                    { new Guid("c7d3f38b-5968-4c0b-8790-68108b1b8621"), "MMS2", "TMS2", 1 },
                    { new Guid("f440a547-e36e-4276-8535-31b52174cbce"), "MMS1", "TMS1", 1 },
                    { new Guid("fdbbaab8-34d6-4640-8f10-c2046829451a"), "MMS0", "TMS0", 1 }
                });

            migrationBuilder.InsertData(
                table: "PhuongThucThanhToan",
                columns: new[] { "Id", "MaPTThanhToan", "MoTa", "TenMaPTThanhToan", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("4a6e5fbe-b6ee-4a96-91af-9c9a7d7732c7"), "MPTTT0", "MTPTTT0", "TPTTT0", 1 },
                    { new Guid("b67e69cb-60db-440e-9a0f-6fad7e8b6c4b"), "MPTTT4", "MTPTTT4", "TPTTT4", 1 },
                    { new Guid("eb995848-ccae-47b1-891a-445c9319179b"), "MPTTT3", "MTPTTT3", "TPTTT3", 0 },
                    { new Guid("f2fee31e-c5a6-4fe5-8684-6910143a03d0"), "MPTTT2", "MTPTTT2", "TPTTT2", 0 },
                    { new Guid("fbccf473-a33f-4a3c-8965-6808706865d9"), "MPTTT1", "MTPTTT1", "TPTTT1", 0 }
                });

            migrationBuilder.InsertData(
                table: "SanPham",
                columns: new[] { "IdSanPham", "MaSanPham", "TenSanPham", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("1c5b395a-1eee-48e4-89e5-c39edee15b28"), "SP4", "TSP4", 1 },
                    { new Guid("35b6bc42-831d-41a3-8ca2-9e9403abff3f"), "SP1", "TSP1", 0 }
                });

            migrationBuilder.InsertData(
                table: "SanPham",
                columns: new[] { "IdSanPham", "MaSanPham", "TenSanPham", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("54599620-ed04-4020-ab32-91a6db9dcde4"), "SP0", "TSP0", 1 },
                    { new Guid("a0c8dc5c-dac3-4d71-8fe4-e17be1bc10c5"), "SP2", "TSP2", 1 },
                    { new Guid("f83c0a33-922e-4a61-99b7-dc31bd14bf5a"), "SP3", "TSP3", 1 }
                });

            migrationBuilder.InsertData(
                table: "SanPhamYeuThichViewModels",
                columns: new[] { "Id", "AnhSanPham", "ChiTietSanPhamId", "DiaChi", "GiaBan", "GiaNhap", "GiaThucTe", "GioiTinh", "MaAnh", "MaNguoiDung", "MaSanPham", "MaSanPhamCT", "Mota", "SoDienThoai", "SoLuongDaBan", "SoLuongTon", "TenNguoiDung", "TenSanPham", "TrangThai", "TrangThaiCTSP", "TrangThaiKhuyenMai", "TrangThaiND", "URL", "VerificationCode", "VerificationCodeExpiry" },
                values: new object[,]
                {
                    { new Guid("32520ad3-39d8-4a81-a6dd-a3fbf128aaef"), "URL4", new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, "TSP4", 0, null, 0, 0, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("56de4bc5-e91e-4706-ba5c-21a0885d2544"), "URL1", new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, "TSP1", 0, null, 0, 0, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("56e86558-da30-4f6f-a442-f27b029fabeb"), "URL3", new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, "TSP3", 0, null, 0, 0, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d4ae6473-7741-45ff-bc9f-7590d70f2447"), "URL0", new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, "TSP0", 0, null, 0, 0, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fbf75ed9-fa0a-4b6f-817a-e598daaecf06"), "URL2", new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, null, null, null, null, null, null, null, null, null, null, null, "TSP2", 0, null, 0, 0, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ThuongHieu",
                columns: new[] { "Guid", "MaThuongHieu", "TenThuongHieu", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("0f497143-dbd8-4a75-8554-cd8bbee260af"), "MTH3", "TTH3", 1 },
                    { new Guid("7d3a3690-9963-440d-8369-2b9998564464"), "MTH4", "TTH4", 1 },
                    { new Guid("8f1cff3e-47cf-400b-a43e-acce4c031ab6"), "MTH0", "TTH0", 1 },
                    { new Guid("b88bf1da-0b62-42b9-bb12-1376a602c077"), "MTH2", "TTH2", 0 },
                    { new Guid("cb3f7283-8348-4553-8a9e-29e827b36329"), "MTH1", "TTH1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Voucher",
                columns: new[] { "Guid", "MaVoucher", "NgayBatDau", "NgayHetHan", "PhanTramGiam", "SoLuong", "TenVoucher", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("43ecde4d-0a46-45d1-ad86-68bf0d8fbc73"), "MVC1", new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8753), new DateTime(2024, 6, 10, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8753), 88, 7, "TVC1", 1 },
                    { new Guid("4c8d9c2a-4aef-49d2-9cac-19fdc9c30f11"), "MVC0", new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8444), new DateTime(2024, 6, 10, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8445), 86, 3, "TVC0", 1 },
                    { new Guid("7e0cc6bf-1e19-48fc-bcf6-569d0ec6a826"), "MVC2", new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8941), new DateTime(2024, 6, 12, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(8941), 56, 3, "TVC2", 1 },
                    { new Guid("94091485-3714-497b-961c-67c925abad85"), "MVC3", new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9120), new DateTime(2024, 5, 21, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9120), 68, 3, "TVC3", 0 },
                    { new Guid("cc82c5e1-cc7e-4043-a56a-67b5fcae9fc5"), "MVC4", new DateTime(2024, 5, 16, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9363), new DateTime(2024, 6, 5, 16, 34, 52, 687, DateTimeKind.Utc).AddTicks(9363), 45, 1, "TVC4", 0 }
                });

            migrationBuilder.InsertData(
                table: "XuatXu",
                columns: new[] { "Guid", "MaXuatXu", "TenXuatXu", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("388e582e-e112-487a-8f7e-0dea24ea7421"), "MXX3", "TXX3", 0 },
                    { new Guid("3a8b5061-df10-413f-9882-cf7fcca75fc3"), "MXX0", "TXX0", 1 },
                    { new Guid("4065adf3-e809-4d19-8613-a5fa5ab9e93c"), "MXX2", "TXX2", 1 },
                    { new Guid("5fa71732-6655-4df3-9da8-926be46b3c3b"), "MXX4", "TXX4", 1 },
                    { new Guid("6e1d14fa-e4c5-419a-924b-6e14d2a1f519"), "MXX1", "TXX1", 1 }
                });

            migrationBuilder.InsertData(
                table: "ChiTietSanPham",
                columns: new[] { "Id", "ChatLieuId", "GiaBan", "GiaNhap", "GiaThucTe", "LoaiId", "MaSanPham", "MauSacId", "Mota", "SanPhamId", "SoLuongDaBan", "SoLuongTon", "ThuongHieuId", "TrangThai", "TrangThaiKhuyenMai", "XuatXuId" },
                values: new object[,]
                {
                    { new Guid("15787cc5-1116-4f68-aa0e-f591cdb1ef96"), new Guid("a781c9ce-0609-4cf5-9016-57bd3230fb0d"), 974889.0, 463713.0, 510728.0, new Guid("1ef043d1-37b2-4fb4-aad9-ea5d00939bc6"), "SP2", new Guid("c7d3f38b-5968-4c0b-8790-68108b1b8621"), "mo ta2", new Guid("a0c8dc5c-dac3-4d71-8fe4-e17be1bc10c5"), 2, 2, new Guid("b88bf1da-0b62-42b9-bb12-1376a602c077"), 1, 1, new Guid("4065adf3-e809-4d19-8613-a5fa5ab9e93c") },
                    { new Guid("1dfbf615-38e7-491e-b593-f59422a4bc06"), new Guid("b84d31ac-9d63-481b-8027-1a074698bb84"), 142970.0, 618620.0, 996704.0, new Guid("1df6af10-f977-4db0-9f50-a135ddcdfd83"), "SP3", new Guid("15e45203-d243-4659-951c-22b8f1a4dc7d"), "mo ta3", new Guid("f83c0a33-922e-4a61-99b7-dc31bd14bf5a"), 3, 3, new Guid("0f497143-dbd8-4a75-8554-cd8bbee260af"), 0, 0, new Guid("388e582e-e112-487a-8f7e-0dea24ea7421") },
                    { new Guid("615ff1b2-dcd2-4a85-a66e-3a75e3d4ee9f"), new Guid("cb1c36d6-ae8e-4212-ae3c-7b44b6aab686"), 773490.0, 439904.0, 245721.0, new Guid("df1d404e-a91c-4f04-b122-8264a508b8e3"), "SP0", new Guid("fdbbaab8-34d6-4640-8f10-c2046829451a"), "mo ta0", new Guid("54599620-ed04-4020-ab32-91a6db9dcde4"), 0, 0, new Guid("8f1cff3e-47cf-400b-a43e-acce4c031ab6"), 0, 0, new Guid("3a8b5061-df10-413f-9882-cf7fcca75fc3") },
                    { new Guid("9fc49348-853b-4420-89bd-cf9b1e0bac6d"), new Guid("584f9f24-001e-4aad-826f-b5f4b710945f"), 463184.0, 275522.0, 386531.0, new Guid("a0b91fbf-bd40-454f-a94e-7c754e818cab"), "SP1", new Guid("f440a547-e36e-4276-8535-31b52174cbce"), "mo ta1", new Guid("35b6bc42-831d-41a3-8ca2-9e9403abff3f"), 1, 1, new Guid("cb3f7283-8348-4553-8a9e-29e827b36329"), 1, 1, new Guid("6e1d14fa-e4c5-419a-924b-6e14d2a1f519") },
                    { new Guid("d1e19dbe-8285-4ca7-b469-e67019a13aab"), new Guid("b29383d8-b17a-4572-8166-c94fe0382548"), 206664.0, 800041.0, 731068.0, new Guid("5b70e17d-1426-4b52-9de7-8a68b4717bb7"), "SP4", new Guid("74e0b1cb-a556-4bb7-8f20-eb6608d5f6d3"), "mo ta4", new Guid("1c5b395a-1eee-48e4-89e5-c39edee15b28"), 4, 4, new Guid("7d3a3690-9963-440d-8369-2b9998564464"), 1, 1, new Guid("5fa71732-6655-4df3-9da8-926be46b3c3b") }
                });

            migrationBuilder.InsertData(
                table: "PhuongThucTTChiTiet",
                columns: new[] { "Id", "HoaDonId", "PTTToanId", "PhuongThucThanhToanId", "SoTien", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("11257592-911a-4e02-bd3a-56e72eb37388"), new Guid("3173edf6-1311-45f1-914b-e9898d9261ed"), new Guid("b67e69cb-60db-440e-9a0f-6fad7e8b6c4b"), null, 689724.0, 0 },
                    { new Guid("2b5e64a0-bcad-4b7d-b652-aad575245868"), new Guid("470dd866-f29e-4bcd-80b4-29f937cdf060"), new Guid("fbccf473-a33f-4a3c-8965-6808706865d9"), null, 986825.0, 1 },
                    { new Guid("587b7ac4-c3cb-48e9-997c-276614ea84ae"), new Guid("077d1f6c-349c-4625-9cea-829cf965252b"), new Guid("f2fee31e-c5a6-4fe5-8684-6910143a03d0"), null, 943778.0, 0 },
                    { new Guid("9167f10f-ed66-44fd-9182-3bc792427556"), new Guid("14101749-75f7-47f9-99d4-b3f597af039c"), new Guid("4a6e5fbe-b6ee-4a96-91af-9c9a7d7732c7"), null, 734339.0, 0 },
                    { new Guid("dc4a9b62-442d-4369-be71-52fc414a52d3"), new Guid("749ebe10-465a-4267-899b-dcfd38e49605"), new Guid("eb995848-ccae-47b1-891a-445c9319179b"), null, 857646.0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Anh",
                columns: new[] { "Guid", "ChiTietSanPhamId", "MaAnh", "URL" },
                values: new object[,]
                {
                    { new Guid("4dd0cc87-767c-495b-b925-57e68e8a1628"), new Guid("615ff1b2-dcd2-4a85-a66e-3a75e3d4ee9f"), "MA0", "URL0" },
                    { new Guid("5233ce9d-de98-4dec-a731-1c7f40333e0a"), new Guid("1dfbf615-38e7-491e-b593-f59422a4bc06"), "MA3", "URL3" },
                    { new Guid("66c6a2fb-19c9-44e4-9171-c2d2f248e92e"), new Guid("d1e19dbe-8285-4ca7-b469-e67019a13aab"), "MA4", "URL4" },
                    { new Guid("792e6f5e-2e1b-404f-b241-43e86b792dfa"), new Guid("15787cc5-1116-4f68-aa0e-f591cdb1ef96"), "MA2", "URL2" },
                    { new Guid("e268fa30-4f64-4037-9312-c559ec38aa3a"), new Guid("9fc49348-853b-4420-89bd-cf9b1e0bac6d"), "MA1", "URL1" }
                });

            migrationBuilder.InsertData(
                table: "ChiTietKhuyenMai",
                columns: new[] { "Id", "ChiTietSanPhamId", "KhuyenMaiId", "Mota", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("32e0d208-ad8a-480c-b64f-c7f08372873d"), new Guid("1dfbf615-38e7-491e-b593-f59422a4bc06"), new Guid("65e73287-17ea-444b-aa88-c5b37c27d2e7"), "MTCTKM3", 1 },
                    { new Guid("710cb96c-d29c-406f-9997-680b90ebf8fd"), new Guid("9fc49348-853b-4420-89bd-cf9b1e0bac6d"), new Guid("6e4c0a24-0a37-4ccf-b913-57f14702a4d2"), "MTCTKM1", 1 },
                    { new Guid("7484b868-a82d-4c9a-9720-78162ed16384"), new Guid("15787cc5-1116-4f68-aa0e-f591cdb1ef96"), new Guid("23830357-bf3e-4ceb-8fb3-551da0f321ab"), "MTCTKM2", 1 },
                    { new Guid("a499e0f4-fe1e-46dc-9284-a77fcb63a9e0"), new Guid("d1e19dbe-8285-4ca7-b469-e67019a13aab"), new Guid("07e54863-9fd0-4b0c-987c-6ba69fecf361"), "MTCTKM4", 1 },
                    { new Guid("b45c0578-2bc2-46f7-8bd0-3f4ef459308b"), new Guid("615ff1b2-dcd2-4a85-a66e-3a75e3d4ee9f"), new Guid("a2dda9d4-ad65-4690-86d4-e001b699ba5f"), "MTCTKM0", 0 }
                });

            migrationBuilder.InsertData(
                table: "HoaDonChiTiet",
                columns: new[] { "Guid", "ChiTietSanPhamId", "GiaBan", "HoaDonId", "SoLuong" },
                values: new object[,]
                {
                    { new Guid("2da46e80-b26e-4d06-b1e6-d4c63c817300"), new Guid("15787cc5-1116-4f68-aa0e-f591cdb1ef96"), 753123.0, new Guid("077d1f6c-349c-4625-9cea-829cf965252b"), 54 },
                    { new Guid("804783c6-ec3f-489e-853b-e5e133f317e5"), new Guid("9fc49348-853b-4420-89bd-cf9b1e0bac6d"), 483200.0, new Guid("470dd866-f29e-4bcd-80b4-29f937cdf060"), 38 },
                    { new Guid("99f9b834-f0e6-4981-bb57-d0160ed86e77"), new Guid("d1e19dbe-8285-4ca7-b469-e67019a13aab"), 947471.0, new Guid("3173edf6-1311-45f1-914b-e9898d9261ed"), 21 },
                    { new Guid("9f3638f3-a220-4fdc-9c2a-afe722be690c"), new Guid("1dfbf615-38e7-491e-b593-f59422a4bc06"), 715665.0, new Guid("749ebe10-465a-4267-899b-dcfd38e49605"), 43 },
                    { new Guid("a05051e9-303e-4a89-9df6-8046f3f3b919"), new Guid("615ff1b2-dcd2-4a85-a66e-3a75e3d4ee9f"), 668757.0, new Guid("14101749-75f7-47f9-99d4-b3f597af039c"), 62 }
                });

            migrationBuilder.InsertData(
                table: "SanPhamYeuThich",
                columns: new[] { "Id", "ChiTietSanPhamId", "NguoiDungId", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("0a64ee5f-9bf1-4436-b165-1b7fa34d77e6"), new Guid("1dfbf615-38e7-491e-b593-f59422a4bc06"), new Guid("35172428-03d8-489d-ba34-a35534dcd22b"), 0 },
                    { new Guid("77ae0dc8-5b0f-48d9-b10a-d0be16cadfa9"), new Guid("15787cc5-1116-4f68-aa0e-f591cdb1ef96"), new Guid("a3383fbf-0883-4950-bb9f-87cb31040d44"), 1 },
                    { new Guid("7989650f-d673-4788-9549-2bd064534ddc"), new Guid("615ff1b2-dcd2-4a85-a66e-3a75e3d4ee9f"), new Guid("55d1353d-42be-4acb-a345-b920295d24dc"), 1 },
                    { new Guid("c7150d2b-9791-40c8-902c-0fa3176babe7"), new Guid("9fc49348-853b-4420-89bd-cf9b1e0bac6d"), new Guid("a5a5b1fb-5ab6-49de-b376-9d6cfddc502f"), 0 },
                    { new Guid("d20edc31-5fb0-47c2-aaea-35bee0999ae8"), new Guid("d1e19dbe-8285-4ca7-b469-e67019a13aab"), new Guid("abf2e001-7e62-42f8-b531-6c33b2d38a98"), 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GioHangChiTiet_GioHang_GioHangId",
                table: "GioHangChiTiet",
                column: "GioHangId",
                principalTable: "GioHang",
                principalColumn: "IdGh",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHangChiTiet_GioHang_GioHangId",
                table: "GioHangChiTiet");

            migrationBuilder.DropTable(
                name: "SanPhamYeuThichViewModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang");

            migrationBuilder.DeleteData(
                table: "Anh",
                keyColumn: "Guid",
                keyValue: new Guid("4dd0cc87-767c-495b-b925-57e68e8a1628"));

            migrationBuilder.DeleteData(
                table: "Anh",
                keyColumn: "Guid",
                keyValue: new Guid("5233ce9d-de98-4dec-a731-1c7f40333e0a"));

            migrationBuilder.DeleteData(
                table: "Anh",
                keyColumn: "Guid",
                keyValue: new Guid("66c6a2fb-19c9-44e4-9171-c2d2f248e92e"));

            migrationBuilder.DeleteData(
                table: "Anh",
                keyColumn: "Guid",
                keyValue: new Guid("792e6f5e-2e1b-404f-b241-43e86b792dfa"));

            migrationBuilder.DeleteData(
                table: "Anh",
                keyColumn: "Guid",
                keyValue: new Guid("e268fa30-4f64-4037-9312-c559ec38aa3a"));

            migrationBuilder.DeleteData(
                table: "ChiTietKhuyenMai",
                keyColumn: "Id",
                keyValue: new Guid("32e0d208-ad8a-480c-b64f-c7f08372873d"));

            migrationBuilder.DeleteData(
                table: "ChiTietKhuyenMai",
                keyColumn: "Id",
                keyValue: new Guid("710cb96c-d29c-406f-9997-680b90ebf8fd"));

            migrationBuilder.DeleteData(
                table: "ChiTietKhuyenMai",
                keyColumn: "Id",
                keyValue: new Guid("7484b868-a82d-4c9a-9720-78162ed16384"));

            migrationBuilder.DeleteData(
                table: "ChiTietKhuyenMai",
                keyColumn: "Id",
                keyValue: new Guid("a499e0f4-fe1e-46dc-9284-a77fcb63a9e0"));

            migrationBuilder.DeleteData(
                table: "ChiTietKhuyenMai",
                keyColumn: "Id",
                keyValue: new Guid("b45c0578-2bc2-46f7-8bd0-3f4ef459308b"));

            migrationBuilder.DeleteData(
                table: "GioHang",
                keyColumn: "IdGh",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("1c6950d0-f548-404c-84fb-321c059f9629"));

            migrationBuilder.DeleteData(
                table: "GioHang",
                keyColumn: "IdGh",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("7003b01f-0bb5-4100-91f7-68131a86712d"));

            migrationBuilder.DeleteData(
                table: "GioHang",
                keyColumn: "IdGh",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("8cc9ae39-9257-40f8-82c5-145f49b80ba0"));

            migrationBuilder.DeleteData(
                table: "GioHang",
                keyColumn: "IdGh",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("8f849e54-340d-4e0b-b84e-f144747d5313"));

            migrationBuilder.DeleteData(
                table: "GioHang",
                keyColumn: "IdGh",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("b0ddbe55-9753-4ba7-8345-e0c7afb8c2d6"));

            migrationBuilder.DeleteData(
                table: "HoaDonChiTiet",
                keyColumn: "Guid",
                keyValue: new Guid("2da46e80-b26e-4d06-b1e6-d4c63c817300"));

            migrationBuilder.DeleteData(
                table: "HoaDonChiTiet",
                keyColumn: "Guid",
                keyValue: new Guid("804783c6-ec3f-489e-853b-e5e133f317e5"));

            migrationBuilder.DeleteData(
                table: "HoaDonChiTiet",
                keyColumn: "Guid",
                keyValue: new Guid("99f9b834-f0e6-4981-bb57-d0160ed86e77"));

            migrationBuilder.DeleteData(
                table: "HoaDonChiTiet",
                keyColumn: "Guid",
                keyValue: new Guid("9f3638f3-a220-4fdc-9c2a-afe722be690c"));

            migrationBuilder.DeleteData(
                table: "HoaDonChiTiet",
                keyColumn: "Guid",
                keyValue: new Guid("a05051e9-303e-4a89-9df6-8046f3f3b919"));

            migrationBuilder.DeleteData(
                table: "PhuongThucTTChiTiet",
                keyColumn: "Id",
                keyValue: new Guid("11257592-911a-4e02-bd3a-56e72eb37388"));

            migrationBuilder.DeleteData(
                table: "PhuongThucTTChiTiet",
                keyColumn: "Id",
                keyValue: new Guid("2b5e64a0-bcad-4b7d-b652-aad575245868"));

            migrationBuilder.DeleteData(
                table: "PhuongThucTTChiTiet",
                keyColumn: "Id",
                keyValue: new Guid("587b7ac4-c3cb-48e9-997c-276614ea84ae"));

            migrationBuilder.DeleteData(
                table: "PhuongThucTTChiTiet",
                keyColumn: "Id",
                keyValue: new Guid("9167f10f-ed66-44fd-9182-3bc792427556"));

            migrationBuilder.DeleteData(
                table: "PhuongThucTTChiTiet",
                keyColumn: "Id",
                keyValue: new Guid("dc4a9b62-442d-4369-be71-52fc414a52d3"));

            migrationBuilder.DeleteData(
                table: "PhuongThucThanhToan",
                keyColumn: "Id",
                keyValue: new Guid("4a6e5fbe-b6ee-4a96-91af-9c9a7d7732c7"));

            migrationBuilder.DeleteData(
                table: "PhuongThucThanhToan",
                keyColumn: "Id",
                keyValue: new Guid("b67e69cb-60db-440e-9a0f-6fad7e8b6c4b"));

            migrationBuilder.DeleteData(
                table: "PhuongThucThanhToan",
                keyColumn: "Id",
                keyValue: new Guid("eb995848-ccae-47b1-891a-445c9319179b"));

            migrationBuilder.DeleteData(
                table: "PhuongThucThanhToan",
                keyColumn: "Id",
                keyValue: new Guid("f2fee31e-c5a6-4fe5-8684-6910143a03d0"));

            migrationBuilder.DeleteData(
                table: "PhuongThucThanhToan",
                keyColumn: "Id",
                keyValue: new Guid("fbccf473-a33f-4a3c-8965-6808706865d9"));

            migrationBuilder.DeleteData(
                table: "SanPhamYeuThich",
                keyColumn: "Id",
                keyValue: new Guid("0a64ee5f-9bf1-4436-b165-1b7fa34d77e6"));

            migrationBuilder.DeleteData(
                table: "SanPhamYeuThich",
                keyColumn: "Id",
                keyValue: new Guid("77ae0dc8-5b0f-48d9-b10a-d0be16cadfa9"));

            migrationBuilder.DeleteData(
                table: "SanPhamYeuThich",
                keyColumn: "Id",
                keyValue: new Guid("7989650f-d673-4788-9549-2bd064534ddc"));

            migrationBuilder.DeleteData(
                table: "SanPhamYeuThich",
                keyColumn: "Id",
                keyValue: new Guid("c7150d2b-9791-40c8-902c-0fa3176babe7"));

            migrationBuilder.DeleteData(
                table: "SanPhamYeuThich",
                keyColumn: "Id",
                keyValue: new Guid("d20edc31-5fb0-47c2-aaea-35bee0999ae8"));

            migrationBuilder.DeleteData(
                table: "Voucher",
                keyColumn: "Guid",
                keyValue: new Guid("43ecde4d-0a46-45d1-ad86-68bf0d8fbc73"));

            migrationBuilder.DeleteData(
                table: "Voucher",
                keyColumn: "Guid",
                keyValue: new Guid("4c8d9c2a-4aef-49d2-9cac-19fdc9c30f11"));

            migrationBuilder.DeleteData(
                table: "Voucher",
                keyColumn: "Guid",
                keyValue: new Guid("7e0cc6bf-1e19-48fc-bcf6-569d0ec6a826"));

            migrationBuilder.DeleteData(
                table: "Voucher",
                keyColumn: "Guid",
                keyValue: new Guid("94091485-3714-497b-961c-67c925abad85"));

            migrationBuilder.DeleteData(
                table: "Voucher",
                keyColumn: "Guid",
                keyValue: new Guid("cc82c5e1-cc7e-4043-a56a-67b5fcae9fc5"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("35172428-03d8-489d-ba34-a35534dcd22b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("55d1353d-42be-4acb-a345-b920295d24dc"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a3383fbf-0883-4950-bb9f-87cb31040d44"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a5a5b1fb-5ab6-49de-b376-9d6cfddc502f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("abf2e001-7e62-42f8-b531-6c33b2d38a98"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("15787cc5-1116-4f68-aa0e-f591cdb1ef96"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("1dfbf615-38e7-491e-b593-f59422a4bc06"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("615ff1b2-dcd2-4a85-a66e-3a75e3d4ee9f"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("9fc49348-853b-4420-89bd-cf9b1e0bac6d"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("d1e19dbe-8285-4ca7-b469-e67019a13aab"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("077d1f6c-349c-4625-9cea-829cf965252b"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("14101749-75f7-47f9-99d4-b3f597af039c"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("3173edf6-1311-45f1-914b-e9898d9261ed"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("470dd866-f29e-4bcd-80b4-29f937cdf060"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("749ebe10-465a-4267-899b-dcfd38e49605"));

            migrationBuilder.DeleteData(
                table: "Khuyenmai",
                keyColumn: "Id",
                keyValue: new Guid("07e54863-9fd0-4b0c-987c-6ba69fecf361"));

            migrationBuilder.DeleteData(
                table: "Khuyenmai",
                keyColumn: "Id",
                keyValue: new Guid("23830357-bf3e-4ceb-8fb3-551da0f321ab"));

            migrationBuilder.DeleteData(
                table: "Khuyenmai",
                keyColumn: "Id",
                keyValue: new Guid("65e73287-17ea-444b-aa88-c5b37c27d2e7"));

            migrationBuilder.DeleteData(
                table: "Khuyenmai",
                keyColumn: "Id",
                keyValue: new Guid("6e4c0a24-0a37-4ccf-b913-57f14702a4d2"));

            migrationBuilder.DeleteData(
                table: "Khuyenmai",
                keyColumn: "Id",
                keyValue: new Guid("a2dda9d4-ad65-4690-86d4-e001b699ba5f"));

            migrationBuilder.DeleteData(
                table: "ChatLieu",
                keyColumn: "Guid",
                keyValue: new Guid("584f9f24-001e-4aad-826f-b5f4b710945f"));

            migrationBuilder.DeleteData(
                table: "ChatLieu",
                keyColumn: "Guid",
                keyValue: new Guid("a781c9ce-0609-4cf5-9016-57bd3230fb0d"));

            migrationBuilder.DeleteData(
                table: "ChatLieu",
                keyColumn: "Guid",
                keyValue: new Guid("b29383d8-b17a-4572-8166-c94fe0382548"));

            migrationBuilder.DeleteData(
                table: "ChatLieu",
                keyColumn: "Guid",
                keyValue: new Guid("b84d31ac-9d63-481b-8027-1a074698bb84"));

            migrationBuilder.DeleteData(
                table: "ChatLieu",
                keyColumn: "Guid",
                keyValue: new Guid("cb1c36d6-ae8e-4212-ae3c-7b44b6aab686"));

            migrationBuilder.DeleteData(
                table: "Loai",
                keyColumn: "Id",
                keyValue: new Guid("1df6af10-f977-4db0-9f50-a135ddcdfd83"));

            migrationBuilder.DeleteData(
                table: "Loai",
                keyColumn: "Id",
                keyValue: new Guid("1ef043d1-37b2-4fb4-aad9-ea5d00939bc6"));

            migrationBuilder.DeleteData(
                table: "Loai",
                keyColumn: "Id",
                keyValue: new Guid("5b70e17d-1426-4b52-9de7-8a68b4717bb7"));

            migrationBuilder.DeleteData(
                table: "Loai",
                keyColumn: "Id",
                keyValue: new Guid("a0b91fbf-bd40-454f-a94e-7c754e818cab"));

            migrationBuilder.DeleteData(
                table: "Loai",
                keyColumn: "Id",
                keyValue: new Guid("df1d404e-a91c-4f04-b122-8264a508b8e3"));

            migrationBuilder.DeleteData(
                table: "MauSac",
                keyColumn: "Guid",
                keyValue: new Guid("15e45203-d243-4659-951c-22b8f1a4dc7d"));

            migrationBuilder.DeleteData(
                table: "MauSac",
                keyColumn: "Guid",
                keyValue: new Guid("74e0b1cb-a556-4bb7-8f20-eb6608d5f6d3"));

            migrationBuilder.DeleteData(
                table: "MauSac",
                keyColumn: "Guid",
                keyValue: new Guid("c7d3f38b-5968-4c0b-8790-68108b1b8621"));

            migrationBuilder.DeleteData(
                table: "MauSac",
                keyColumn: "Guid",
                keyValue: new Guid("f440a547-e36e-4276-8535-31b52174cbce"));

            migrationBuilder.DeleteData(
                table: "MauSac",
                keyColumn: "Guid",
                keyValue: new Guid("fdbbaab8-34d6-4640-8f10-c2046829451a"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("1c5b395a-1eee-48e4-89e5-c39edee15b28"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("35b6bc42-831d-41a3-8ca2-9e9403abff3f"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("54599620-ed04-4020-ab32-91a6db9dcde4"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("a0c8dc5c-dac3-4d71-8fe4-e17be1bc10c5"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("f83c0a33-922e-4a61-99b7-dc31bd14bf5a"));

            migrationBuilder.DeleteData(
                table: "ThuongHieu",
                keyColumn: "Guid",
                keyValue: new Guid("0f497143-dbd8-4a75-8554-cd8bbee260af"));

            migrationBuilder.DeleteData(
                table: "ThuongHieu",
                keyColumn: "Guid",
                keyValue: new Guid("7d3a3690-9963-440d-8369-2b9998564464"));

            migrationBuilder.DeleteData(
                table: "ThuongHieu",
                keyColumn: "Guid",
                keyValue: new Guid("8f1cff3e-47cf-400b-a43e-acce4c031ab6"));

            migrationBuilder.DeleteData(
                table: "ThuongHieu",
                keyColumn: "Guid",
                keyValue: new Guid("b88bf1da-0b62-42b9-bb12-1376a602c077"));

            migrationBuilder.DeleteData(
                table: "ThuongHieu",
                keyColumn: "Guid",
                keyValue: new Guid("cb3f7283-8348-4553-8a9e-29e827b36329"));

            migrationBuilder.DeleteData(
                table: "XuatXu",
                keyColumn: "Guid",
                keyValue: new Guid("388e582e-e112-487a-8f7e-0dea24ea7421"));

            migrationBuilder.DeleteData(
                table: "XuatXu",
                keyColumn: "Guid",
                keyValue: new Guid("3a8b5061-df10-413f-9882-cf7fcca75fc3"));

            migrationBuilder.DeleteData(
                table: "XuatXu",
                keyColumn: "Guid",
                keyValue: new Guid("4065adf3-e809-4d19-8613-a5fa5ab9e93c"));

            migrationBuilder.DeleteData(
                table: "XuatXu",
                keyColumn: "Guid",
                keyValue: new Guid("5fa71732-6655-4df3-9da8-926be46b3c3b"));

            migrationBuilder.DeleteData(
                table: "XuatXu",
                keyColumn: "Guid",
                keyValue: new Guid("6e1d14fa-e4c5-419a-924b-6e14d2a1f519"));

            migrationBuilder.DropColumn(
                name: "IdGh",
                table: "GioHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang",
                column: "IdNguoiDung");

            migrationBuilder.CreateTable(
                name: "ThongKeViewModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiaBan = table.Column<double>(type: "float", nullable: false),
                    GiaNhap = table.Column<double>(type: "float", nullable: false),
                    MaHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamThongKe = table.Column<int>(type: "int", nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayShip = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayThongKe = table.Column<int>(type: "int", nullable: false),
                    PhanTramLo = table.Column<double>(type: "float", nullable: false),
                    PhanTramLoiNhuan = table.Column<double>(type: "float", nullable: false),
                    SoLuongDaBan = table.Column<int>(type: "int", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThangThongKe = table.Column<int>(type: "int", nullable: false),
                    TienGiam = table.Column<double>(type: "float", nullable: false),
                    TienShip = table.Column<double>(type: "float", nullable: false),
                    TongTienThongKe = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKeViewModels", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GioHangChiTiet_GioHang_GioHangId",
                table: "GioHangChiTiet",
                column: "GioHangId",
                principalTable: "GioHang",
                principalColumn: "IdNguoiDung",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
