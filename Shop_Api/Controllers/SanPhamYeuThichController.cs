using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Api.Services.IServices;
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
        private readonly ISanPhamYeuThichServices _services;
        public SanPhamYeuThichController(ISanPhamYeuThichRepository repository, ISanPhamYeuThichServices service)
        {
            res = repository;
            _services = service;
        }

        [HttpGet("get-spyt")]
        public async Task<IActionResult> GetSPYTAsync(int? status, int page)
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
        public async Task<IActionResult> PostSPYTAsync(Guid id, Guid nguoidungid, Guid chitietsanphamid, int trangthai)
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

        [HttpGet("Xoa-mot-san-pham-khoi-danh-sach-yeu-thich")]
        public async Task<IActionResult> DeleteSPYTAsync(Guid id)
        {
            //if (id == Guid.Empty) return NotFound();
            //await res.DeleteSPYT(id);
            //return NoContent();

            try
            {
                var response = await res.DeleteSPYT(id);
                //return StatusCode(response.Code, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Xoa-mot-san-pham-khoi-danh-sach-yeu-thich-cua-nguoi-dung")]
        public async Task<IActionResult> DeleteSPYTAsync(Guid userId, Guid productId)
        {
            if (userId == Guid.Empty || productId == Guid.Empty) return NotFound();
            await res.DeleteSPYT(productId);
            return NoContent();
        }

        [HttpGet("Nguoi-dung-xem-danh-sach-san-pham-yeu-thich")]
        public async Task<IActionResult> GetFavoriteProductsAsync(Guid userId)
        {
            try
            {
                var favoriteProducts = await _services.GetFavoriteProductsForUser(userId);
                return Ok(favoriteProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Them-mot-san-pham-vao-danh-sach-yeu-thich-cua-nguoi-dung")]
        public async Task<IActionResult> AddToFavoriteAsync(Guid userId, Guid productId)
        {
            try
            {
                var response = await _services.AddToFavoriteBus(userId, productId);
                return StatusCode(response.Code, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}