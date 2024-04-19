using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class ItemDetailViewModel: ItemShopViewModel
    {
        public int? SoLuongDaBan { get; set; }
        public int? SoLuotYeuThich { get; set; }
        public List<string>? itemShopListColor { get; set; }
        public List<string>? DanhSachMau { get; set; }
        public int IsYeuThich { get; set; }
        public string? XuatXu { get; set; }
        public string? LoaiMu { get; set; }
        public string? ChatLieu { get; set; }

    }
}
