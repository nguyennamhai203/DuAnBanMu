using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface ISanPhamYeuThichRepository
    {
        public Task<ResponseDto> CreateSPYT(SanPhamYeuThich add);
        public Task<ResponseDto> UpdateSPYT(SanPhamYeuThich update);
        public Task<ResponseDto> DeleteSPYT(Guid Id);
        public Task<List<SanPhamYeuThich>> GetSPYT();
        public Task<List<SanPhamYeuThich>> GetListSPYT(int? status, int page);
        public Task<ResponseDto> GetByIdSPYT(Guid id);
    }
}