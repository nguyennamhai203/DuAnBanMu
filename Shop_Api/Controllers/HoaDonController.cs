using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Api.Repository;
using Shop_Models.Entities;
using Shop_Models.Dto;
using System;
using Microsoft.EntityFrameworkCore;
using Shop_Api.Services.IServices;
using Shop_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace Shop_Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonRepository _db;
        private readonly IHoaDonServices _hoaDonServices;
        public HoaDonController(IHoaDonRepository _db1, IHoaDonServices hoaDonServices)
        {
            _db = _db1;
            _hoaDonServices = hoaDonServices;

        }
        [HttpGet("Get-all")]
        public async Task<IActionResult> GetHD(int? status,int page)
        {

            var result = await _db.GetAsync(status,page);
            return Ok(result);

            return _db.GetAll();

        }
        [HttpPost("Create-HoaDon")]
        public async Task<IActionResult> CreateHD(HoaDon a)
        {
            var obj = new HoaDon();
            obj.Id = new Guid();
            obj.MaHoaDon = a.MaHoaDon;
            obj.NgayTao = a.NgayTao;
            obj.NgayThanhToan = a.NgayThanhToan;
            obj.NgayShip = a.NgayShip;
            obj.NgayNhan = a.NgayNhan;
            obj.MoTa = a.MoTa;
            obj.TienGiam = a.TienGiam;
            obj.TienShip = a.TienShip;
            obj.TongTien = a.TongTien;
            obj.TrangThaiGiaoHang = a.TrangThaiGiaoHang;
            obj.TrangThaiThanhToan = a.TrangThaiThanhToan;
            obj.NgayGiaoDuKien = a.NgayGiaoDuKien;
            obj.LiDoHuy = a.LiDoHuy;





            if (a.MaHoaDon == null || a.NgayTao == null || a.NgayThanhToan == null || a.NgayShip == null || a.NgayNhan == null
                || a.MoTa == null || a.TienGiam == null || a.TienShip == null || a.TongTien == null || a.TrangThaiGiaoHang == null
                    || a.TrangThaiThanhToan == null || a.NgayGiaoDuKien == null || a.LiDoHuy == null)
            {
                return BadRequest("Du lieu them bi trong");
            }
            try
            {
                await _db.CreateHD(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Update-HD")]
        public async Task<IActionResult> UpdateHD(Guid id, HoaDon a)
        {
            var obj = await _db.GetById(id);

            obj.MaHoaDon = a.MaHoaDon;
            obj.NgayTao = a.NgayTao;
            obj.NgayThanhToan = a.NgayThanhToan;
            obj.NgayShip = a.NgayShip;
            obj.NgayNhan = a.NgayNhan;
            obj.MoTa = a.MoTa;
            obj.TienGiam = a.TienGiam;
            obj.TienShip = a.TienShip;
            obj.TongTien = a.TongTien;
            obj.TrangThaiGiaoHang = a.TrangThaiGiaoHang;
            obj.TrangThaiThanhToan = a.TrangThaiThanhToan;
            obj.NgayGiaoDuKien = a.NgayGiaoDuKien;
            obj.LiDoHuy = a.LiDoHuy;

            try
            {
                await _db.UpdateHD(id, obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateBill")]
        public async Task<IActionResult> CreateBill(RequestBillDto request)
        {
            var reponse = await _hoaDonServices.CreateBill(request);
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            return BadRequest("");
        }

        [AllowAnonymous]
        [HttpGet("PGetBillByInvoiceCode")]
        public async Task<IActionResult> PGetBillByInvoiceCode(string invoiceCode)
        {
            var result = await _hoaDonServices.PGetBillByInvoiceCode(invoiceCode);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
    }
}
