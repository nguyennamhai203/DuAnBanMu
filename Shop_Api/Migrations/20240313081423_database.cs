﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Voucher",
                keyColumn: "Guid",
                keyValue: new Guid("f27e6385-975b-4bfb-9b8f-e6bc6bba0fee"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Voucher",
                columns: new[] { "Guid", "MaVoucher", "NgayBatDau", "NgayHetHan", "PhanTramGiam", "SoLuong", "TenVoucher", "TrangThai" },
                values: new object[] { new Guid("f27e6385-975b-4bfb-9b8f-e6bc6bba0fee"), "test", new DateTime(2000, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1, "test", 1 });
        }
    }
}
