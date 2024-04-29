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
        public readonly ApplicationDbContext contextSPYT;
        public SanPhamYeuThichServices(ApplicationDbContext ct)
        {
            contextSPYT = ct;
        }

        public async Task<List<SanPhamYeuThich>> GetFavoriteProductsForUser(Guid userId)
        {
            try
            {
                return await contextSPYT.SanPhamYeuThichs.Where(x => x.NguoiDungId == userId).ToListAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<ResponseDto> AddToFavorite(Guid userId, Guid productId)
        {
            try
            {
                var favorite = new SanPhamYeuThich
                {
                    NguoiDungId = userId,
                    ChiTietSanPhamId = productId,
                    TrangThai = 1 // Set TrangThai to indicate the product is favorited
                };
                await contextSPYT.SanPhamYeuThichs.AddAsync(favorite);
                await contextSPYT.SaveChangesAsync();
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
                var favorite = await contextSPYT.SanPhamYeuThichs.FirstOrDefaultAsync(x => x.NguoiDungId == userId && x.ChiTietSanPhamId == productId);
                if (favorite != null)
                {
                    contextSPYT.SanPhamYeuThichs.Remove(favorite);
                    await contextSPYT.SaveChangesAsync();
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

        public async Task<ResponseDto> ToggleFavoriteStatus(Guid userId, Guid productId)
        {
            try
            {
                var favorite = await contextSPYT.SanPhamYeuThichs.FirstOrDefaultAsync(x => x.NguoiDungId == userId && x.ChiTietSanPhamId == productId);
                if (favorite == null)
                {
                    return await AddToFavorite(userId, productId);
                }
                else
                {
                    return await RemoveFromFavorite(userId, productId);
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Code = 500, Message = $"Error toggling favorite status: {ex.Message}" };
            }
        }

        public async Task<ResponseDto> UpdateInterfaceOnFavoriteChange(Guid userId, Guid productId)
        {
            try
            {
                return new ResponseDto
                {
                    Code = 200,
                    Message = "Thanh cong"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    Code = 500,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDto> RedirectToFavoritePage()
        {
            try
            {
                return new ResponseDto { Code = 200, Message = "Redirected to favorite products page successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    Code = 500,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDto> ShowNotification(string message)
        {
            try
            {
                return new ResponseDto { Code = 200, Message = "Notification shown successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    Code = 500,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDto> StoreFavoriteProductInformation(Guid userId, List<SanPhamYeuThich> favoriteProducts)
        {
            try
            {
                var favoriteProductService = new FavoriteProductService();
                favoriteProductService.SaveFavoriteProducts(userId, favoriteProducts);
                return new ResponseDto { Code = 200, Message = "Favorite product information stored successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    Code = 500,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}