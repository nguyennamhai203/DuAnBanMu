using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class GioHangChiTietViewModel
    {
        public Guid Id { get; set; }
        public Guid GioHangId { get; set; }
        public Guid ChiTietSanPhamId { get; set; }
        public string? MaSPCT { get; set; }
        public string? TenSanPhamChiTiet { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongBanSanPham { get; set; }
        public double GiaBan { get; set; } /*int*/
        public double GiaGoc { get; set; }/*int*/
        public int TrangThai { get; set; }/*int*/
    }
}
