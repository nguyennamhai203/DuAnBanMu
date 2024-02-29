using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IXuatXuRepository
    {
        public Task<ResponseDto> CreateXX(XuatXu add);
        public Task<ResponseDto> UpdateXX(XuatXu update);
        public Task<ResponseDto> DeleteXX(Guid Id);
        public Task<List<XuatXu>> GetXuatXu();
        public Task<List<XuatXu>> GetListXuatXu(int? status, int page = 1);
        public Task<ResponseDto> GetByIdXuatXu(Guid id);
    }
}