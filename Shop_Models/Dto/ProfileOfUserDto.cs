using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class ProfileOfUserDto
    {
        public string? TenTaiKhoan { get; set; }
        public string? TenNguoiDung { get; set; }
        public string? soDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }
        public bool? GioiTinh { get; set; }
    }
}
