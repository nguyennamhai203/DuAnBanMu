using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class HoaDonChiTietDto
    {
      
        public Guid Id { get; set; }
        public Guid HoaDonId { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public string? CodeProductDetail { get; set; }
        public string? NameProductDetail { get; set; }
        public Guid SanPhamChiTietId { get; set; }
        public double Price { get; set; }
        public double PriceBan { get; set; }
        public string? Description { get; set; }
        public string? NameColor { get; set; }
        public string? NameProduct { get; set; }
        public string? NameManufacturer { get; set; }
       
    }
}
