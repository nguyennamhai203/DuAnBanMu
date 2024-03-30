using Shop_Models.Dto;

namespace Shop_Api.Services.IServices
{
    public interface IGioHangChiTietServices
    {
        Task<ResponseDto> AddCart(string userName, string codeProductDetail);// Phương thức để người dùng tạo giỏ hàng
        Task<ResponseDto> CongQuantityCartDetail(Guid idCartDetail);
        Task<ResponseDto> TruQuantityCartDetail(Guid idCartDetail);
        Task<ResponseDto> GetAllCarts();// Cho admin quản lý
        Task<ResponseDto> GetCartById(Guid id);// Cho admin quản lý
        Task<ResponseDto> GetCartJoinForUser(string username);// Hiển thị giỏ hàng cho người dùng
    }
}
