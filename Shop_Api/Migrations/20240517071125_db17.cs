using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class db17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHangChiTiet_GioHang_GioHangId",
                table: "GioHangChiTiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "IdGh",
                table: "GioHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang",
                column: "IdNguoiDung");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHangChiTiet_GioHang_GioHangId",
                table: "GioHangChiTiet",
                column: "GioHangId",
                principalTable: "GioHang",
                principalColumn: "IdNguoiDung",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHangChiTiet_GioHang_GioHangId",
                table: "GioHangChiTiet");

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

            migrationBuilder.AddForeignKey(
                name: "FK_GioHangChiTiet_GioHang_GioHangId",
                table: "GioHangChiTiet",
                column: "GioHangId",
                principalTable: "GioHang",
                principalColumn: "IdGh",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
