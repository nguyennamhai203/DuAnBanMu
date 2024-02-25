using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IKhuyenMaiRepository
    {
        public Task<ResponseDto> GetAsync(int? status, int page = 1);
        Task<ResponseDto> CreateAsync(Khuyenmai obj);
        Task<ResponseDto> UpdateAsync(Khuyenmai obj);
        public Task<ResponseDto> DeleteAsync(Guid Id);
        Task<ResponseDto> GetByIdAsync(Guid Id);

    }
}
