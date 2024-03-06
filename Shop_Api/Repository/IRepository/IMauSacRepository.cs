using Shop_Models.Entities;
using Shop_Models.Dto;


namespace Shop_Api.Repository.IRepository
{
    public interface IMauSacRepository

    {
        public List<MauSac> GetAll();
        public Task<ResponseDto> CreateMS(MauSac a);
        public Task<ResponseDto> UpdateMS(Guid id, MauSac anh);
        public Task<MauSac> GetById(Guid id);
        public Task<ResponseDto> DeleteMS(Guid id);
    }
}
