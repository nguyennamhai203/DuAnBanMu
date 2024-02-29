using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IGioHangRepository
    {
        public Task<ResponseDto> CreateGioHang(GioHang add);
        public Task<ResponseDto> UpdateGioHang(GioHang update);
        public Task<ResponseDto> DeleteGioHang(Guid Id);
        public Task<List<GioHang>> GetGioHang();
        public Task<List<GioHang>> GetListGioHang(int? status, int page = 1);
        public Task<ResponseDto> GetByIdGioHang(Guid id);
    }
}