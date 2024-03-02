using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface ILoaiRepository
    {
        public Task<Loai> GetByIdAsync(Guid id);
        public Task<List<Loai>> GetAllAsync();
        public Task<ResponseDto> CreateAsync(Loai loai);
        public Task<ResponseDto> UpdateAsync(Guid id, Loai loai);
        public Task<ResponseDto> DeleteAsync(Guid id);
    }
}
