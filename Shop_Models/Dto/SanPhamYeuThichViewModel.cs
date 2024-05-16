using Shop_Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class SanPhamYeuThichViewModel
    {
        [Key]
        public Guid Id { get; set; }
        //Nguoi dung
        public string? MaNguoiDung { get; set; }
        public int TrangThaiND { get; set; }
        public string? TenNguoiDung { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public bool? GioiTinh { get; set; }
        public string? VerificationCode { get; set; }
        public DateTime VerificationCodeExpiry { get; set; }
        //Anh
        public Guid ChiTietSanPhamId { get; set; }
        public string? MaAnh { get; set; }
        public string? AnhSanPham { get; set; }
        public string? URL { get; set; }
        //ChiTietSanPham
        public string? MaSanPhamCT { get; set; }
        public double? GiaNhap { get; set; }
        public double? GiaBan { get; set; }
        public double? GiaThucTe { get; set; }
        public int? SoLuongTon { get; set; }
        public int? SoLuongDaBan { get; set; }
        public int? TrangThaiCTSP { get; set; }
        public string? Mota { get; set; }
        public int? TrangThaiKhuyenMai { get; set; }
        //SanPham
        public string? MaSanPham { get; set; }
        public string? TenSanPham { get; set; }
        public int TrangThai { get; set; }
    }
}