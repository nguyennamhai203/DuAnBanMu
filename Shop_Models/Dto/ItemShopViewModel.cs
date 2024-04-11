using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class ItemShopViewModel
    {
        public string? IdChiTietSp { get; set; }
        public string? MaSanPham { get; set; }
        public string? ThuongHieu { get; set; }
        public string? MauSac { get; set; }
        public string? TheLoai { get; set; }
        public string? TenSanPham { get; set; }
        public double? GiaMin { get; set; }
        public double? GiaBan { get; set; }
        public int? SoLuongTon { get; set; }
        public double? GiaGoc { get; set; }
        public double? GiaMax { get; set; }
        public string? MoTaSanPham { get; set; }
        public double? GiaKhuyenMai { get; set; }
        public List<SelectListItem>? LstMauSac { get; set; }
        public string? Anh { get; set; }
        public int? IsKhuyenMai { get; set; }
    }
}
