using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Api.Services.IServices;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;
using System.Reflection.Metadata.Ecma335;

namespace Shop_Api.Services
{
    public class ThongKeSanPhamServices : IThongKeSanPhamServices
    {
        private readonly ApplicationDbContext _context;
        public ThongKeSanPhamServices(ApplicationDbContext context)
        {
            _context = context;
        }
        // hàm lấy top 5 sản phẩm bán chạy
        public async Task<List<SanPhamChiTietDto>> GetTop5Products()
        {
            var list = _context.ChiTietSanPhams.AsQueryable();
            var result = list.Select(sp => new SanPhamChiTietDto
            {
                Id = sp.Id,
                MaSanPhamChiTiet = sp.MaSanPham,
                GiaBan = sp.GiaBan,
                GiaNhap = sp.GiaNhap,
                GiaThucTe = sp.GiaThucTe,
                SoLuongTon = sp.SoLuongTon,
                SoLuongDaBan = sp.SoLuongDaBan,


                MaSanPham = sp.MaSanPham,
                TenSanPham = sp.SanPham.TenSanPham,

                MaLoai = sp.Loai.MaLoai,
                TenLoai = sp.Loai.TenLoai,

                MaThuongHieu = sp.ThuongHieu.MaThuongHieu,
                TenThuongHieu = sp.ThuongHieu.TenThuongHieu,

                MaXuatXu = sp.XuatXu.MaXuatXu,
                TenXuatXu = sp.XuatXu.TenXuatXu,

                MaMauSac = sp.MauSac.MaMauSac,
                TenMauSac = sp.MauSac.TenMauSac,

                MaChatLieu = sp.ChatLieu.MaChatLieu,
                TenChatLieu = sp.ChatLieu.TenChatLieu,

                TrangThai = sp.TrangThai,
                TrangThaiKhuyenMai = sp.TrangThaiKhuyenMai,

                SanPhamId = sp.SanPhamId,
                LoaiId = sp.LoaiId,
                ThuongHieuId = sp.ThuongHieuId,
                XuatXuId = sp.XuatXuId,
                MauSacId = sp.MauSacId,
                ChatLieuId = sp.ChatLieuId,

            }).Take(5);
            return result.Where(x => x.SoLuongDaBan > 0).ToList();
        }
    }
}