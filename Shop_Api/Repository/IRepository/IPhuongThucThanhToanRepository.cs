using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IPhuongThucThanhToanRepository
    {
        public Task<ResponseDto> CreateAsync(PhuongThucThanhToan PTTT);
        public Task<List<PhuongThucThanhToan>> GetAllAsync();
        public Task<PhuongThucThanhToan> GetByIdAsync(Guid id);
        public Task<ResponseDto> UpdateAsync(Guid id, PhuongThucThanhToan PTTT);
        public Task<ResponseDto> DeleteAsync(Guid id);
    }
}
