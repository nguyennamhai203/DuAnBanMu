using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IGioHangChiTietRepository
    {
        Task<bool> CreateAsync(GioHangChiTiet obj);
        Task<bool> Updatesync(GioHangChiTiet obj);
        Task<bool> Deletesync(Guid id);
        Task<GioHangChiTiet> GetById(Guid id);
        Task<IEnumerable<GioHangChiTiet>> GetAll();
        public Task<ResponseDto> GetCartJoinForUser(string username);
        public Task<IEnumerable<GioHangChiTietViewModel>> GetCartDetailByUserName(string username);
        public Task<GioHangChiTiet> TimGioHangChiTIet(string username,string codeproduct);

    }
}
