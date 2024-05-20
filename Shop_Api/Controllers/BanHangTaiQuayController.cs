using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Services.IServices;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanHangTaiQuayController : ControllerBase
    {
        private readonly IHoaDonServices _hoaDonServices;

        public BanHangTaiQuayController(IHoaDonServices hoaDonServices)
        {
            _hoaDonServices = hoaDonServices;
        }

        [HttpGet("GetAllHdTaiQuay")]
        public async Task<IActionResult> GetAllHdTaiQuay()
        {
            var result = _hoaDonServices.GetAllHDTaiQuay();
            if (result.Count == 0)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("CreateHdTaiQuay")]

        public async Task<IActionResult> CreateHdTaiQuay(RequestHDTaiQuay _requestHdTaiQuay)
        {
            var result = _hoaDonServices.TaoHoaDonTaiQuay(_requestHdTaiQuay);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPost("AddBillDetail")]

        public async Task<IActionResult> AddBillDetail(string mahoadon, string codeProductDetail, int? soluong)
        {
            var result = await _hoaDonServices.AddBillDetail(mahoadon, codeProductDetail, soluong);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateBillDetail")]

        public IActionResult UpdateBillDetail(string mahoadon, string codeProductDetail, int soluong)
        {
            var result = _hoaDonServices.CapNhatSoLuongHoaDonChiTietTaiQuay(mahoadon, codeProductDetail, soluong);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("TruQuantityBillDetail")]

        public IActionResult TruQuantityBillDetail(Guid idBillDetail)
        {
            var result = _hoaDonServices.TruQuantityBillDetail(idBillDetail).Result;
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("CongQuantityBillDetail")]

        public IActionResult CongQuantityBillDetail(Guid idBillDetail)
        {
            var result = _hoaDonServices.CongQuantityBillDetail(idBillDetail).Result;
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



        [HttpPut("ThanhToanTaiQuay")]

        public IActionResult ThanhToanTaiQuay(HoaDon _hoaDon)
        {
            var result = _hoaDonServices.ThanhToan(_hoaDon);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpDelete("XoaSanPhamKhoiHoaDon")]

        public IActionResult XoaSanPhamKhoiHoaDon(string maHD , string maSP)
        {
            var result = _hoaDonServices.XoaSanPhamKhoiHoaDon(maHD,maSP);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpPut("HuyHoaDon")]

        public IActionResult HuyHoaDon(string maHD, string lyDoHuy)
        {
            var result = _hoaDonServices.HuyHoaDonAsync(maHD, lyDoHuy).Result;
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



    }
}
