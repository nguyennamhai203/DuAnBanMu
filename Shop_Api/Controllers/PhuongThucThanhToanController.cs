using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhuongThucThanhToanController : ControllerBase
    {
        public readonly IPhuongThucThanhToanRepository _ptttRepository;

        public PhuongThucThanhToanController(IPhuongThucThanhToanRepository ptttRepository)
        {
            _ptttRepository = ptttRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PhuongThucThanhToan>>> GetAll()
        {
            var phuongThucThanhToans = await _ptttRepository.GetAllAsync();
            return Ok(phuongThucThanhToans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhuongThucThanhToan>> GetById(Guid id)
        {
            var phuongThucThanhToan = await _ptttRepository.GetByIdAsync(id);
            if (phuongThucThanhToan == null)
            {
                return NotFound();
            }
            return Ok(phuongThucThanhToan);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create(PhuongThucThanhToan phuongThucThanhToan)
        {
            phuongThucThanhToan.Id = Guid.NewGuid();
            var result = await _ptttRepository.CreateAsync(phuongThucThanhToan);
            return StatusCode(result.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> Update(Guid id, PhuongThucThanhToan phuongThucThanhToan)
        {
            var result = await _ptttRepository.UpdateAsync(id, phuongThucThanhToan);
            return StatusCode(result.Code, result);
        }
        // hhhhh
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> Delete(Guid id)
        {
            var result = await _ptttRepository.DeleteAsync(id);
            return StatusCode(result.Code, result);
        }
    }
}
