using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class RequestBillDto
    {
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Usename { get; set; }
        public string? CodeVoucher { get; set; }
        public int Payment { get; set; }
        public int phiship2 { get; set; }
        public string? MaPTTT { get; set; }
        public int? trangthaithanhtoan { get; set; }
        public string? MaHoaDon { get; set; }
        public PaymentMethod PaymentMethod { get; set; } // Thuộc tính mới để lưu trữ PTTT
        public List<GioHangChiTietViewModel>? CartItem { get; set; }
    }
    public enum PaymentMethod
    {
        ThanhToanTaiCuaHang = 1,
        ThanhToanKhiNhanHang = 2,
        ChuyenKhoanNganHang = 3,
        VNPay = 4

    }
}

