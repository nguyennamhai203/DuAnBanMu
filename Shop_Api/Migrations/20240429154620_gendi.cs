using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class gendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ChiTietSanPham",
                columns: new[] { "Id", "ChatLieuId", "GiaBan", "GiaNhap", "GiaThucTe", "LoaiId", "MaSanPham", "MauSacId", "Mota", "SanPhamId", "SoLuongDaBan", "SoLuongTon", "ThuongHieuId", "TrangThai", "TrangThaiKhuyenMai", "XuatXuId" },
                values: new object[,]
                {
                    { new Guid("8c6d452f-1d24-4df9-a379-9a3228c51068"), null, 229229.0, 789900.0, 221200.0, null, "SP3", null, "mo ta5", null, 4, 2, null, 1, 0, null },
                    { new Guid("a70ed030-ec61-4b77-8c07-3ceee43fed1b"), null, 281610.0, 282760.0, 931211.0, null, "SP2", null, "mo ta4", null, 2, 9, null, 1, 1, null },
                    { new Guid("be7cc791-74a1-4c8f-88b6-99a1dcd35b35"), null, 727682.0, 511515.0, 725149.0, null, "SP4", null, "mo ta2", null, 2, 1, null, 0, 0, null },
                    { new Guid("df3c35a5-02e0-46ee-9f88-61157bc16bed"), null, 684261.0, 503469.0, 353417.0, null, "SP1", null, "mo ta3", null, 5, 4, null, 1, 0, null },
                    { new Guid("f1ec4cbb-6269-48f9-bbda-cc33fd4e7744"), null, 435263.0, 391736.0, 142104.0, null, "SP1", null, "mo ta2", null, 2, 6, null, 0, 1, null }
                });

            migrationBuilder.InsertData(
                table: "HoaDon",
                columns: new[] { "Id", "DiaChiGiaoHang", "LiDoHuy", "MaHoaDon", "MoTa", "NgayGiaoDuKien", "NgayNhan", "NgayShip", "NgayTao", "NgayThanhToan", "NguoiDungId", "SoDienThoai", "TenKhachHang", "TienGiam", "TienShip", "TongTien", "TrangThaiGiaoHang", "TrangThaiThanhToan", "VoucherId" },
                values: new object[,]
                {
                    { new Guid("0176c2db-08e5-4adf-9c8a-474b67313d11"), null, "ly do huy5", "HD3", "mo ta3", new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2920), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2918), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2918), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2917), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2918), null, null, null, 84569.0, 25797.0, 81302.0, 1, 0, null },
                    { new Guid("6adde222-c9f4-404a-a95a-84e3ac0b69b2"), null, "ly do huy1", "HD1", "mo ta2", new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2872), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2870), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2870), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2869), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2869), null, null, null, 48826.0, 34821.0, 101652.0, 1, 0, null },
                    { new Guid("ceab2da8-e87c-422e-ab04-5223b8ad91bb"), null, "ly do huy3", "HD4", "mo ta4", new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2841), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2839), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2839), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2838), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2838), null, null, null, 26162.0, 23548.0, 52382.0, 1, 1, null },
                    { new Guid("cebb1254-a623-4d1e-a4a5-d3123c31feea"), null, "ly do huy5", "HD2", "mo ta2", new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2897), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2896), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2895), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2894), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2895), null, null, null, 84838.0, 33675.0, 128045.0, 0, 1, null },
                    { new Guid("dba5b4f7-6354-4ada-b3c8-0fa411601d22"), null, "ly do huy2", "HD4", "mo ta1", new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2784), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2781), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2781), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2777), new DateTime(2024, 4, 29, 15, 46, 20, 276, DateTimeKind.Utc).AddTicks(2780), null, null, null, 31987.0, 17344.0, 46367.0, 0, 0, null }
                });

            migrationBuilder.InsertData(
                table: "SanPham",
                columns: new[] { "IdSanPham", "MaSanPham", "TenSanPham", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("207647b0-bc76-42f5-bee3-2bdfb896f9b9"), "SP1", "TSP5", 0 },
                    { new Guid("2f36bc6e-638d-4df3-90bd-a3a1c2618877"), "SP4", "TSP2", 1 },
                    { new Guid("352270f1-aa82-4b3f-9a4e-e52a94943734"), "SP5", "TSP1", 0 },
                    { new Guid("b58fa4a8-2c5b-449b-adc7-c0257a42217e"), "SP5", "TSP3", 1 },
                    { new Guid("d4cc7071-f519-428c-9a81-01433c08abd0"), "SP1", "TSP5", 0 }
                });

            migrationBuilder.InsertData(
                table: "ThongKe",
                columns: new[] { "Id", "ChiTietSanPhamId", "HoaDonId", "Nam", "Ngay", "SanPhamChiTietId", "Thang", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("270d0924-b506-474f-8a9c-e0172cb2f2e0"), null, new Guid("ceab2da8-e87c-422e-ab04-5223b8ad91bb"), 2012, 18, new Guid("a70ed030-ec61-4b77-8c07-3ceee43fed1b"), 10, 1 },
                    { new Guid("6a12b335-8ffc-44f7-9edc-3305b0d08438"), null, new Guid("dba5b4f7-6354-4ada-b3c8-0fa411601d22"), 2021, 7, new Guid("be7cc791-74a1-4c8f-88b6-99a1dcd35b35"), 2, 1 },
                    { new Guid("957786f2-c8a6-46f9-a3bc-7891a943cae8"), null, new Guid("0176c2db-08e5-4adf-9c8a-474b67313d11"), 2002, 23, new Guid("f1ec4cbb-6269-48f9-bbda-cc33fd4e7744"), 7, 1 },
                    { new Guid("c3d6ec62-2881-4c40-8618-60fe54af250f"), null, new Guid("6adde222-c9f4-404a-a95a-84e3ac0b69b2"), 2012, 14, new Guid("8c6d452f-1d24-4df9-a379-9a3228c51068"), 10, 0 },
                    { new Guid("f098522d-7421-40be-9bce-0c9f48912b4f"), null, new Guid("cebb1254-a623-4d1e-a4a5-d3123c31feea"), 2011, 14, new Guid("df3c35a5-02e0-46ee-9f88-61157bc16bed"), 9, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("8c6d452f-1d24-4df9-a379-9a3228c51068"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("a70ed030-ec61-4b77-8c07-3ceee43fed1b"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("be7cc791-74a1-4c8f-88b6-99a1dcd35b35"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("df3c35a5-02e0-46ee-9f88-61157bc16bed"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("f1ec4cbb-6269-48f9-bbda-cc33fd4e7744"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("207647b0-bc76-42f5-bee3-2bdfb896f9b9"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("2f36bc6e-638d-4df3-90bd-a3a1c2618877"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("352270f1-aa82-4b3f-9a4e-e52a94943734"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("b58fa4a8-2c5b-449b-adc7-c0257a42217e"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("d4cc7071-f519-428c-9a81-01433c08abd0"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("270d0924-b506-474f-8a9c-e0172cb2f2e0"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("6a12b335-8ffc-44f7-9edc-3305b0d08438"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("957786f2-c8a6-46f9-a3bc-7891a943cae8"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("c3d6ec62-2881-4c40-8618-60fe54af250f"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("f098522d-7421-40be-9bce-0c9f48912b4f"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("0176c2db-08e5-4adf-9c8a-474b67313d11"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("6adde222-c9f4-404a-a95a-84e3ac0b69b2"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("ceab2da8-e87c-422e-ab04-5223b8ad91bb"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("cebb1254-a623-4d1e-a4a5-d3123c31feea"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("dba5b4f7-6354-4ada-b3c8-0fa411601d22"));
        }
    }
}
