using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface ISanPhamYeuThichRepository
    {
        public Task<ResponseDto> CreateSPYT(SanPhamYeuThich add);
        public Task<ResponseDto> UpdateSPYT(SanPhamYeuThich update);
        public Task<ResponseDto> DeleteSPYT(Guid Id);
        public Task<List<SanPhamYeuThich>> GetSPYT();
        public Task<List<SanPhamYeuThich>> GetListSPYT(int? status, int page);
        public Task<ResponseDto> GetByIdSPYT(Guid id);
        // Thêm một sản phẩm vào danh sách yêu thích của người dùng
        public Task<ResponseDto> AddToFavoriteAsync(Guid userId, Guid productId);
        // Xóa một sản phẩm khỏi danh sách yêu thích của người dùng
        public Task<ResponseDto> RemoveFromFavoriteAsync(Guid userId, Guid productId);
        // Kiểm tra xem một sản phẩm có trong danh sách yêu thích của người dùng hay không
        public Task<bool> IsInFavoriteAsync(Guid userId, Guid productId);
        // Lấy danh sách các sản phẩm yêu thích của người dùng
        public Task<List<ChiTietSanPham>> GetFavoriteProductsAsync(Guid userId);
        // Kiểm tra xem một sản phẩm có trong danh sách yêu thích hay không
        public Task<bool> IsFavoriteProductAsync(Guid productId);
        // Đếm số lượng sản phẩm yêu thích của người dùng
        public Task<int> CountFavoriteProductsAsync(Guid userId);
        // Xóa toàn bộ sản phẩm trong danh sách yêu thích của người dùng
        public Task<ResponseDto> ClearFavoriteProductsAsync(Guid userId);
        // Lấy trạng thái yêu thích của một sản phẩm
        public Task<FavoriteStatus> GetFavoriteStatusAsync(Guid userId, Guid productId);
    }
}