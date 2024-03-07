using Shop_Models.Entities;
using Shop_Models.Dto;


namespace Shop_Api.Repository.IRepository
{
    public interface IAnhRepository

    {
        public List<Anh> GetAll();
        public Task<ResponseDto> CreateAnh(Anh anh);
        public Task<ResponseDto> UpdateAnh(Guid id, Anh anh);
        public Task<Anh> GetAnhById(Guid id);
        public Task<ResponseDto> DeleteAnh(Guid id);
    }
}
