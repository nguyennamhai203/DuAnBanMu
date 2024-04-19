using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop_Api.Migrations
{
    public partial class gendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "SanPhamId",
                table: "ThongKe",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiThongKe",
                table: "ThongKe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ChiTietSanPhamId",
                table: "HoaDon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HoaDonId",
                table: "ChiTietSanPham",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.InsertData(
                table: "ChiTietSanPham",
                columns: new[] { "Id", "ChatLieuId", "GiaBan", "GiaNhap", "GiaThucTe", "HoaDonId", "LoaiId", "MaSanPham", "MauSacId", "Mota", "SanPhamId", "SoLuongDaBan", "SoLuongTon", "ThuongHieuId", "TrangThai", "TrangThaiKhuyenMai", "XuatXuId" },
                values: new object[,]
                {
                    { new Guid("4126787d-89a6-495f-87ef-762984a4ca49"), null, 242580.0, 651874.0, 799372.0, null, null, "SP4", null, "mo ta1", null, 7, 9, null, 1, 0, null },
                    { new Guid("510c4c6e-249c-4b5a-8b2c-b11ad13d45ad"), null, 694273.0, 158697.0, 562969.0, null, null, "SP5", null, "mo ta2", null, 5, 6, null, 1, 0, null },
                    { new Guid("8e933d5d-729d-47a1-b81b-725fd51e530a"), null, 907371.0, 428874.0, 645059.0, null, null, "SP2", null, "mo ta2", null, 4, 3, null, 1, 0, null },
                    { new Guid("aa8dbf73-f1fb-4dd6-b135-0d57080528b7"), null, 269761.0, 490539.0, 272096.0, null, null, "SP5", null, "mo ta4", null, 9, 6, null, 1, 0, null },
                    { new Guid("ff1cc6ec-6c0f-4127-9bce-82200de57617"), null, 543091.0, 595937.0, 612723.0, null, null, "SP1", null, "mo ta2", null, 7, 2, null, 1, 0, null }
                });

            migrationBuilder.InsertData(
                table: "HoaDon",
                columns: new[] { "Id", "ChiTietSanPhamId", "LiDoHuy", "MaHoaDon", "MoTa", "NgayGiaoDuKien", "NgayNhan", "NgayShip", "NgayTao", "NgayThanhToan", "NguoiDungId", "TienGiam", "TienShip", "TongTien", "TrangThaiGiaoHang", "TrangThaiThanhToan", "VoucherId" },
                values: new object[,]
                {
                    { new Guid("00c231bb-ded6-4647-9170-e13e959dedd5"), null, "ly do huy4", "HD1", "mo ta3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 45257.0, 16436.0, 115677.0, 1, 0, null },
                    { new Guid("3be3979e-273e-4543-8323-038a290d8e94"), null, "ly do huy4", "HD3", "mo ta3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 57985.0, 18603.0, 45532.0, 1, 0, null },
                    { new Guid("62f6da71-394d-48d0-bc9a-6b468f2a276c"), null, "ly do huy5", "HD4", "mo ta3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 58525.0, 25319.0, 65762.0, 1, 0, null },
                    { new Guid("70edc1d4-d05b-4fd7-8c12-fab44ebb7f09"), null, "ly do huy1", "HD3", "mo ta1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 30471.0, 13133.0, 74488.0, 0, 1, null },
                    { new Guid("93fe2dc1-cc2d-4fab-bd05-6d9bb2490f50"), null, "ly do huy4", "HD2", "mo ta3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 15703.0, 23348.0, 71128.0, 0, 1, null }
                });

            migrationBuilder.InsertData(
                table: "SanPham",
                columns: new[] { "IdSanPham", "MaSanPham", "TenSanPham", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("1e49bf60-5274-474c-824f-3140565f828d"), "SP3", "TSP3", 0 },
                    { new Guid("2841ee03-a643-457c-b94a-532fc5d78794"), "SP1", "TSP5", 0 },
                    { new Guid("66db1341-7c80-4e8c-8ace-ab8f28074936"), "SP2", "TSP2", 1 },
                    { new Guid("f1ee8654-6202-4055-b208-ed68e546b130"), "SP4", "TSP1", 0 },
                    { new Guid("fc46b0b1-2e2f-4c11-9b10-2330766e25c2"), "SP4", "TSP5", 1 }
                });

            migrationBuilder.InsertData(
                table: "ThongKe",
                columns: new[] { "Id", "ChiTietSanPhamId", "HoaDonId", "Nam", "Ngay", "SanPhamChiTietId", "SanPhamId", "Thang", "TrangThaiThongKe" },
                values: new object[,]
                {
                    { new Guid("06f09a9b-caee-422a-825d-76d177cad6b2"), null, new Guid("00c231bb-ded6-4647-9170-e13e959dedd5"), 2012, 26, new Guid("d3f35d08-35d8-4e7a-8569-5b5813deb945"), new Guid("1e49bf60-5274-474c-824f-3140565f828d"), 5, 0 },
                    { new Guid("4db86003-a2e7-4843-973f-6ac4ea97bcab"), null, new Guid("3be3979e-273e-4543-8323-038a290d8e94"), 2012, 12, new Guid("5e44483a-8247-4c72-aa0c-8dc1c0c81f11"), new Guid("2841ee03-a643-457c-b94a-532fc5d78794"), 8, 0 },
                    { new Guid("8c4d8d5c-9e25-4051-a578-448ea65b7a4e"), null, new Guid("62f6da71-394d-48d0-bc9a-6b468f2a276c"), 2008, 5, new Guid("673f6626-b225-460e-89f5-0e24332956ae"), new Guid("66db1341-7c80-4e8c-8ace-ab8f28074936"), 1, 1 },
                    { new Guid("9af03ca2-5196-4841-827a-3e5e6b51b449"), null, new Guid("70edc1d4-d05b-4fd7-8c12-fab44ebb7f09"), 2013, 26, new Guid("c7b4903f-ab0b-406b-a25c-033de1b45b4f"), new Guid("f1ee8654-6202-4055-b208-ed68e546b130"), 9, 1 },
                    { new Guid("e7d40ca1-1d92-4a51-bbc9-5733002d44c0"), null, new Guid("93fe2dc1-cc2d-4fab-bd05-6d9bb2490f50"), 2004, 6, new Guid("ec847c81-05c1-4baf-9b62-eb9e444ffa65"), new Guid("fc46b0b1-2e2f-4c11-9b10-2330766e25c2"), 4, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThongKe_SanPhamId",
                table: "ThongKe",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_ChiTietSanPhamId",
                table: "HoaDon",
                column: "ChiTietSanPhamId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_ChiTietSanPham_ChiTietSanPhamId",
                table: "HoaDon",
                column: "ChiTietSanPhamId",
                principalTable: "ChiTietSanPham",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThongKe_SanPham_SanPhamId",
                table: "ThongKe",
                column: "SanPhamId",
                principalTable: "SanPham",
                principalColumn: "IdSanPham",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_ChiTietSanPham_ChiTietSanPhamId",
                table: "HoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongKe_SanPham_SanPhamId",
                table: "ThongKe");

            migrationBuilder.DropTable(
                name: "ThongKeViewModels");

            migrationBuilder.DropIndex(
                name: "IX_ThongKe_SanPhamId",
                table: "ThongKe");

            migrationBuilder.DropIndex(
                name: "IX_HoaDon_ChiTietSanPhamId",
                table: "HoaDon");

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("4126787d-89a6-495f-87ef-762984a4ca49"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("510c4c6e-249c-4b5a-8b2c-b11ad13d45ad"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("8e933d5d-729d-47a1-b81b-725fd51e530a"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("aa8dbf73-f1fb-4dd6-b135-0d57080528b7"));

            migrationBuilder.DeleteData(
                table: "ChiTietSanPham",
                keyColumn: "Id",
                keyValue: new Guid("ff1cc6ec-6c0f-4127-9bce-82200de57617"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("00c231bb-ded6-4647-9170-e13e959dedd5"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("3be3979e-273e-4543-8323-038a290d8e94"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("62f6da71-394d-48d0-bc9a-6b468f2a276c"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("70edc1d4-d05b-4fd7-8c12-fab44ebb7f09"));

            migrationBuilder.DeleteData(
                table: "HoaDon",
                keyColumn: "Id",
                keyValue: new Guid("93fe2dc1-cc2d-4fab-bd05-6d9bb2490f50"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("1e49bf60-5274-474c-824f-3140565f828d"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("2841ee03-a643-457c-b94a-532fc5d78794"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("66db1341-7c80-4e8c-8ace-ab8f28074936"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("f1ee8654-6202-4055-b208-ed68e546b130"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "IdSanPham",
                keyValue: new Guid("fc46b0b1-2e2f-4c11-9b10-2330766e25c2"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("06f09a9b-caee-422a-825d-76d177cad6b2"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("4db86003-a2e7-4843-973f-6ac4ea97bcab"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("8c4d8d5c-9e25-4051-a578-448ea65b7a4e"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("9af03ca2-5196-4841-827a-3e5e6b51b449"));

            migrationBuilder.DeleteData(
                table: "ThongKe",
                keyColumn: "Id",
                keyValue: new Guid("e7d40ca1-1d92-4a51-bbc9-5733002d44c0"));

            migrationBuilder.DropColumn(
                name: "SanPhamId",
                table: "ThongKe");

            migrationBuilder.DropColumn(
                name: "TrangThaiThongKe",
                table: "ThongKe");

            migrationBuilder.DropColumn(
                name: "ChiTietSanPhamId",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "HoaDonId",
                table: "ChiTietSanPham");

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
    }
}
