using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class HoaDonChoDTO
    {
        public Guid Id { get; set; }
        public Guid? IdNguoiDung { get; set; }
        public string? NguoiTao { get; set; }
        public Guid? IdVoucher { get; set; }
        public string? TenKhachHang { get; set; }
        public string? SDT { get; set; }
        public string? MaHoaDon { get; set; }
        public DateTime? NgayTao { get; set; }
        public int? TrangThaiGiaoHang { get; set; }
        public int? TrangThaiThanhToan { get; set; }
        public int? TienGiam { get; set; }
        public int? GiamGia { get; set; }
        public string? CodeVoucher { get; set; }
        public int? ThanhTien { get; set; }
        public string? Phuongthucthanhtoan { get; set; }

        public List<HoaDonChiTietDto> hoaDonChiTietDTOs { get; set; }
    }
}
