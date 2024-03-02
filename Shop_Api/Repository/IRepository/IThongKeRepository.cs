using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IThongKeRepository
    {
        public Task<ResponseDto> CreateAsync(ThongKe model);
        public Task<ResponseDto> UpdateAsync(ThongKe model);
        public Task<ResponseDto> DeleteAsync(Guid Id);
        public Task<List<ThongKe>> GetAsync();
        public Task<List<ThongKe>> GetAsync(int? status, int page = 1);
    }
}
