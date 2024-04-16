using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class datactsp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("1440dde6-fb8e-4ee2-9ee6-ce59090333f7"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("63dd89e9-8780-449a-b488-12f7fcbfe05c"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("84e846c5-d270-43eb-889e-68d30608501c"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("b252ae5c-ee4c-4612-a352-dda52bb152d4"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("f4ed5bee-9b96-49a4-8649-42794de12aee"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("791041b8-f1e9-43b3-abe9-e38d4c821890"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("82fb2ac6-266d-4fae-94cf-fa2d475857d1"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("9238fe90-69ef-4157-9623-4e6561091472"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("aab8495e-2730-42c4-9838-c6b7d5fca568"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("d00f3e2f-8b69-41f3-b75e-266ec9dcb738"));

            migrationBuilder.InsertData(
                table: "ChiTietSanPham",
                columns: new[] { "Id", "ChatLieuId", "GiaBan", "GiaNhap", "GiaThucTe", "LoaiId", "MaSanPham", "MauSacId", "Mota", "SanPhamId", "SoLuongDaBan", "SoLuongTon", "ThuongHieuId", "TrangThai", "TrangThaiKhuyenMai", "XuatXuId" },
                values: new object[,]
                {
                    { new Guid("0d654694-bd67-4f83-a657-5b60340b59ca"), null, 694035.0, 696635.0, 830321.0, null, "SP2", null, "mo ta2", null, 8, 5, null, 0, 0, null },
                    { new Guid("8c737d04-31ec-404b-b622-dc12c7d93cb7"), null, 502463.0, 131780.0, 677858.0, null, "SP5", null, "mo ta5", null, 8, 9, null, 0, 1, null },
                    { new Guid("dd07d0d6-0865-4a0b-83ca-fb0ede30423a"), null, 129957.0, 504809.0, 432475.0, null, "SP2", null, "mo ta4", null, 5, 6, null, 0, 1, null },
                    { new Guid("dff773cc-53e2-40f1-b7e2-f10ac8e4f24a"), null, 403781.0, 724415.0, 848790.0, null, "SP2", null, "mo ta5", null, 8, 8, null, 0, 0, null },
                    { new Guid("eaef531c-d1d2-49b5-af9d-8be4a0efd30d"), null, 589380.0, 498877.0, 190083.0, null, "SP2", null, "mo ta4", null, 5, 5, null, 1, 1, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("0d654694-bd67-4f83-a657-5b60340b59ca"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("8c737d04-31ec-404b-b622-dc12c7d93cb7"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("dd07d0d6-0865-4a0b-83ca-fb0ede30423a"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("dff773cc-53e2-40f1-b7e2-f10ac8e4f24a"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("eaef531c-d1d2-49b5-af9d-8be4a0efd30d"));

            migrationBuilder.InsertData(
                table: "HoaDon",
                columns: new[] { "Id", "LiDoHuy", "MaHoaDon", "MoTa", "NgayGiaoDuKien", "NgayNhan", "NgayShip", "NgayTao", "NgayThanhToan", "NguoiDungId", "TienGiam", "TienShip", "TongTien", "TrangThaiGiaoHang", "TrangThaiThanhToan", "VoucherId" },
                values: new object[,]
                {
                    { new Guid("1440dde6-fb8e-4ee2-9ee6-ce59090333f7"), "ly do huy5", "HD5", "mo ta3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 55204.0, 32114.0, 49706.0, 1, 1, null },
                    { new Guid("63dd89e9-8780-449a-b488-12f7fcbfe05c"), "ly do huy4", "HD3", "mo ta3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 68617.0, 18188.0, 107657.0, 0, 0, null },
                    { new Guid("84e846c5-d270-43eb-889e-68d30608501c"), "ly do huy3", "HD5", "mo ta3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 27712.0, 32877.0, 88232.0, 0, 1, null },
                    { new Guid("b252ae5c-ee4c-4612-a352-dda52bb152d4"), "ly do huy1", "HD3", "mo ta5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 44834.0, 21200.0, 65525.0, 0, 1, null },
                    { new Guid("f4ed5bee-9b96-49a4-8649-42794de12aee"), "ly do huy5", "HD2", "mo ta1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 81497.0, 38628.0, 61273.0, 1, 0, null }
                });

            migrationBuilder.InsertData(
                table: "ThongKe",
                columns: new[] { "Id", "ChiTietSanPhamId", "HoaDonId", "Nam", "Ngay", "SanPhamChiTietId", "Thang" },
                values: new object[,]
                {
                    { new Guid("791041b8-f1e9-43b3-abe9-e38d4c821890"), null, new Guid("00000000-0000-0000-0000-000000000000"), 2002, 23, new Guid("00000000-0000-0000-0000-000000000000"), 6 },
                    { new Guid("82fb2ac6-266d-4fae-94cf-fa2d475857d1"), null, new Guid("00000000-0000-0000-0000-000000000000"), 2017, 14, new Guid("00000000-0000-0000-0000-000000000000"), 1 },
                    { new Guid("9238fe90-69ef-4157-9623-4e6561091472"), null, new Guid("00000000-0000-0000-0000-000000000000"), 2004, 29, new Guid("00000000-0000-0000-0000-000000000000"), 8 },
                    { new Guid("aab8495e-2730-42c4-9838-c6b7d5fca568"), null, new Guid("00000000-0000-0000-0000-000000000000"), 2014, 22, new Guid("00000000-0000-0000-0000-000000000000"), 6 },
                    { new Guid("d00f3e2f-8b69-41f3-b75e-266ec9dcb738"), null, new Guid("00000000-0000-0000-0000-000000000000"), 2001, 27, new Guid("00000000-0000-0000-0000-000000000000"), 1 }
                });
        }
    }
}
