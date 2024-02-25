using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("HoaDonChiTiet")]
    public class HoaDonChiTiet
    {

        [Key]
        public Guid Guid { get; set; }
        public Guid ChiTietSanPhamId { get; set; }
        public int SoLuong { get; set; }
        public double GiaBan { get; set; }
        public virtual HoaDon? HoaDon { get; set; }
        public virtual ChiTietSanPham? ChiTietSanPham { get; set; }
    }
}
