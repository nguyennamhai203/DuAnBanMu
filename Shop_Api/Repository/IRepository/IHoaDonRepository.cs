using Shop_Models.Entities;
using Shop_Models.Dto;


namespace Shop_Api.Repository.IRepository
{
    public interface IHoaDonRepository

    {
        public List<HoaDon> GetAll();
        public Task<ResponseDto> CreateHD(HoaDon a);
        public Task<ResponseDto> UpdateHD(Guid id, HoaDon a);
        public Task<HoaDon> GetById(Guid id);
        
    }
}
