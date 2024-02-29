using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
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
        // GET: api/<SanPhamYeuThichController>
        [HttpGet("get-spyt")]
        public async Task<IActionResult> GetSPYTAsync(int? status,int page = 1)
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

        // POST api/<SanPhamYeuThichController>
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

        // PUT api/<SanPhamYeuThichController>/5
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

        // DELETE api/<SanPhamYeuThichController>/5
        [HttpDelete("delete-spyt")]
        public async Task<IActionResult> DeleteSPYTAsync(Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            await res.DeleteSPYT(id);
            return NoContent();
        }
    }
}