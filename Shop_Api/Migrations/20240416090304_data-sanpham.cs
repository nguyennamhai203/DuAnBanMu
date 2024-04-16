using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class datasanpham : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "ChiTietSanPham",
                columns: new[] { "Id", "ChatLieuId", "GiaBan", "GiaNhap", "GiaThucTe", "LoaiId", "MaSanPham", "MauSacId", "Mota", "SanPhamId", "SoLuongDaBan", "SoLuongTon", "ThuongHieuId", "TrangThai", "TrangThaiKhuyenMai", "XuatXuId" },
                values: new object[,]
                {
                    { new Guid("026abea6-ee8a-49bf-8412-ef50a65fc782"), null, 970655.0, 304109.0, 645530.0, null, "SP2", null, "mo ta3", null, 9, 9, null, 0, 1, null },
                    { new Guid("27179460-4307-4b24-a74c-c9cfb8e31bc8"), null, 533729.0, 253913.0, 689934.0, null, "SP3", null, "mo ta3", null, 8, 7, null, 1, 0, null },
                    { new Guid("4eadea45-ee6e-4fe9-924e-06baae44da51"), null, 696117.0, 851534.0, 116929.0, null, "SP4", null, "mo ta4", null, 4, 3, null, 1, 0, null },
                    { new Guid("7a439e74-92ca-433a-b184-58a906755da3"), null, 680127.0, 304275.0, 338206.0, null, "SP2", null, "mo ta2", null, 6, 7, null, 0, 1, null },
                    { new Guid("ad769cd2-bcf1-497b-871e-0ae05b38f1c8"), null, 487571.0, 539205.0, 870573.0, null, "SP5", null, "mo ta4", null, 5, 1, null, 1, 0, null }
                });

            migrationBuilder.InsertData(
                table: "HoaDon",
                columns: new[] { "Id", "LiDoHuy", "MaHoaDon", "MoTa", "NgayGiaoDuKien", "NgayNhan", "NgayShip", "NgayTao", "NgayThanhToan", "NguoiDungId", "TienGiam", "TienShip", "TongTien", "TrangThaiGiaoHang", "TrangThaiThanhToan", "VoucherId" },
                values: new object[,]
                {
                    { new Guid("00259551-d185-4011-a340-43a4facd8188"), "ly do huy5", "HD4", "mo ta4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 77415.0, 46304.0, 49889.0, 0, 0, null },
                    { new Guid("199be224-1b8e-4cb1-85c7-74767fe97838"), "ly do huy5", "HD3", "mo ta3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 57933.0, 37534.0, 41117.0, 1, 1, null },
                    { new Guid("96543049-7a1c-47e8-8bb6-e39ba6e149a9"), "ly do huy1", "HD3", "mo ta1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 19116.0, 14378.0, 118072.0, 0, 0, null },
                    { new Guid("c95ae31c-47fb-4782-aa91-65a563053f3b"), "ly do huy5", "HD2", "mo ta1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 40906.0, 14426.0, 145392.0, 1, 0, null },
                    { new Guid("ead64f2f-e36e-483c-9bca-0dd14e12459b"), "ly do huy1", "HD2", "mo ta5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 90274.0, 11515.0, 112057.0, 0, 1, null }
                });

            migrationBuilder.InsertData(
                table: "SanPham",
                columns: new[] { "IdSanPham", "MaSanPham", "TenSanPham", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("1cac3f0e-d55f-4e78-8bcb-e989e4b2bac3"), "SP4", "TSP4", 1 },
                    { new Guid("58ca5696-c8c1-457b-842f-96f5f456f97d"), "SP2", "TSP5", 0 },
                    { new Guid("bb2b2e47-07fb-4e65-ad6d-734bec89a806"), "SP5", "TSP5", 1 },
                    { new Guid("d4e8083d-3175-4436-be2a-84017d10a051"), "SP1", "TSP2", 1 },
                    { new Guid("ebf40002-0f1e-484e-a97e-ae8ec3988b0c"), "SP5", "TSP1", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("026abea6-ee8a-49bf-8412-ef50a65fc782"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("27179460-4307-4b24-a74c-c9cfb8e31bc8"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("4eadea45-ee6e-4fe9-924e-06baae44da51"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("7a439e74-92ca-433a-b184-58a906755da3"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("ad769cd2-bcf1-497b-871e-0ae05b38f1c8"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("00259551-d185-4011-a340-43a4facd8188"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("199be224-1b8e-4cb1-85c7-74767fe97838"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("96543049-7a1c-47e8-8bb6-e39ba6e149a9"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("c95ae31c-47fb-4782-aa91-65a563053f3b"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("ead64f2f-e36e-483c-9bca-0dd14e12459b"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("1cac3f0e-d55f-4e78-8bcb-e989e4b2bac3"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("58ca5696-c8c1-457b-842f-96f5f456f97d"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("bb2b2e47-07fb-4e65-ad6d-734bec89a806"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("d4e8083d-3175-4436-be2a-84017d10a051"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("ebf40002-0f1e-484e-a97e-ae8ec3988b0c"));

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
    }
}
