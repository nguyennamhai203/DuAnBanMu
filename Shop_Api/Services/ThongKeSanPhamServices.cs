using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Api.Services.IServices;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;

namespace Shop_Api.Services
{
    public class ThongKeSanPhamServices : IThongKeSanPhamServices
    {
        private readonly ApplicationDbContext _context;
        public ThongKeSanPhamServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SanPhamChiTietDto>> FilterProductsTheoChatLieu(string maChatlieu)
        {
            var query = _context.ChiTietSanPhams
                //.Where(sp => sp.ChatLieu.Guid == maChatlieu)
                .Select(sp => new SanPhamChiTietDto
                {
                    Id = sp.Id,
                    MaSanPhamChiTiet = sp.MaSanPham,
                    TenSanPhamChiTiet = sp.SanPham.TenSanPham,
                    GiaBan = sp.GiaBan,
                    GiaNhap = sp.GiaNhap,
                    GiaThucTe = sp.GiaThucTe,
                    SoLuongTon = sp.SoLuongTon,
                    SoLuongDaBan = sp.SoLuongDaBan,
                    TrangThai = sp.TrangThai,
                    TrangThaiKhuyenMai = sp.TrangThaiKhuyenMai,
                    MaChatLieu = sp.ChatLieu.MaChatLieu,
                    TenChatLieu = sp.ChatLieu.TenChatLieu
                });
            //return query.ToListAsync();
            return null;
        }

        public async Task<List<SanPhamChiTietDto>> FilterProductsTheoLoai(string maLoai)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SanPhamChiTietDto>> FilterProductsTheoMauSac(string maMausac)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SanPhamChiTietDto>> FilterProductsTheoThuongHieu(string maThuonghieu)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SanPhamChiTietDto>> FilterProductsTheoXuatXu(string maXuatxu)
        {
            throw new NotImplementedException();
        }

    }
}