using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Shop_Api.Repository.IRepository;
using Shop_Models.Entities;
using Shop_Models.ViewModels.Vouchers;
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
        public async Task<IActionResult> PostVoucherAsync(CreateVoucher createVoucher)
        {
            var obj = new Voucher();
            obj.Guid = Guid.NewGuid();
            obj.MaVoucher = createVoucher.MaVoucher;
            obj.TenVoucher = createVoucher.TenVoucher;
            obj.SoLuong = createVoucher.SoLuong;
            obj.PhanTramGiam = createVoucher.PhanTramGiam;
            obj.NgayBatDau = createVoucher.NgayBatDau;
            obj.NgayHetHan = createVoucher.NgayHetHan;
            obj.TrangThai = 0;
            if (createVoucher == null)
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
        [HttpPut("put-voucher/{id:Guid}")]
        public async Task<IActionResult> PutVoucherAsync(Voucher voucher, Guid id)
        {
            var respon = await res.UpdateVoucher(voucher, id);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        // DELETE api/<VoucherController>/5
        [HttpDelete("removeVoucher/{id:Guid}")]
        public async Task<IActionResult> RemoveVoucherAsync(Guid id)
        {
            var respon = await res.DeleteVoucher(id);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        [HttpGet("getAllVoucher")]
        public async Task<IActionResult> GetVoucher()
        {
            try
            {
                var list = await res.GetVoucher();
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };
                string json = JsonConvert.SerializeObject(list, settings);
                JToken parsedJson = JToken.Parse(json);
                string formattedJson = parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);
                return Ok(formattedJson);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-byid-voucher/{id:Guid}")]
        public async Task<IActionResult> GetByIdVoucher(Guid id)
        {
            try
            {
                var list = await res.GetByIdVoucher(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}