using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IChiTietSanPhamRepository
    {
        public Task<ResponseDto> CreateAsync(ChiTietSanPham model);
        public Task<ResponseDto> UpdateAsync(ChiTietSanPham model);
        public Task<ResponseDto> DeleteAsync(Guid Id);
        public Task<List<ChiTietSanPham>> GetAsync();
        public Task<List<ChiTietSanPham>> GetAsync(int? status, int page = 1);
    }
}
