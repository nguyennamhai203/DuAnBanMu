using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonCTController : ControllerBase
    {
        public readonly IHoaDonChiTietRepository HDCTREpository;
        public readonly IHoaDonRepository _hoaDonRepository;

        public HoaDonCTController(IHoaDonChiTietRepository hDCTREpository, IHoaDonRepository hoaDonRepository)
        {
            HDCTREpository = hDCTREpository;
            _hoaDonRepository = hoaDonRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<HoaDonChiTiet>>> GetAll()
        {
            var HDCT = await HDCTREpository.GetAllAsync();
            return Ok(HDCT);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDonChiTiet>> GetById(Guid id)
        {
            var HDCT = await HDCTREpository.GetByIdAsync(id);
            if (HDCT == null)
            {
                return NotFound();
            }
            return Ok(HDCT);
        }
        [HttpGet("GetAllByInvoiceCode")]
        public async Task<ActionResult<HoaDonChiTiet>> GetAllByInvoiceCode(string mahoadon)
        {
            var HDCT = await _hoaDonRepository.GetBillDetailByInvoiceCode(mahoadon);
            if (HDCT == null)
            {
                return NotFound();
            }
            return Ok(HDCT);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create(HoaDonChiTiet HDCT)
        {
            HDCT.Guid = HDCT.Guid;
            var result = await HDCTREpository.CreateAsync(HDCT);
            return StatusCode(result.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> Update(Guid id, HoaDonChiTiet HDCT)
        {
            var result = await HDCTREpository.UpdateAsync(id, HDCT);
            return StatusCode(result.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> Delete(Guid id)
        {
            var result = await HDCTREpository.DeleteAsync(id);
            return StatusCode(result.Code, result);
        }
    }
}
