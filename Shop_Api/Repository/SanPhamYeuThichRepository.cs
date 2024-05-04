using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;

namespace Shop_Api.Repository
{
    public class SanPhamYeuThichRepository : ISanPhamYeuThichRepository
    {
        private readonly ApplicationDbContext contextSPYT;
        public static int PAGE_SIZE { get; set; } = 1;
        public SanPhamYeuThichRepository(ApplicationDbContext context)
        {
            contextSPYT = context;
        }
        public async Task<ResponseDto> CreateSPYT(SanPhamYeuThich add)
        {
            try
            {
                await contextSPYT.SanPhamYeuThichs.AddAsync(add);
                await contextSPYT.SaveChangesAsync();
                return new ResponseDto
                {
                    Code = 200,
                    Message = "Them thanh cong"
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Code = 500,
                    Message = "Them loi roi"
                };
            }
        }

        public async Task<ResponseDto> DeleteSPYT(Guid Id)
        {
            var iddelete = await contextSPYT.SanPhamYeuThichs.FindAsync(Id);
            try
            {
                contextSPYT.SanPhamYeuThichs.Remove(iddelete);
                await contextSPYT.SaveChangesAsync();
                return new ResponseDto
                {
                    Code = 200,
                    Message = "Xoa thanh cong"
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Code = 500,
                    Message = "Xoa loi roi"
                };
            }
        }

        public async Task<ResponseDto> GetByIdSPYT(Guid id)
        {
            var getid = await contextSPYT.SanPhamYeuThichs.FindAsync(id);
            try
            {
                return new ResponseDto
                {
                    IsSuccess = true,
                    Content = getid,
                    Code = 200,
                    Message = "Da tim thay du lieu"
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "Khong thay du lieu"
                };
            }
        }

        public async Task<List<SanPhamYeuThich>> GetListSPYT(int? status, int page)
        {
            var list = contextSPYT.SanPhamYeuThichs.AsQueryable();
            if (status.HasValue)
            {
                list = list.Where(x => x.TrangThai == status);
            }
            var result = list.Select(x => new SanPhamYeuThich
            {
                Id = x.Id,
                NguoiDungId = x.NguoiDungId,
                ChiTietSanPhamId = x.ChiTietSanPhamId,
                TrangThai = x.TrangThai
            });
            return result.ToList();
        }

        public async Task<List<SanPhamYeuThich>> GetSPYT()
        {
            return await contextSPYT.SanPhamYeuThichs.ToListAsync();
        }

        public async Task<ResponseDto> UpdateSPYT(SanPhamYeuThich update)
        {
            var idupdate = await contextSPYT.SanPhamYeuThichs.FindAsync(update.Id);
            try
            {
                idupdate.NguoiDungId = update.NguoiDungId;
                idupdate.ChiTietSanPhamId = update.ChiTietSanPhamId;
                idupdate.TrangThai = update.TrangThai;
                contextSPYT.SanPhamYeuThichs.Update(update);
                await contextSPYT.SaveChangesAsync();
                return new ResponseDto
                {
                    Code = 200,
                    Message = "Cap nhat thanh cong"
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Code = 500,
                    Message = "Cap nhat loi roi"
                };
            }
        }

        // Thêm một sản phẩm vào danh sách yêu thích của người dùng
        public async Task<ResponseDto> AddToFavoriteAsync(Guid userId, Guid productId)
        {
            try
            {
                var existingFavorite = await contextSPYT.SanPhamYeuThichs
                    .FirstOrDefaultAsync(x => x.NguoiDungId == userId && x.ChiTietSanPhamId == productId);

                if (existingFavorite != null)
                {
                    return new ResponseDto { Code = 400, Message = "Sản phẩm đã tồn tại trong danh sách yêu thích." };
                }
                
                var newFavorite = new SanPhamYeuThich { NguoiDungId = userId, ChiTietSanPhamId = productId };
                await contextSPYT.SanPhamYeuThichs.AddAsync(newFavorite);
                await contextSPYT.SaveChangesAsync();

                return new ResponseDto { Code = 200, Message = "Thêm sản phẩm vào danh sách yêu thích thành công." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Code = 500, Message = $"Đã xảy ra lỗi khi thêm sản phẩm vào danh sách yêu thích: {ex.Message}" };
            }
        }

