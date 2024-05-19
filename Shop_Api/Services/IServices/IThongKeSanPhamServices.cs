using Microsoft.AspNetCore.Mvc;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Services.IServices
{
    public interface IThongKeSanPhamServices
    {
        public Task<List<SanPhamChiTietDto>> FilterProductsTheoLoai(string maLoai);
        public Task<List<SanPhamChiTietDto>> FilterProductsTheoThuongHieu(string maThuonghieu);
        public Task<List<SanPhamChiTietDto>> FilterProductsTheoXuatXu(string maXuatxu);
        public Task<List<SanPhamChiTietDto>> FilterProductsTheoMauSac(string maMausac);
        public Task<List<SanPhamChiTietDto>> FilterProductsTheoChatLieu(string maChatlieu);
    }
}