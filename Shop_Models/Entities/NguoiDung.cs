using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shop_Models.Entities
{
    [Table("NguoiDung")]
    public class NguoiDung : IdentityUser<Guid>
    {
        public string? MaNguoiDung { get; set; }
        //public string? Password { get; set; } 
        public int TrangThai { get; set; }
        public string? TenNguoiDung { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public bool? GioiTinh { get; set; }


    }
}
