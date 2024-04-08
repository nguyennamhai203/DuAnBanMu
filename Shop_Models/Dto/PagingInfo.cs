using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class PagingInfo
    {
        public int TongSoItem { get; set; }
        public int SoItemTrenMotTrang { get; set; } = 12;
        public int TrangHienTai { get; set; } = 1;
        public int SoItemTrenTrangHienTai { get; set; }
        public int SoTrang => (int)Math.Ceiling((double)TongSoItem / SoItemTrenMotTrang);
    }
}
