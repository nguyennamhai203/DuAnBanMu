using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IChiTietKhuyenMaiRepository
    {
        public Task<ResponseDto> CreateAsync(ChiTietKhuyenMai model);
        public Task<ResponseDto> UpdateAsync(ChiTietKhuyenMai model);
        public Task<ResponseDto> DeleteAsync(Guid Id);
        public Task<List<ChiTietKhuyenMai>> GetAsync();
        public Task<List<ChiTietKhuyenMai>> GetAsync(int? status, int page = 1);
    }
}
