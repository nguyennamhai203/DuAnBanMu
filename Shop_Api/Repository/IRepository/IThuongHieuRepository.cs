using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IThuongHieuRepository
    {
        public Task<ResponseDto> CreateAsync(ThuongHieu TH);
        public Task<List<ThuongHieu>> GetAllAsync();
        public Task<ThuongHieu> GetByIdAsync(Guid id);
        public Task<ResponseDto> UpdateAsync(Guid id, ThuongHieu TH);
        public Task<ResponseDto> DeleteAsync(Guid id);
    }
}