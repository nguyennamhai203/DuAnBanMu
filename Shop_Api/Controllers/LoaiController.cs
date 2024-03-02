using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        public readonly ILoaiRepository _loaiRepository;
        public LoaiController(ILoaiRepository loaiRepository)
        {
            _loaiRepository = loaiRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Loai>>> Get()
        {
            var loais = await _loaiRepository.GetAllAsync();
            return Ok(loais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loai>> GetById(Guid id)
        {
            var loai = await _loaiRepository.GetByIdAsync(id);
            if (loai == null)
            {
                return NotFound();
            }
            return Ok(loai);
        }

        [HttpPost("CreateAsync")]
        public async Task<ActionResult<ResponseDto>> Create(Loai loai)
        {
            loai.Id = Guid.NewGuid();
            var respon = await _loaiRepository.CreateAsync(loai);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> Update(Guid id, Loai loai)
        {
            var result = await _loaiRepository.UpdateAsync(id,loai);
            return StatusCode(result.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> Delete(Guid id)
        {
            var result = await _loaiRepository.DeleteAsync(id);
            return StatusCode(result.Code, result);
        }
    }
}
