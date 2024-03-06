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
    
    public class MauSacController : ControllerBase
    {
        private readonly IMauSacRepository db3;
        public MauSacController(IMauSacRepository _db1)
        {
            db3 = _db1;

        }
        [HttpGet("Get-All-MauSac")]
        public IEnumerable<MauSac> GetAllMauSac()
        {
            
                return db3.GetAll();
            
        }
        [HttpPost("Create-MauSac")]
        public async Task<IActionResult> CreateMauSac( string MaMau, string tenMau, int TrangThai)
        {
            var obj = new MauSac();
            obj.Guid = new Guid();
            obj.MaMauSac=MaMau;
            obj.TenMauSac = tenMau;
            obj.TrangThai = TrangThai;
            
            if ( MaMau == null || tenMau == null|| TrangThai==null)
            {
                return BadRequest("Du lieu them bi trong");
            }
            try
                
            {
               await db3.CreateMS(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Update-MS")]
        public async Task<IActionResult> UpdateMauSac(Guid id, string MaMau, string tenMau, int TrangThai)
        {
            var obj = await db3.GetById(id);
            obj.MaMauSac = MaMau;
            obj.TenMauSac = tenMau;
            obj.TrangThai = TrangThai;
            try
            {
                await db3.UpdateMS(id,obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete-MS")]
        public async Task<IActionResult> DeleteMauSac(Guid id)
        {
            
           var a= await db3.DeleteMS(id);
            return Ok(a);
        }
    }
}
