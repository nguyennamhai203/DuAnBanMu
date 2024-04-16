using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class ParameterGianHang
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? Total { get; set; }
        public int? TotalPages { get; set; }
        public string? SortBy { get; set; }
        public string? TenSanPham { get; set; }
        public string? TenLoai { get; set; }
        public string? TenThuongHieu { get; set; }
        public string? TenMauSac { get; set; }
        public string? TenXuatXu { get; set; }
        public string? TenChatLieu { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
        


    }
}
