using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class db8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("6bf0e746-a1cd-46af-8a5a-ef31a0146ed9"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("82f8ab51-cf2a-40a9-ab0d-f80d2316889b"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("a0165caa-1439-44d0-8f5b-da6361e268f9"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("b4369cf7-90a7-4c5e-8055-6f4c6883d65b"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("e69656b2-9cff-4108-87fd-96b72b5c444d"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("0aeeda2f-7c73-40ec-b5d5-0681f435c2da"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("2208cf49-19fb-46ec-becd-59c2aeef6d49"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("293a31d2-28f2-4a4b-b3ad-73d1e67b30f9"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("78e2507c-35d3-4257-a50d-8c74a57c3288"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("b4548276-2a45-4ce9-84c9-119920b356fa"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("0c5fcf83-f47b-46cb-b4da-32520c89e38f"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("42155eb0-e16b-4153-a8c9-9461a9f5d5aa"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("89d8f2fa-f40d-4e0e-95dd-62a76fa04365"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("d2654b8e-1e30-471b-9ff3-252fe258f423"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("db8242e6-0bb5-4bcd-82f0-3dbc4c06043d"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("3e58d91f-056d-4cc3-ad61-50e9c15e91b5"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("ab4b6507-61ce-420e-bb40-61509184dabe"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("c2678717-39b5-41e6-97c8-ed5d2a9630cb"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("f273b63b-9e1f-46ee-b9c6-f91e83bf9545"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("f86787fb-4044-41c2-bfb1-8d23a78aff83"));

            migrationBuilder.CreateTable(
                name: "ThongKeViewModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaBan = table.Column<double>(type: "float", nullable: false),
                    GiaNhap = table.Column<double>(type: "float", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    SoLuongDaBan = table.Column<int>(type: "int", nullable: false),
                    MaHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayShip = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayThongKe = table.Column<int>(type: "int", nullable: false),
                    ThangThongKe = table.Column<int>(type: "int", nullable: false),
                    NamThongKe = table.Column<int>(type: "int", nullable: false),
                    TienGiam = table.Column<double>(type: "float", nullable: false),
                    TienShip = table.Column<double>(type: "float", nullable: false),
                    TongTienThongKe = table.Column<double>(type: "float", nullable: false),
                    PhanTramLoiNhuan = table.Column<double>(type: "float", nullable: false),
                    PhanTramLo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKeViewModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongKeViewModels");

            migrationBuilder.InsertData(
                table: "ChiTietSanPham",
                columns: new[] { "Id", "ChatLieuId", "GiaBan", "GiaNhap", "GiaThucTe", "LoaiId", "MaSanPham", "MauSacId", "Mota", "SanPhamId", "SoLuongDaBan", "SoLuongTon", "ThuongHieuId", "TrangThai", "TrangThaiKhuyenMai", "XuatXuId" },
                values: new object[,]
                {
                    { new Guid("6bf0e746-a1cd-46af-8a5a-ef31a0146ed9"), null, 550439.0, 494166.0, 995581.0, null, "SP5", null, "mo ta3", null, 1, 2, null, 0, 0, null },
                    { new Guid("82f8ab51-cf2a-40a9-ab0d-f80d2316889b"), null, 328580.0, 940495.0, 932171.0, null, "SP2", null, "mo ta2", null, 8, 4, null, 0, 1, null },
                    { new Guid("a0165caa-1439-44d0-8f5b-da6361e268f9"), null, 817778.0, 628824.0, 582753.0, null, "SP1", null, "mo ta2", null, 8, 3, null, 0, 0, null },
                    { new Guid("b4369cf7-90a7-4c5e-8055-6f4c6883d65b"), null, 914111.0, 912974.0, 841163.0, null, "SP2", null, "mo ta3", null, 5, 8, null, 1, 0, null },
                    { new Guid("e69656b2-9cff-4108-87fd-96b72b5c444d"), null, 519281.0, 259129.0, 551145.0, null, "SP3", null, "mo ta1", null, 2, 1, null, 1, 0, null }
                });

            migrationBuilder.InsertData(
                table: "HoaDon",
                columns: new[] { "Id", "DiaChiGiaoHang", "LiDoHuy", "MaHoaDon", "MoTa", "NgayGiaoDuKien", "NgayNhan", "NgayShip", "NgayTao", "NgayThanhToan", "NguoiDungId", "SoDienThoai", "TenKhachHang", "TienGiam", "TienShip", "TongTien", "TrangThaiGiaoHang", "TrangThaiThanhToan", "VoucherId" },
                values: new object[,]
                {
                    { new Guid("0aeeda2f-7c73-40ec-b5d5-0681f435c2da"), null, "ly do huy4", "HD2", "mo ta2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 90214.0, 24073.0, 132137.0, 0, 1, null },
                    { new Guid("2208cf49-19fb-46ec-becd-59c2aeef6d49"), null, "ly do huy5", "HD5", "mo ta1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 64670.0, 24421.0, 99281.0, 1, 0, null },
                    { new Guid("293a31d2-28f2-4a4b-b3ad-73d1e67b30f9"), null, "ly do huy1", "HD5", "mo ta2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 70866.0, 41974.0, 90655.0, 1, 1, null },
                    { new Guid("78e2507c-35d3-4257-a50d-8c74a57c3288"), null, "ly do huy3", "HD1", "mo ta1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 97160.0, 16459.0, 125386.0, 1, 1, null },
                    { new Guid("b4548276-2a45-4ce9-84c9-119920b356fa"), null, "ly do huy5", "HD5", "mo ta3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 41073.0, 29589.0, 103344.0, 1, 0, null }
                });

            migrationBuilder.InsertData(
                table: "SanPham",
                columns: new[] { "IdSanPham", "MaSanPham", "TenSanPham", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("0c5fcf83-f47b-46cb-b4da-32520c89e38f"), "SP5", "TSP1", 0 },
                    { new Guid("42155eb0-e16b-4153-a8c9-9461a9f5d5aa"), "SP5", "TSP2", 1 },
                    { new Guid("89d8f2fa-f40d-4e0e-95dd-62a76fa04365"), "SP4", "TSP1", 0 },
                    { new Guid("d2654b8e-1e30-471b-9ff3-252fe258f423"), "SP5", "TSP3", 1 },
                    { new Guid("db8242e6-0bb5-4bcd-82f0-3dbc4c06043d"), "SP1", "TSP2", 0 }
                });

            migrationBuilder.InsertData(
                table: "ThongKe",
                columns: new[] { "Id", "ChiTietSanPhamId", "HoaDonId", "Nam", "Ngay", "SanPhamChiTietId", "Thang", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("3e58d91f-056d-4cc3-ad61-50e9c15e91b5"), null, new Guid("6bf6b4fa-40e2-45a2-8fda-3642a18fa2a6"), 2020, 24, new Guid("014decc6-6302-4c5c-888f-70a111f813fd"), 4, 0 },
                    { new Guid("ab4b6507-61ce-420e-bb40-61509184dabe"), null, new Guid("8d614bb9-0105-4d38-95d4-b89aea185d93"), 2018, 8, new Guid("51e8eb44-7cec-44e8-81d5-d3895da94cb5"), 12, 1 },
                    { new Guid("c2678717-39b5-41e6-97c8-ed5d2a9630cb"), null, new Guid("a1749dee-e05e-42b2-8ec5-2f593bc49e72"), 2008, 27, new Guid("63736132-db68-4e3b-b0c1-c752a4a91584"), 12, 1 },
                    { new Guid("f273b63b-9e1f-46ee-b9c6-f91e83bf9545"), null, new Guid("a072d595-d958-4a37-b7e9-fff60796f3bc"), 2015, 3, new Guid("3f3739f9-7965-4750-933c-da2cc216f45a"), 10, 0 },
                    { new Guid("f86787fb-4044-41c2-bfb1-8d23a78aff83"), null, new Guid("e97129bc-b754-4d8a-b4ed-7ed25bc8b927"), 2008, 11, new Guid("b69c1b0a-bcd4-4347-beef-67be43f5e252"), 10, 0 }
                });
        }
    }
}
