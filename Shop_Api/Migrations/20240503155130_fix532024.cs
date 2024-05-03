using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class fix532024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0288c956-4ef7-495c-a37a-1b7bcae6fd4d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1e4412a1-2a7a-466e-84be-e5c019889f48"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("855410c7-2e8c-464a-a5ba-e0b1b78da818"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a78c34df-5ef1-411c-9d7e-c6eaf56048de"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d702698a-ccde-405d-b2b6-3d148dba9378"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("24cdfa90-76ab-4448-919c-c175b9a04713"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("262ffd9c-5231-497e-8f14-76c4f5c4b6a7"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("2f074778-600e-4113-9c9b-6c07eb2cc3e8"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("eb924dd1-250f-4177-8fac-9dbd05bc14d8"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("fa3b8213-2f30-4865-9c79-b211a7a9a8f6"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("13926959-5b3b-4f52-8480-e690344cdca0"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("47915583-bee4-4e49-86eb-6ae75b0b5d0e"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("798a692e-eadc-4316-8310-1b8e4253299a"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("963ac5fb-569b-4919-a090-8c3ad3fd4ba4"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("9fadc732-aa1e-4f80-974b-9669609552b2"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("0f4972e1-c04c-4923-aa08-5850a797d933"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("1bfe19ee-d3ce-4391-9957-954d2cbd74f8"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("5a91595a-b996-49db-84cc-99f0034bd734"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("ae7c0418-ac09-490f-8eba-86360767cbed"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("e7dbcea7-6e00-4dae-9f9e-a44351a044e2"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("5110984b-70b7-40fb-a2a4-f18546cd36c8"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("87e0b74d-98de-4bd1-9ada-f48c609a21e3"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("b59add71-d695-4972-bfef-1e5037570c40"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("c56e481c-3981-4070-9476-d1476dcdd2e3"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("d32acd23-6b45-4564-95e0-ccd8f44ef389"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DiaChi", "Email", "EmailConfirmed", "GioiTinh", "LockoutEnabled", "LockoutEnd", "MaNguoiDung", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SoDienThoai", "TenNguoiDung", "TrangThai", "TwoFactorEnabled", "UserName", "VerificationCode", "VerificationCodeExpiry" },
                values: new object[,]
                {
                    { new Guid("0288c956-4ef7-495c-a37a-1b7bcae6fd4d"), 0, "c04e1257-2fc2-4989-b1d7-5adeae9c165d", "DC1", null, false, true, false, null, "ND1", null, null, null, null, false, null, "071381178", "TND2", 1, false, null, "VC1", new DateTime(2024, 5, 1, 5, 41, 3, 346, DateTimeKind.Utc).AddTicks(9540) },
                    { new Guid("1e4412a1-2a7a-466e-84be-e5c019889f48"), 0, "ec004531-eda3-41f3-bb6b-a2fe8aa238f5", "DC4", null, false, true, false, null, "ND3", null, null, null, null, false, null, "038070307", "TND2", 0, false, null, "VC4", new DateTime(2024, 4, 30, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9499) },
                    { new Guid("855410c7-2e8c-464a-a5ba-e0b1b78da818"), 0, "72cd5cdf-1732-49e0-af13-6ca43a5e8a72", "DC2", null, false, false, false, null, "ND2", null, null, null, null, false, null, "053404051", "TND4", 0, false, null, "VC4", new DateTime(2024, 4, 30, 4, 41, 3, 346, DateTimeKind.Utc).AddTicks(9514) },
                    { new Guid("a78c34df-5ef1-411c-9d7e-c6eaf56048de"), 0, "8ac7f638-09ae-4443-8af2-9b9b59358ee1", "DC5", null, false, false, false, null, "ND4", null, null, null, null, false, null, "076156585", "TND3", 0, false, null, "VC1", new DateTime(2024, 5, 1, 5, 41, 3, 346, DateTimeKind.Utc).AddTicks(9527) },
                    { new Guid("d702698a-ccde-405d-b2b6-3d148dba9378"), 0, "616b4b2c-9798-4333-92d2-02f365049ef8", "DC4", null, false, false, false, null, "ND2", null, null, null, null, false, null, "097298861", "TND1", 1, false, null, "VC2", new DateTime(2024, 4, 30, 13, 41, 3, 346, DateTimeKind.Utc).AddTicks(9461) }
                });

            migrationBuilder.InsertData(
                table: "ChiTietSanPham",
                columns: new[] { "Id", "ChatLieuId", "GiaBan", "GiaNhap", "GiaThucTe", "LoaiId", "MaSanPham", "MauSacId", "Mota", "SanPhamId", "SoLuongDaBan", "SoLuongTon", "ThuongHieuId", "TrangThai", "TrangThaiKhuyenMai", "XuatXuId" },
                values: new object[,]
                {
                    { new Guid("24cdfa90-76ab-4448-919c-c175b9a04713"), null, 185334.0, 910114.0, 450145.0, null, "SP4", null, "mo ta4", null, 8, 5, null, 0, 1, null },
                    { new Guid("262ffd9c-5231-497e-8f14-76c4f5c4b6a7"), null, 589405.0, 660748.0, 366740.0, null, "SP2", null, "mo ta2", null, 8, 2, null, 1, 1, null },
                    { new Guid("2f074778-600e-4113-9c9b-6c07eb2cc3e8"), null, 754190.0, 410859.0, 570426.0, null, "SP4", null, "mo ta5", null, 1, 3, null, 0, 1, null },
                    { new Guid("eb924dd1-250f-4177-8fac-9dbd05bc14d8"), null, 674420.0, 481583.0, 454115.0, null, "SP4", null, "mo ta5", null, 2, 8, null, 0, 1, null },
                    { new Guid("fa3b8213-2f30-4865-9c79-b211a7a9a8f6"), null, 785447.0, 683014.0, 952117.0, null, "SP2", null, "mo ta3", null, 4, 6, null, 1, 1, null }
                });

            migrationBuilder.InsertData(
                table: "HoaDon",
                columns: new[] { "Id", "DiaChiGiaoHang", "LiDoHuy", "MaHoaDon", "MoTa", "NgayGiaoDuKien", "NgayNhan", "NgayShip", "NgayTao", "NgayThanhToan", "NguoiDungId", "SoDienThoai", "TenKhachHang", "TienGiam", "TienShip", "TongTien", "TrangThaiGiaoHang", "TrangThaiThanhToan", "VoucherId" },
                values: new object[,]
                {
                    { new Guid("5110984b-70b7-40fb-a2a4-f18546cd36c8"), null, "ly do huy4", "HD1", "mo ta4", new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9662), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9659), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9659), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9654), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9655), null, null, null, 70679.0, 14039.0, 64551.0, 1, 0, null },
                    { new Guid("87e0b74d-98de-4bd1-9ada-f48c609a21e3"), null, "ly do huy4", "HD2", "mo ta3", new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9839), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9837), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9836), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9836), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9836), null, null, null, 68053.0, 26960.0, 77065.0, 1, 1, null },
                    { new Guid("b59add71-d695-4972-bfef-1e5037570c40"), null, "ly do huy1", "HD2", "mo ta1", new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9737), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9735), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9735), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9734), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9734), null, null, null, 67657.0, 14087.0, 98207.0, 1, 0, null },
                    { new Guid("c56e481c-3981-4070-9476-d1476dcdd2e3"), null, "ly do huy4", "HD3", "mo ta2", new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9786), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9785), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9785), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9784), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9784), null, null, null, 56581.0, 47148.0, 41775.0, 1, 1, null },
                    { new Guid("d32acd23-6b45-4564-95e0-ccd8f44ef389"), null, "ly do huy3", "HD3", "mo ta5", new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9765), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9763), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9763), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9762), new DateTime(2024, 4, 29, 17, 41, 3, 346, DateTimeKind.Utc).AddTicks(9763), null, null, null, 53069.0, 19348.0, 44495.0, 1, 0, null }
                });

            migrationBuilder.InsertData(
                table: "SanPham",
                columns: new[] { "IdSanPham", "MaSanPham", "TenSanPham", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("13926959-5b3b-4f52-8480-e690344cdca0"), "SP2", "TSP2", 1 },
                    { new Guid("47915583-bee4-4e49-86eb-6ae75b0b5d0e"), "SP3", "TSP2", 0 },
                    { new Guid("798a692e-eadc-4316-8310-1b8e4253299a"), "SP5", "TSP1", 0 },
                    { new Guid("963ac5fb-569b-4919-a090-8c3ad3fd4ba4"), "SP5", "TSP3", 0 },
                    { new Guid("9fadc732-aa1e-4f80-974b-9669609552b2"), "SP5", "TSP1", 1 }
                });

            migrationBuilder.InsertData(
                table: "ThongKe",
                columns: new[] { "Id", "ChiTietSanPhamId", "HoaDonId", "Nam", "Ngay", "SanPhamChiTietId", "Thang", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("0f4972e1-c04c-4923-aa08-5850a797d933"), null, new Guid("d32acd23-6b45-4564-95e0-ccd8f44ef389"), 2022, 21, new Guid("24cdfa90-76ab-4448-919c-c175b9a04713"), 6, 0 },
                    { new Guid("1bfe19ee-d3ce-4391-9957-954d2cbd74f8"), null, new Guid("b59add71-d695-4972-bfef-1e5037570c40"), 2022, 4, new Guid("262ffd9c-5231-497e-8f14-76c4f5c4b6a7"), 11, 1 },
                    { new Guid("5a91595a-b996-49db-84cc-99f0034bd734"), null, new Guid("5110984b-70b7-40fb-a2a4-f18546cd36c8"), 2019, 20, new Guid("2f074778-600e-4113-9c9b-6c07eb2cc3e8"), 11, 0 },
                    { new Guid("ae7c0418-ac09-490f-8eba-86360767cbed"), null, new Guid("87e0b74d-98de-4bd1-9ada-f48c609a21e3"), 2023, 21, new Guid("eb924dd1-250f-4177-8fac-9dbd05bc14d8"), 12, 1 },
                    { new Guid("e7dbcea7-6e00-4dae-9f9e-a44351a044e2"), null, new Guid("c56e481c-3981-4070-9476-d1476dcdd2e3"), 2021, 12, new Guid("fa3b8213-2f30-4865-9c79-b211a7a9a8f6"), 9, 0 }
                });
        }
    }
}
