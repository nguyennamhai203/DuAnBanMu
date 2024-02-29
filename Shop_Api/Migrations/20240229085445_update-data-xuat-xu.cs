using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class updatedataxuatxu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "XuatXu",
                columns: new[] { "Guid", "MaXuatXu", "TenXuatXu", "TrangThai" },
                values: new object[] { new Guid("235674e2-f8b9-4b8a-8c74-42553912bbc8"), "XX01", "VietNam", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "XuatXu",
                keyColumn: "Guid",
                keyValue: new Guid("235674e2-f8b9-4b8a-8c74-42553912bbc8"));
        }
    }
}
