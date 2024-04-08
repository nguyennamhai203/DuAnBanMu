using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class ParametersTongQuanDanhSach
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string? SearchValue { get; set; }
        public string? SumGuid { get; set; }
        public Guid? IdSanPham { get; set; }
        public Guid? IdThuongHieu { get; set; }
        public Guid? IdChatLieu { get; set; }
        public Guid? IdLoaiMu { get; set; }
        public Guid? IdXuatXu { get; set; }
    }
}
