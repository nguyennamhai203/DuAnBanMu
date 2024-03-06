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
    
    public class PhuongThucThanhToanChiTietController : ControllerBase
    {
        private readonly IPhuongThucThanhToanChiTietRepository db4;
        public PhuongThucThanhToanChiTietController(IPhuongThucThanhToanChiTietRepository _db1)
        {
            db4 = _db1;

        }
        [HttpGet("Get-All-PTTTCT")]
        public IEnumerable<PhuongThucTTChiTiet> GetAllPTTTCT()
        {
            
                return db4.GetAll();
            
        }
        [HttpPost("Create-PTTTCT")]
        public async Task<IActionResult> CreatePTTTCT( Guid hdid, Guid ptttid, double sotien, int trangthai)
        {
            var obj = new PhuongThucTTChiTiet();
            obj.Id = new Guid();
            obj.HoaDonId=hdid;
            obj.PTTToanId=ptttid;
            obj.SoTien = sotien;
            obj.TrangThai = trangthai;
            
            if ( hdid == null || ptttid == null|| sotien==null || trangthai==null)
            {
                return BadRequest("Du lieu them bi trong");
            }
            try
                
            {
               await db4.Create(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Update-PTTTCT")]
        public async Task<IActionResult> UpdatePTTTCT(Guid id ,Guid hdid, Guid ptttid, double sotien, int trangthai)
        {
            var obj = await db4.GetById(id);
            obj.HoaDonId = hdid;
            obj.PTTToanId = ptttid;
            obj.SoTien = sotien;
            obj.TrangThai = trangthai;
            try
            {
                await db4.Update(id,obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete-PTTTCT")]
        public async Task<IActionResult> DeletePTTTCT(Guid id)
        {
            
           var a= await db4.Delete(id);
            return Ok(a);
        }
    }
}
