using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class fix_spcthoadon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_Voucher_VouchersGuid",
                table: "HoaDon");

            migrationBuilder.RenameColumn(
                name: "VouchersGuid",
                table: "HoaDon",
                newName: "VoucherId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDon_VouchersGuid",
                table: "HoaDon",
                newName: "IX_HoaDon_VoucherId");

            migrationBuilder.AddColumn<string>(
                name: "Mota",
                table: "ChiTietSanPham",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiKhuyenMai",
                table: "ChiTietSanPham",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_Voucher_VoucherId",
                table: "HoaDon",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_Voucher_VoucherId",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "Mota",
                table: "ChiTietSanPham");

            migrationBuilder.DropColumn(
                name: "TrangThaiKhuyenMai",
                table: "ChiTietSanPham");

            migrationBuilder.RenameColumn(
                name: "VoucherId",
                table: "HoaDon",
                newName: "VouchersGuid");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDon_VoucherId",
                table: "HoaDon",
                newName: "IX_HoaDon_VouchersGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_Voucher_VouchersGuid",
                table: "HoaDon",
                column: "VouchersGuid",
                principalTable: "Voucher",
                principalColumn: "Guid");
        }
    }
}
