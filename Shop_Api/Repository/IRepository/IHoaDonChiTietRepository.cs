using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IHoaDonChiTietRepository
    {
        public Task<ResponseDto> CreateAsync(HoaDonChiTiet HDCT);
        public Task<List<HoaDonChiTiet>> GetAllAsync();
        public Task<HoaDonChiTiet> GetByIdAsync(Guid id);
        public Task<ResponseDto> UpdateAsync(Guid id, HoaDonChiTiet HDCT);
        public Task<ResponseDto> DeleteAsync(Guid id);
        public Task<List<HoaDonChiTiet>> GetAllById(Guid id);
    }
}
