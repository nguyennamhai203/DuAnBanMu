using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Api.Services.IServices;
using Shop_Models.Entities;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GioHangController : ControllerBase
	{
		private readonly IGioHangRepository res;
		private readonly IGioHangChiTietRepository resGHCT;
		private readonly IGioHangChiTietServices _reposGHCT;
		public GioHangController(IGioHangRepository repository, IGioHangChiTietServices reposGHCT, IGioHangChiTietRepository gioHangChiTietRepository)
		{
			res = repository;
			_reposGHCT = reposGHCT;
			resGHCT = gioHangChiTietRepository;
		}
		// GET: api/<GioHangController>
		[HttpGet("get-gio-hang")]
		public async Task<IActionResult> GetGioHangAsync(int? status, int page = 1)
		{
			try
			{
				var list = await res.GetListGioHang(status, page);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// POST api/<GioHangController>
		[HttpPost("post-gio-hang")]
		public async Task<IActionResult> PostGioHangAsync(Guid idnguoidung, Guid nguoidungid, int trangthai)
		{
			var obj = new GioHang();
			obj.IdNguoiDung = idnguoidung;
			obj.TrangThai = trangthai;
			if (idnguoidung == null || trangthai == null)
			{
				return BadRequest("Du lieu them bi trong");
			}
			try
			{
				await res.CreateGioHang(obj);
				return CreatedAtAction(nameof(GetGioHangAsync), obj);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// PUT api/<GioHangController>/5
		[HttpPut("put-gio-hang")]
		public async Task<IActionResult> PutGioHangAsync(Guid idnguoidung, Guid nguoidungid, int trangthai)
		{
			var obj = new GioHang();
			obj.IdNguoiDung = idnguoidung;
			obj.TrangThai = trangthai;
			var update = await res.GetByIdGioHang(idnguoidung);
			if (update.IsSuccess == false) return BadRequest("Invalid XuatXu object");
			else await res.UpdateGioHang(obj);
			return NoContent();
		}

		// DELETE api/<GioHangController>/5
		[HttpDelete("delete-gio-hang")]
		public async Task<IActionResult> DeleteGioHangAsync(Guid id)
		{
			if (id == Guid.Empty) return NotFound();
			await res.DeleteGioHang(id);
			return NoContent();
		}


		[HttpGet("add-gio-hang")]
		public async Task<IActionResult> AddToCart(string userName, string codeProductDetail,int? soluong)
		{
			try
			{
				var result = await _reposGHCT.AddCart(userName, codeProductDetail, soluong);
				if (result.IsSuccess == true)
				{
					return Ok(result);
				}return BadRequest(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}


		[HttpGet("CongQuantityCartDetail")]
		public async Task<IActionResult> CongQuantityCartDetail(Guid idCartDetail)
		{
			try
			{
				var result = await _reposGHCT.CongQuantityCartDetail(idCartDetail);
				if (result.IsSuccess == true)
				{
					return Ok(result);
				}
				return BadRequest(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}


        [HttpGet("TruQuantityCartDetail")]
        public async Task<IActionResult> TruQuantityCartDetail(Guid idCartDetail)
        {
            try
            {
                var result = await _reposGHCT.TruQuantityCartDetail(idCartDetail);
                if (result.IsSuccess == true)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("CapNhatSoLuongCartDetail")]
        public async Task<IActionResult> CapNhatSoLuongCartDetail(Guid idCartDetail,int soLuong)
        {
            try
            {
                var result = await _reposGHCT.CapNhatSoLuongCartDetail(idCartDetail,soLuong);
                if (result.IsSuccess == true)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("DeleteCartDetail")]
        public async Task<IActionResult> DeleteCartDetail(Guid idCartDetail)
        {
            try
            {
                var result = await _reposGHCT.DeleteCartDetail(idCartDetail);
                if (result.IsSuccess == true)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("TimGioHangChiTIet")]
        public async Task<IActionResult> TimGioHangChiTIet(string userName, string codeProductDetail)
        {
            try
            {
                var result = await resGHCT.TimGioHangChiTIet(userName,codeProductDetail);
                if (result!=null)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}