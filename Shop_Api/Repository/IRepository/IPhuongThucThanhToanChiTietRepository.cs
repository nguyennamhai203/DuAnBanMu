using Shop_Models.Entities;
using Shop_Models.Dto;


namespace Shop_Api.Repository.IRepository
{
    public interface IPhuongThucThanhToanChiTietRepository

    {
        public List<PhuongThucTTChiTiet> GetAll();
        public Task<ResponseDto> Create(PhuongThucTTChiTiet a);
        public Task<ResponseDto> Update(Guid id, PhuongThucTTChiTiet a);
        public Task<PhuongThucTTChiTiet> GetById(Guid id);
        public Task<ResponseDto> Delete(Guid id);
        
        
    }
}
