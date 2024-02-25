using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository.IRepository
{
    public interface IGioHangChiTietRepository
    {
        Task<bool> CreateAsync(GioHangChiTiet obj);
        Task<bool> Updatesync(GioHangChiTiet obj);
        Task<bool> Deletesync(Guid id);
        Task<GioHangChiTiet> GetById(Guid id);
        Task<IEnumerable<GioHangChiTiet>> GetAll();
        public Task<ResponseDto> GetCartJoinForUser(string username);

    }
}
