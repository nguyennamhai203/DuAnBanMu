using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class HoaDonDto
    {

        public string? InvoiceCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? CodeVoucher { get; set; }
        public double? GiamGia { get; set; }
        public int Payment { get; set; }
        public string? Phuongthucthanhtoan { get; set; }
        public int TienShip { get; set; }
        public int TienGiam { get; set; }
        public int ThanhTien { get; set; }
        public bool IsPayment { get; set; }
        public Guid? UserId { get; set; }
        public string? TenKhachHang { get; set; }
        public int TrangThaiGiaoHang { get; set; }
        public int TrangThaiThanhToan { get; set; }
        public object? BillDetail { get; set; }
        public int Count { get; set; } = 0;
    }
}
