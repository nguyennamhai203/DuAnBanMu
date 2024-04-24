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
        [HttpGet("Get-All-HoaDon")]
        public IEnumerable<HoaDon> GetAllHD()
        {

            return _db.GetAll();

        }
        private static int HD1 = 0;
        [HttpPost("Create-HoaDon")]
        public async Task<IActionResult> CreateHD(HoaDon a)
        {
            var obj = new HoaDon();

            // Tạo mã hóa đơn tự sinh
            string maHoaDon = "HD" + (++HD1).ToString("D2");

            // Gán các giá trị từ đối tượng a
            obj.Id = Guid.NewGuid(); // Tạo một ID mới
            obj.MaHoaDon = maHoaDon;
            obj.NgayTao = a.NgayTao;
            obj.NgayThanhToan = a.NgayThanhToan;
            obj.NgayShip = a.NgayShip;
            obj.NgayNhan = a.NgayNhan;
            obj.MoTa = a.MoTa;
            obj.TenKhachHang = a.TenKhachHang;
            obj.SoDienThoai = a.SoDienThoai;
            obj.DiaChiGiaoHang = a.DiaChiGiaoHang;
            obj.TienGiam = a.TienGiam;
            obj.TienShip = a.TienShip;
            obj.TongTien = a.TongTien;
            obj.TrangThaiGiaoHang = a.TrangThaiGiaoHang;
            obj.TrangThaiThanhToan = a.TrangThaiThanhToan;
            obj.NgayGiaoDuKien = a.NgayGiaoDuKien;
            obj.LiDoHuy = a.LiDoHuy;
            // Các dòng còn lại giữ nguyên

            // Kiểm tra dữ liệu
            if (string.IsNullOrEmpty(obj.MaHoaDon) || obj.NgayTao == null || obj.NgayThanhToan == null || obj.NgayShip == null || obj.NgayNhan == null
                || string.IsNullOrEmpty(obj.MoTa) || string.IsNullOrEmpty(obj.TenKhachHang) || string.IsNullOrEmpty(obj.SoDienThoai) || string.IsNullOrEmpty(obj.DiaChiGiaoHang) 
                || obj.TienGiam == null || obj.TienShip == null || obj.TongTien == null || obj.TrangThaiGiaoHang == null
                || obj.TrangThaiThanhToan == null || obj.NgayGiaoDuKien == null || string.IsNullOrEmpty(obj.LiDoHuy))
            {
                return BadRequest("Dữ liệu thêm bị trống");
            }
            
            try
            {
                await _db.CreateHD(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
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
        [HttpPut("CancelOrder")]
        public async Task<IActionResult> CancelOrder(Guid id, string reason)
        {
            try
            {
                await _db.CancelOrder(id, reason);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }

        [HttpGet("CheckCustomerExistence")]
        public async Task<ActionResult<bool>> CheckCustomerExistence(Guid id)
        {
            try
            {
                var exists = await _db.CheckCustomerExistence(id);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
            }
        }
        //[HttpGet("GetAllHoaDonCho")]
        //public async Task<List<HoaDon>> GetAllHoaDonCho()
        //{
        //    return _db.GetAllHoaDonCho();
        //}
        [HttpGet("GetHoaDonOnline")]
        public async Task<List<HoaDon>> GetHoaDonOnline()
        {
            return _db.GetAll();
        }
        // GET: api/HoaDon/{id}
        [HttpGet("GetById")]
        public async Task<ActionResult<HoaDon>> GetById(Guid id)
        {
            var hoaDon = await _db.GetById(id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            return hoaDon;
        }
        //[HttpPut("UpdateTrangThaiGiaoHangHoaDon")]
        //public async Task<IActionResult> UpdateTrangThaiGiaoHangHoaDon(Guid idHoaDon, int TrangThai, string? Lido, DateTime? ngayCapNhatGanNhat)
        //{
        //    try
        //    {
        //        // Lấy thông tin hóa đơn từ repository
        //        var hoadon = await _db.GetById(idHoaDon);

        //        if (hoadon == null)
        //        {
        //            return NotFound("Không tìm thấy hóa đơn.");
        //        }

        //        // Cập nhật thông tin hóa đơn
        //        hoadon.TrangThaiGiaoHang = TrangThai;
        //        hoadon.LiDoHuy = Lido;
        //        hoadon.NgayGiaoDuKien = ngayCapNhatGanNhat ?? hoadon.NgayGiaoDuKien;

        //        // Kiểm tra nếu trạng thái giao hàng là Đã Giao, cập nhật trạng thái thanh toán
        //        if (TrangThai == (int)TrangThaiGiaoHang.DaGiaoHang)
        //        {
        //            hoadon.TrangThaiThanhToan = 1; // Đánh dấu là đã thanh toán
        //        }

        //        // Gọi phương thức từ repository để cập nhật hóa đơn
        //        var response = await _db.UpdateTrangThaiGiaoHangHoaDon(idHoaDon, TrangThai, Lido, ngayCapNhatGanNhat);

        //        if (response.IsSuccess)
        //        {
        //            // Trả về kết quả cập nhật thành công
        //            return Ok("Cập nhật trạng thái giao hàng của hóa đơn thành công.");
        //        }
        //        else
        //        {
        //            return BadRequest($"Cập nhật trạng thái giao hàng của hóa đơn không thành công. Lỗi: {response.Message}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}");
        //    }
        //}



        [HttpPut("UpdateNgayHoaDonOnline")]
        public async Task<IActionResult> UpdateNgayHoaDonOnline(Guid idHoaDon, DateTime? NgayThanhToan, DateTime? NgayNhan, DateTime? NgayShip)
        {
            try
            {
                var response = await _db.UpdateNgayHoaDonOnline(idHoaDon, NgayThanhToan, NgayNhan, NgayShip);
                if (response.IsSuccess)
                {
                    return Ok(response); // Trả về 200 OK nếu cập nhật thành công
                }
                else
                {
                    return BadRequest(response); // Trả về 400 Bad Request nếu có lỗi
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server nội bộ: {ex.Message}"); // Trả về lỗi server nếu có lỗi xảy ra
            }
        }
    }
}
