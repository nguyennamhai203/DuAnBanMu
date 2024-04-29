using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System;
using System.Security.Principal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamYeuThichController : ControllerBase
    {
        private readonly ISanPhamYeuThichRepository res;
        public SanPhamYeuThichController(ISanPhamYeuThichRepository repository)
        {
            res = repository;
        }
        
        [HttpGet("get-spyt")]
        public async Task<IActionResult> GetSPYTAsync(int? status,int page)
        {
            try
            {
                var list = await res.GetListSPYT(status, page);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("post-spyt")]
        public async Task<IActionResult> PostSPYTAsync(Guid id,Guid nguoidungid,Guid chitietsanphamid,int trangthai)
        {
            var obj = new SanPhamYeuThich();
            obj.Id = id;
            obj.NguoiDungId = nguoidungid;
            obj.ChiTietSanPhamId = chitietsanphamid;
            obj.TrangThai = trangthai;
            if (id == null || nguoidungid == null || chitietsanphamid == null || trangthai == null)
            {
                return BadRequest("Du lieu them bi trong");
            }
            try
            {
                await res.CreateSPYT(obj);
                return CreatedAtAction(nameof(GetSPYTAsync), obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("put-spyt")]
        public async Task<IActionResult> PutSPYTAsync(Guid id, Guid nguoidungid, Guid chitietsanphamid, int trangthai)
        {
            var obj = new SanPhamYeuThich();
            obj.Id = id;
            obj.NguoiDungId = nguoidungid;
            obj.ChiTietSanPhamId = chitietsanphamid;
            obj.TrangThai = trangthai;
            var update = await res.GetByIdSPYT(id);
            if (update.IsSuccess == false) return BadRequest("Invalid XuatXu object");
            else await res.UpdateSPYT(obj);
            return NoContent();
        }

        [HttpDelete("delete-spyt")]
        public async Task<IActionResult> DeleteSPYTAsync(Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            await res.DeleteSPYT(id);
            return NoContent();
        }

        [HttpPost("Thêm một sản phẩm vào danh sách yêu thích của người dùng")]
        public async Task<IActionResult> AddToFavoriteAsync(Guid userId, Guid productId)
        {
            try
            {
                var response = await res.AddToFavoriteAsync(userId, productId);
                return StatusCode(response.Code, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Xóa một sản phẩm khỏi danh sách yêu thích của người dùng")]
        public async Task<IActionResult> RemoveFromFavoriteAsync(Guid userId, Guid productId)
        {
            try
            {
                var response = await res.RemoveFromFavoriteAsync(userId, productId);
                return StatusCode(response.Code, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Kiểm tra xem một sản phẩm có trong danh sách yêu thích của người dùng hay không")]
        public async Task<IActionResult> IsInFavoriteAsync(Guid userId, Guid productId)
        {
            try
            {
                var isFavorite = await res.IsInFavoriteAsync(userId, productId);
                return Ok(isFavorite);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Lấy danh sách các sản phẩm yêu thích của người dùng")]
        public async Task<IActionResult> GetFavoriteProductsAsync(Guid userId)
        {
            try
            {
                var favoriteProducts = await res.GetFavoriteProductsAsync(userId);
                return Ok(favoriteProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Kiểm tra xem một sản phẩm có trong danh sách yêu thích hay không")]
        public async Task<IActionResult> IsFavoriteProductAsync(Guid productId)
        {
            try
            {
                var isFavoriteProduct = await res.IsFavoriteProductAsync(productId);
                return Ok(isFavoriteProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Đếm số lượng sản phẩm yêu thích của người dùng")]
        public async Task<IActionResult> CountFavoriteProductsAsync(Guid userId)
        {
            try
            {
                var count = await res.CountFavoriteProductsAsync(userId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Xóa toàn bộ sản phẩm trong danh sách yêu thích của người dùng")]
        public async Task<IActionResult> ClearFavoriteProductsAsync(Guid userId)
        {
            try
            {
                var response = await res.ClearFavoriteProductsAsync(userId);
                return StatusCode(response.Code, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Lấy trạng thái yêu thích của một sản phẩm")]
        public async Task<IActionResult> GetFavoriteStatusAsync(Guid userId, Guid productId)
        {
            try
            {
                var favoriteStatus = await res.GetFavoriteStatusAsync(userId, productId);
                return Ok(favoriteStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}