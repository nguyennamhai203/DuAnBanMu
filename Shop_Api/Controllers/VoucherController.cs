using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Entities;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRepository res;
        public VoucherController(IVoucherRepository repository)
        {
            res = repository;
        }
        // GET: api/<VoucherController>
        [HttpGet("get-voucher")]
        public async Task<IActionResult> GetVoucherAsync(int? status, int page = 1)
        {
            try
            {
                var list = await res.GetListVoucher(status, page);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<VoucherController>
        [HttpPost("post-voucher")]
        public async Task<IActionResult> PostVoucherAsync(Guid guid, string mavoucher, string tenvoucher, int phantramgiam, int soluong, DateTime ngaybatdau, DateTime ngayketthuc, int trangthai)
        {
            var obj = new Voucher();
            obj.Guid = guid;
            obj.MaVoucher = mavoucher;
            obj.TenVoucher = tenvoucher;
            obj.SoLuong = soluong;
            obj.PhanTramGiam = phantramgiam;
            obj.NgayBatDau = ngaybatdau;
            obj.NgayHetHan = ngayketthuc;
            obj.TrangThai = trangthai;
            if (guid == null || mavoucher == null || tenvoucher == null || soluong == null || phantramgiam == null || ngaybatdau == null || ngayketthuc == null || trangthai == null)
            {
                return BadRequest("Du lieu them bi trong");
            }
            try
            {
                await res.CreateVoucher(obj);
                return CreatedAtAction("GetVoucher", "Voucher", obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<VoucherController>/5
        [HttpPut("put-voucher")]
        public async Task<IActionResult> PutVoucherAsync(Guid id, string mavoucher, string tenvoucher, int phantramgiam, int soluong, DateTime ngaybatdau, DateTime ngayketthuc, int trangthai)
        {
            var obj = new Voucher();
            obj.Guid = id;
            obj.MaVoucher = mavoucher;
            obj.TenVoucher = tenvoucher;
            obj.SoLuong = soluong;
            obj.PhanTramGiam = phantramgiam;
            obj.NgayBatDau = ngaybatdau;
            obj.NgayHetHan = ngayketthuc;
            obj.TrangThai = trangthai;
            var update = await res.GetByIdVoucher(id);
            if (update.IsSuccess == false) return BadRequest("Invalid XuatXu object");
            else await res.UpdateVoucher(obj);
            return NoContent();
        }

        // DELETE api/<VoucherController>/5
        [HttpDelete("delete-voucher")]
        public async Task<IActionResult> DeleteVoucherAsync(Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            await res.DeleteVoucher(id);
            return NoContent();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var result = await res.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NoContent();
        }



    }
}