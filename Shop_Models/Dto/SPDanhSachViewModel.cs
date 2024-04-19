using Microsoft.AspNetCore.Mvc.Rendering;
using Shop_Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class SPDanhSachViewModel
    {
        public string? SumGuild { get; set; }
        public string? SanPham { get; set; }
        public string? ThuongHieu { get; set; }
        public string? LoaiMu { get; set; }
        public string? XuatXu { get; set; }
        public string? ChatLieu { get; set; }
        public int SoMau { get; set; }
        public List<SelectListItem>? LstMauSac { get; set; }
        public List<string>? LstMauSac2 { get; set; }
        public List<SelectListItem>? Id { get; set; }
        public string? LinkAnh { get; set; }
        public double? GiaBanLonNhat { get; set; }
        public double? GiaBanNhoNhat { get; set; }
        public int TongSoLuongTon { get; set; }
        public double TongSoLuongDaBan { get; set; }
    }
}
