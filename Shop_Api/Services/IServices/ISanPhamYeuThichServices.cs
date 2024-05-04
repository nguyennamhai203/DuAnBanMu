using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Services.IServices
{
    public interface ISanPhamYeuThichServices
    {
        /*1.Tạo trang sản phẩm yêu thích:
        Tạo một trang riêng để hiển thị danh sách các sản phẩm mà người dùng đã yêu thích.
        Trang này có thể được truy cập từ thanh điều hướng hoặc từ biểu tượng trái tim.*/
        public Task<List<SanPhamYeuThich>> GetFavoriteProductsForUser(Guid userId);
        /*2.Thêm hoặc xóa sản phẩm từ trang sản phẩm:
        Trên trang chi tiết sản phẩm, sử dụng một biểu tượng trái tim để thể hiện trạng thái yêu thích của sản phẩm(yêu thích hoặc không yêu thích).*/
        public Task<ResponseDto> AddToFavorite(Guid userId, Guid productId);
        public Task<ResponseDto> RemoveFromFavorite(Guid userId, Guid productId);
        /*3.Khi người dùng nhấp vào biểu tượng trái tim:
        Nếu sản phẩm đã được thêm vào danh sách yêu thích, xóa nó khỏi danh sách.
        Nếu sản phẩm chưa được thêm vào danh sách yêu thích, thêm nó vào danh sách.*/
        public Task<ResponseDto> ToggleFavoriteStatus(Guid userId, Guid productId);
        /*4.Cập nhật giao diện khi thêm hoặc xóa sản phẩm:
        Khi người dùng thêm hoặc xóa sản phẩm khỏi danh sách yêu thích, cập nhật giao diện ngay lập tức để phản ánh thay đổi.
        Nếu sản phẩm đã được thêm vào danh sách yêu thích, biểu tượng trái tim sẽ thay đổi màu sắc hoặc hình ảnh để chỉ ra rằng sản phẩm đã được yêu thích.*/
        public Task<ResponseDto> UpdateInterfaceOnFavoriteChange(Guid userId, Guid productId);
        /*5.Chuyển hướng khi nhấp vào biểu tượng trái tim:
        Khi người dùng nhấp vào biểu tượng trái tim, chuyển hướng người dùng đến trang sản phẩm yêu thích.
        Trang này sẽ hiển thị danh sách các sản phẩm mà người dùng đã thêm vào danh sách yêu thích.*/
        public Task<ResponseDto> RedirectToFavoritePage();
        /*6.Thông báo và xác nhận hành động:
        Hiển thị thông báo hoặc xác nhận khi người dùng thêm hoặc xóa sản phẩm khỏi danh sách yêu thích.
        Điều này giúp người dùng có thể xác nhận hành động của họ và tránh các hành động không mong muốn.*/
        public Task<ResponseDto> ShowNotification(string message);
        /*7.Lưu trữ thông tin về sản phẩm yêu thích:
        Lưu trữ thông tin về sản phẩm yêu thích của mỗi người dùng để có thể hiển thị danh sách này mỗi khi họ truy cập vào trang sản phẩm yêu thích.*/
        public Task<ResponseDto> StoreFavoriteProductInformation(Guid userId, List<SanPhamYeuThich> favoriteProducts);
    }
}