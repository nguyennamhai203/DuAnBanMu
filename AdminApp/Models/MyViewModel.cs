using Shop_Models.Entities;

namespace AdminApp.Models
{
    public class MyViewModel
    {
        public IEnumerable<ChiTietSanPham> ChiTietSanPhams { get; set; }
        public IEnumerable<HoaDon> HoaDons { get; set; }
    }
}