        // Xóa một sản phẩm khỏi danh sách yêu thích của người dùng
        public async Task<ResponseDto> RemoveFromFavoriteAsync(Guid userId, Guid productId)
        {
            try
            {
                var favorite = await contextSPYT.SanPhamYeuThichs
                    .FirstOrDefaultAsync(x => x.NguoiDungId == userId && x.ChiTietSanPhamId == productId);

                if (favorite == null)
                {
                    return new ResponseDto { Code = 404, Message = "Không tìm thấy sản phẩm trong danh sách yêu thích của người dùng." };
                }

                contextSPYT.SanPhamYeuThichs.Remove(favorite);
                await contextSPYT.SaveChangesAsync();

                return new ResponseDto { Code = 200, Message = "Xóa sản phẩm khỏi danh sách yêu thích thành công." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Code = 500, Message = $"Đã xảy ra lỗi khi xóa sản phẩm khỏi danh sách yêu thích: {ex.Message}" };
            }
        }
        
        // Kiểm tra xem một sản phẩm có trong danh sách yêu thích của người dùng hay không
        public async Task<bool> IsInFavoriteAsync(Guid userId, Guid productId)
        {
            return await contextSPYT.SanPhamYeuThichs.AnyAsync(x => x.NguoiDungId == userId && x.ChiTietSanPhamId == productId);
        }

        // Lấy danh sách các sản phẩm yêu thích của người dùng
        public async Task<List<ChiTietSanPham>> GetFavoriteProductsAsync(Guid userId)
        {
            var list = await contextSPYT.SanPhamYeuThichs.Where(x => x.NguoiDungId == userId).Select(x => x.ChiTietSanPham).ToListAsync();
            return list;
        }

        // Kiểm tra xem một sản phẩm có trong danh sách yêu thích hay không
        public async Task<bool> IsFavoriteProductAsync(Guid productId)
        {
            return await contextSPYT.SanPhamYeuThichs.AnyAsync(x => x.ChiTietSanPhamId == productId);
        }

        // Đếm số lượng sản phẩm yêu thích của người dùng
        public async Task<int> CountFavoriteProductsAsync(Guid userId)
        {
            return await contextSPYT.SanPhamYeuThichs.CountAsync(x => x.NguoiDungId == userId);
        }

        // Xóa toàn bộ sản phẩm trong danh sách yêu thích của người dùng
        public async Task<ResponseDto> ClearFavoriteProductsAsync(Guid userId)
        {
            try
            {
                var favorites = await contextSPYT.SanPhamYeuThichs.Where(x => x.NguoiDungId == userId).ToListAsync();
                contextSPYT.SanPhamYeuThichs.RemoveRange(favorites);
                await contextSPYT.SaveChangesAsync();

                return new ResponseDto { Code = 200, Message = "Xóa toàn bộ sản phẩm trong danh sách yêu thích thành công." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Code = 500, Message = $"Đã xảy ra lỗi khi xóa toàn bộ sản phẩm trong danh sách yêu thích: {ex.Message}" };
            }
        }

        // Lấy trạng thái yêu thích của một sản phẩm
        public async Task<FavoriteStatus> GetFavoriteStatusAsync(Guid userId, Guid productId)
        {
            var favoriteStatus = new FavoriteStatus();
            favoriteStatus.IsFavorite = await IsInFavoriteAsync(userId, productId);
            favoriteStatus.FavoriteCount = await CountFavoriteProductsAsync(userId);
            return favoriteStatus;
        }
    }
}