using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class RequestHDTaiQuay
    {
        public Guid? IdNguoiDung { get; set; }
        public string? TenNhanVien {get; set;}
        public DateTime? NgayTao {get; set;}
        public string? TenKhachHang {get; set;}
        public string? SoDienThoai {get; set;}
        public double TongTienHang {get; set;}
        public Guid? VoucherId {get;set;}
        public string? MaVoucher {get;set;}
        public double? TongThanhToan {get;set;}
        public List<GioHangChiTietViewModel>? CartItem { get; set; }

    }
}
    