using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Entities;
using Shop_Models.Heplers;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeRepository _repository;
        private readonly IChiTietSanPhamRepository chiTietSanPhamRepository;
        private readonly ApplicationDbContext _context;
        public ThongKeController(IThongKeRepository repository,ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet("Get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int? status, int page = 1)
        {
            var result = await _repository.GetAsync(status, page);
            return Ok(result);
        }

        [Authorize(Roles = AppRole.Admin)]
        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync(ThongKe obj)
        {
            var respon = await _repository.CreateAsync(obj);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        [Authorize(Roles = AppRole.Admin)]
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(ThongKe obj)
        {
            var respon = await _repository.UpdateAsync(obj);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        [Authorize(Roles = AppRole.Admin)]
        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var respon = await _repository.DeleteAsync(Id);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        /*[Authorize(Roles = AppRole.Admin)]
        [HttpGet("Thong-ke-san-pham-theo-ngay")]
        public async Task<IActionResult> ThongKeSanPhamTheoNgay(DateTime ngay, int? status, int page)
        {
            var thongke = from x in _repository.GetAll()
                          from y in chiTietSanPhamRepository.GetAllAsync()

            return Ok();
        }*/
    }
}