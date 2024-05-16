using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Services.IServices
{
    public interface ISanPhamYeuThichServices
    {
        public Task<List<SanPhamYeuThichViewModel>> GetFavoriteProductsForUser(Guid userId);
        public Task<ResponseDto> AddToFavoriteBus(Guid userId, Guid productId);
        public Task<ResponseDto> RemoveFromFavorite(Guid userId, Guid productId);
    }
}