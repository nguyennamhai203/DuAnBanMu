using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IChatLieuRepository
    {
        public Task<ResponseDto> CreateAsync(ChatLieu model);
        public Task<ResponseDto> UpdateAsync(ChatLieu model);
        public Task<ResponseDto> DeleteAsync(Guid Id);
        public Task<List<ChatLieu>> GetAsync();
        public Task<List<ChatLieu>> GetAsync(int? status, int page = 1);
    }
}
