using Shop_Models.Dto;

namespace Shop_Api.Services.IServices
{
    public interface IThongKeSanPhamServices
    {
        public Task<List<SanPhamChiTietDto>> GetTop5Products();
    }
}