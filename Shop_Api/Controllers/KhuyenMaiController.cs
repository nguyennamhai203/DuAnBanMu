using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Entities;
using Shop_Models.Heplers;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuyenMaiController : ControllerBase
    {
        private readonly IKhuyenMaiRepository _repository;

        public KhuyenMaiController(IKhuyenMaiRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int? status, int page=1)
        {
            var respon = await _repository.GetAsync(status,  page);
            return Ok(respon);
        }
        [HttpGet("GetAllProductInSale")]
        public async Task<IActionResult> GetAllProductInSale(Guid idSale)
        {
            var respon = await _repository.GetAllProductInSale(idSale);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        } 
        
        [HttpPost("RemoveSelectedProductPromotions")]
        public async Task<IActionResult> RemoveSelectedProductPromotions(List<Guid> idSale)
        {
            var respon = await _repository.RemoveSelectedProductPromotions(idSale);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        } 
        
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var respon = await _repository.GetAll();
            return Ok(respon);
        }
        //[Authorize(Roles = AppRole.Admin)]
        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync(Khuyenmai obj)
        {
            var respon = await _repository.CreateAsync(obj);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(Khuyenmai obj)
        {
            var respon = await _repository.UpdateAsync(obj);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }
        
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Khuyenmai obj)
        {
            var respon = await _repository.UpdateAsync(obj);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

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
