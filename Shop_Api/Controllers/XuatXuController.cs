using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cmp;
using Shop_Api.AppDbContext;
using Shop_Api.Migrations;
using Shop_Api.Repository;
using Shop_Api.Repository.IRepository;
using Shop_Models.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XuatXuController : ControllerBase
    {
        private readonly IXuatXuRepository res;
        public XuatXuController(IXuatXuRepository repository)
        {
            res = repository;
        }
        // GET: api/<XuatXuController>
        [HttpGet("Get-xuat-xu")]
        public async Task<IActionResult> GetXuatXuAsync(int? status,int page = 1)
        {
            try
            {
                var list = await res.GetListXuatXu(status,page);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        } 
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await res.GetXuatXu();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        // POST api/<XuatXuController>
        [HttpPost("post-xuat-xu-param")]
        public async Task<IActionResult> PostXuatXuByParam(Guid guid,string maxuatxu,string tenxuatxu,int trangthai) 
        {
            var obj = new XuatXu();
            obj.Guid = guid;
            obj.MaXuatXu = maxuatxu;
            obj.TenXuatXu = tenxuatxu;
            obj.TrangThai = trangthai;
            if (guid == null || maxuatxu == null || tenxuatxu == null || trangthai == null)
            {
                return BadRequest("Du lieu them bi trong");
            }
            try
            {
                //await res.CreateXX(obj);
                //return CreatedAtAction(nameof(GetXuatXuAsync),obj);
                await res.CreateXX(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<XuatXuController>/5
        [HttpPut("put-xuat-xu")]
        public async Task<IActionResult> PutXuatXuAsync(Guid id,string maxuatxu, string tenxuatxu, int trangthai) 
        {
            var obj = new XuatXu();
            obj.Guid = id;
            obj.MaXuatXu = maxuatxu;
            obj.TenXuatXu = tenxuatxu;
            obj.TrangThai = trangthai;
            var update = await res.GetByIdXuatXu(id);
            if (update.IsSuccess == false) return BadRequest("Invalid XuatXu object");
            else await res.UpdateXX(obj);
            return NoContent();
        }
        // DELETE api/<XuatXuController>/5
        [HttpDelete("delete-xuat-xu")]
        public async Task<IActionResult> DeleteXuatXuAsync(Guid id) 
        {
            if (id == Guid.Empty) return NotFound();
            await res.DeleteXX(id);
            return NoContent();
        }
    }
}