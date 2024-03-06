using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Api.Repository;
using Shop_Models.Entities;
using Shop_Models.Dto;
using System;
using Microsoft.EntityFrameworkCore;


namespace Shop_Api.Controllers
{
    
    public class AnhController : ControllerBase
    {
        private readonly IAnhRepository db2;
        public AnhController(IAnhRepository _db1)
        {
            db2 = _db1;

        }
        [HttpGet("Get-All-Anh")]
        public IEnumerable<Anh> GetAllAnh()
        {
            
                return  db2.GetAll();
            
        }
        [HttpPost("Create-Anh")]
        public async Task<IActionResult> CreateAnh( string MaAnh, string Url)
        {
            var obj = new Anh();
            obj.Guid = new Guid();
            obj.MaAnh=MaAnh;
            obj.URL = Url;  
            
            if ( MaAnh == null || Url == null)
            {
                return BadRequest("Du lieu them bi trong");
            }
            try
                
            {
               await db2.CreateAnh(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Update-Anh")]
        public async Task<IActionResult> UpdateAnh(Guid id, string MaAnh, string Url)
        {
            var obj = await db2.GetAnhById(id);
            obj.MaAnh = MaAnh;
            obj.URL = Url;
            try
            {
                await db2.UpdateAnh(id,obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete-Anh")]
        public async Task<IActionResult> DeleteAnh(Guid id)
        {
            
           var a= await db2.DeleteAnh(id);
            return Ok(a);
        }
    }
}
