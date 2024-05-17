using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Services.IServices;
using Shop_Api.Services.Models;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Services
{
    public class SanPhamYeuThichServices : ISanPhamYeuThichServices
    {
        public readonly ApplicationDbContext context;
        public SanPhamYeuThichServices(ApplicationDbContext ct)
        {
            context = ct;
        }

        // Thêm 1 sản phẩm vào danh sách yêu thích của người dùng
        public async Task<ResponseDto> AddToFavoriteBus(Guid userId, Guid productId)
        {
            try
            {
                // Lấy thông tin sản phẩm chi tiết từ cơ sở dữ liệu
                var productDetail = await context.ChiTietSanPhams.FirstOrDefaultAsync(x => x.Id == productId);
                if (productDetail == null)
                {
                    return new ResponseDto { Code = 404, Message = "Product not found." };
                }
                var checkproducinyeuthichlist = await context.SanPhamYeuThichs.AnyAsync(x => x.ChiTietSanPhamId == productId && x.NguoiDungId == userId);
                if (checkproducinyeuthichlist)
                {
                    return new ResponseDto { Code = 400, Message = "Sản phẩm đã tồn tại trong list sản phẩm yêu thích của bạn" };
                }
                // Tạo một đối tượng SanPhamYeuThich để thêm vào danh sách yêu thích
                var favorite = new SanPhamYeuThich
                {
                    Id = Guid.NewGuid(), // Tạo một ID mới
                    ChiTietSanPhamId = productId,
                    NguoiDungId = userId,
                    TrangThai = 1
                };

                // Thêm vào danh sách yêu thích và lưu vào cơ sở dữ liệu
                await context.SanPhamYeuThichs.AddAsync(favorite);
                await context.SaveChangesAsync();
                return new ResponseDto { Code = 200, Message = "Product added to favorites successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Code = 500, Message = $"Error adding product to favorites: {ex.Message}" };
            }
        }

        public async Task<ResponseDto> RemoveFromFavorite(Guid userId, Guid productId)
        {
            try
            {
                var favorite = await context.SanPhamYeuThichs.FirstOrDefaultAsync(x => x.NguoiDungId == userId && x.ChiTietSanPhamId == productId);
                if (favorite != null)
                {
                    context.SanPhamYeuThichs.Remove(favorite);
                    await context.SaveChangesAsync();
                    return new ResponseDto { Code = 200, Message = "Product removed from favorites successfully." };
                }
                else
                {
                    return new ResponseDto { Code = 404, Message = "Product not found in favorites." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Code = 500, Message = $"Error removing product from favorites: {ex.Message}" };
            }
        }

        public async Task<List<SanPhamYeuThichViewModel>> GetFavoriteProductsForUser(Guid userId)
        {
            try
            {
                var getforusers = (
                    from spyt in context.SanPhamYeuThichs.AsQueryable().Where(a => a.NguoiDungId == userId)
                    join ctsp in context.ChiTietSanPhams.AsQueryable() on spyt.ChiTietSanPhamId equals ctsp.Id
                    join anh in context.Anhs.AsQueryable() on ctsp.Id equals anh.ChiTietSanPhamId
                    where spyt.NguoiDungId == userId && anh.MaAnh.Equals(anh.MaAnh)
                    select new SanPhamYeuThichViewModel
                    {
                        Id = spyt.Id,
                        ChiTietSanPhamId = spyt.ChiTietSanPhamId,
                        AnhSanPham = anh.URL, // Giả sử URL là thuộc tính chứa đường dẫn đến ảnh
                        MaSanPham = spyt.ChiTietSanPham.MaSanPham,
                        GiaNhap = ctsp.GiaBan,  // giá bán khi giảm giá 
                        GiaBan = ctsp.GiaThucTe,// giá bán khi chưa giảm giá (nếu đc giảm giá sẽ bằng giá thực tế sau giảm giá)
                        TrangThaiKhuyenMai = spyt.ChiTietSanPham.TrangThaiKhuyenMai

                    }).AsEnumerable();
                return getforusers.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}