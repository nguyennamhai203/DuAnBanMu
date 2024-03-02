using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongHieuController : ControllerBase
    {
        public readonly IThuongHieuRepository THRepository;
        public ThuongHieuController(IThuongHieuRepository thuongHieuRepository)
        {
            this.THRepository = thuongHieuRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<ThuongHieu>>> GetAll()
        {
            var ThuongHieu = await THRepository.GetAllAsync();
            return Ok(ThuongHieu);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ThuongHieu>> GetById(Guid id)
        {
            var thuonghieu = await THRepository.GetByIdAsync(id);
            if (thuonghieu == null)
            {
                return NotFound();
            }
            return Ok(thuonghieu);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create(ThuongHieu thuonghieu)
        {
            thuonghieu.Guid = Guid.NewGuid();
            var result = await THRepository.CreateAsync(thuonghieu);
            return StatusCode(result.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> Update(Guid id, ThuongHieu thuonghieu)
        {
            var result = await THRepository.UpdateAsync(id, thuonghieu);
            return StatusCode(result.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> Delete(Guid id)
        {
            var result = await THRepository.DeleteAsync(id);
            return StatusCode(result.Code, result);
        }
    }
}
