using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Api.Services.IServices;
using Shop_Models.Entities;
using Shop_Models.Heplers;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeRepository _repository;
        private readonly IThongKeSanPhamServices _service;
        public ThongKeController(IThongKeRepository repository,IThongKeSanPhamServices service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet("Get-top-5-san-pham-ban-chay")]
        public async Task<IActionResult> GetTop5Products()
        {
            var result = await _service.GetTop5Products();
            return Ok(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int? status, int page)
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

        
    }
}