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
using static Shop_Models.Heplers.TrangThai;

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

            // Kiểm tra xem mã hóa đơn đã tồn tại chưa
            var existingHD = await _db.GetHoaDonByMaHoaDonAsync(maHoaDon);
            while (existingHD != null)
            {
                // Nếu mã đã tồn tại, tạo lại mã mới
                maHoaDon = "HD" + (++HD1).ToString("D2");
                existingHD = await _db.GetHoaDonByMaHoaDonAsync(maHoaDon);
            }
            // Gán các giá trị từ đối tượng a
            obj.Id = Guid.NewGuid(); // Tạo một ID mới
            obj.VoucherId = a.VoucherId;
            obj.NguoiDungId = a.NguoiDungId;
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
            if (obj.VoucherId == null || obj.NguoiDungId == null || string.IsNullOrEmpty(obj.MaHoaDon) || obj.NgayTao == null || obj.NgayThanhToan == null || obj.NgayShip == null || obj.NgayNhan == null
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
        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(Guid id, string reason)
        {
            try
            {
                var result = await _db.CancelOrder(id, reason);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
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
        [HttpGet("HoaDonStatus")]
        public async Task<ActionResult<List<HoaDon>>> GetByHoaDonStatus(int HoaDonStatus)
        {
            // Kiểm tra xem giá trị truyền vào có hợp lệ không bằng cách kiểm tra trong enum
            if (!Enum.IsDefined(typeof(TrangThaiGiaoHang), HoaDonStatus))
            {
                return BadRequest(new ResponseDto { IsSuccess = false, Message = "Trạng thái hóa đơn không hợp lệ.", Code = 400 });
            }

            // Tiếp tục lấy dữ liệu nếu giá trị hợp lệ
            var hoaDons = await _db.GetByHoaDonStatus(HoaDonStatus);

            if (hoaDons == null || hoaDons.Count == 0)
            {
                return NotFound(new ResponseDto { IsSuccess = false, Message = "Không tìm thấy hóa đơn cho trạng thái đã cho.", Code = 400 });
                //return BadRequest(new ResponseDto { IsSuccess = false, Message = "Không tìm thấy hóa đơn cho trạng thái đã cho.", Code = 400 });
            }

            return Ok(hoaDons);

        }
        // PUT api/hoadon/{id}/status/{HoaDonStatus}
        [HttpPut("updateHoaDonStatus")]
        public async Task<ActionResult<ResponseDto>> UpdateHoaDonStatus(Guid id, int HoaDonStatus)
        {
            // Kiểm tra nếu trạng thái hiện tại là "Đã giao hàng" hoặc "Đã hủy" thì không thực hiện cập nhật nữa
            if ((TrangThaiGiaoHang)HoaDonStatus >= TrangThaiGiaoHang.DaGiaoHang)
            {
                return BadRequest(new ResponseDto { IsSuccess = false, Message = "Đã quá trạng thái cập nhật." });
            }

            // Tăng trạng thái lên một bước, nếu không vượt quá trạng thái "Đã giao hàng"
            HoaDonStatus++; // Tăng trạng thái lên một

            if ((TrangThaiGiaoHang)HoaDonStatus > TrangThaiGiaoHang.DaGiaoHang)
            {
                // Nếu trạng thái đã vượt quá "Đã giao hàng", đặt lại thành "Đã giao hàng"
                HoaDonStatus = (int)TrangThaiGiaoHang.DaGiaoHang;
            }

            // Kiểm tra nếu trạng thái đang là "Chưa thanh toán" và muốn cập nhật lên "Đã giao hàng"
            if ((TrangThaiThanhToan)HoaDonStatus == TrangThaiThanhToan.Chuathanhtoan)
            {
                // Thì chuyển trạng thái thanh toán thành "Đã thanh toán"
                HoaDonStatus = (int)TrangThaiThanhToan.Dathanhtoan;
            }

            var result = await _db.UpdateHoaDonStatus(id, HoaDonStatus);

            if (result.IsSuccess)
            {
                // Trả về một phản hồi thành công nếu cập nhật thành công
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        [HttpGet("GetPurchaseHistory")]
        public async Task<ActionResult<List<HoaDon>>> GetCustomerPurchaseHistory(string customerId)
        {
            var purchaseHistory = await _db.GetCustomerPurchaseHistory(customerId);
            if (purchaseHistory == null || purchaseHistory.Count == 0)
            {
                return NotFound("Không có dữ liệu lịch sử mua hàng cho khách hàng này.");
            }
            return Ok(purchaseHistory);
        }
    }
}
