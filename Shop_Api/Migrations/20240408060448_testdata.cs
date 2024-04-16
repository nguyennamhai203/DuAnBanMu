﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class testdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("6964a07b-d7fd-4b98-b0f4-8e7786ca4e10"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("81bf5de4-d224-4276-a0c9-a3974250230f"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("82b05715-72c3-453b-bca1-a8ab9ad7149c"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("9a6d9240-b356-4c12-a134-acb283c5b0aa"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("aa012160-1ae4-4eb2-9e52-3bdf8ab73aaf"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("0acf1885-5217-4f8f-8c25-bbb7a4b8675d"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("35663684-f3a6-421d-9e5d-3a91728bd63e"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("4e497702-5b45-49e5-95b3-9800b7ad283b"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("54e7eb28-0af9-45c5-8d77-2033cf1f5099"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("9084ee76-6477-48de-8dd3-9160ddac97fc"));

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
                    { new Guid("791041b8-f1e9-43b3-abe9-e38d4c821890"), null, new Guid("1440dde6-fb8e-4ee2-9ee6-ce59090333f7"), 2002, 23, new Guid("00000000-0000-0000-0000-000000000000"), 6 },
                    { new Guid("82fb2ac6-266d-4fae-94cf-fa2d475857d1"), null, new Guid("63dd89e9-8780-449a-b488-12f7fcbfe05c"), 2017, 14, new Guid("00000000-0000-0000-0000-000000000000"), 1 },
                    { new Guid("9238fe90-69ef-4157-9623-4e6561091472"), null, new Guid("84e846c5-d270-43eb-889e-68d30608501c"), 2004, 29, new Guid("00000000-0000-0000-0000-000000000000"), 8 },
                    { new Guid("aab8495e-2730-42c4-9838-c6b7d5fca568"), null, new Guid("b252ae5c-ee4c-4612-a352-dda52bb152d4"), 2014, 22, new Guid("00000000-0000-0000-0000-000000000000"), 6 },
                    { new Guid("d00f3e2f-8b69-41f3-b75e-266ec9dcb738"), null, new Guid("f4ed5bee-9b96-49a4-8649-42794de12aee"), 2001, 27, new Guid("00000000-0000-0000-0000-000000000000"), 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "HoaDon",
                columns: new[] { "Id", "LiDoHuy", "MaHoaDon", "MoTa", "NgayGiaoDuKien", "NgayNhan", "NgayShip", "NgayTao", "NgayThanhToan", "NguoiDungId", "TienGiam", "TienShip", "TongTien", "TrangThaiGiaoHang", "TrangThaiThanhToan", "VoucherId" },
                values: new object[,]
                {
                    { new Guid("6964a07b-d7fd-4b98-b0f4-8e7786ca4e10"), "ly do huy1", "HD1", "mo ta2", new DateTime(2001, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 18385.0, 16998.0, 92928.0, 0, 1, null },
                    { new Guid("81bf5de4-d224-4276-a0c9-a3974250230f"), "ly do huy4", "HD4", "mo ta5", new DateTime(2005, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2004, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2006, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2009, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 99232.0, 45509.0, 110351.0, 1, 1, null },
                    { new Guid("82b05715-72c3-453b-bca1-a8ab9ad7149c"), "ly do huy5", "HD4", "mo ta5", new DateTime(2002, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2013, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 15939.0, 43005.0, 101257.0, 0, 1, null },
                    { new Guid("9a6d9240-b356-4c12-a134-acb283c5b0aa"), "ly do huy1", "HD4", "mo ta4", new DateTime(2014, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2008, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2013, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 26793.0, 45980.0, 132398.0, 1, 1, null },
                    { new Guid("aa012160-1ae4-4eb2-9e52-3bdf8ab73aaf"), "ly do huy3", "HD2", "mo ta5", new DateTime(2004, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2008, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2004, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 51365.0, 33592.0, 89465.0, 0, 0, null }
                });

            migrationBuilder.InsertData(
                table: "ThongKe",
                columns: new[] { "Id", "ChiTietSanPhamId", "HoaDonId", "Nam", "Ngay", "SanPhamChiTietId", "Thang" },
                values: new object[,]
                {
                    { new Guid("0acf1885-5217-4f8f-8c25-bbb7a4b8675d"), null, new Guid("00000000-0000-0000-0000-000000000000"), 2011, 22, new Guid("00000000-0000-0000-0000-000000000000"), 2 },
                    { new Guid("35663684-f3a6-421d-9e5d-3a91728bd63e"), null, new Guid("00000000-0000-0000-0000-000000000000"), 2005, 9, new Guid("00000000-0000-0000-0000-000000000000"), 11 },
                    { new Guid("4e497702-5b45-49e5-95b3-9800b7ad283b"), null, new Guid("00000000-0000-0000-0000-000000000000"), 2017, 27, new Guid("00000000-0000-0000-0000-000000000000"), 4 },
                    { new Guid("54e7eb28-0af9-45c5-8d77-2033cf1f5099"), null, new Guid("00000000-0000-0000-0000-000000000000"), 2017, 1, new Guid("00000000-0000-0000-0000-000000000000"), 12 },
                    { new Guid("9084ee76-6477-48de-8dd3-9160ddac97fc"), null, new Guid("00000000-0000-0000-0000-000000000000"), 2017, 22, new Guid("00000000-0000-0000-0000-000000000000"), 10 }
                });
        }
    }
}
