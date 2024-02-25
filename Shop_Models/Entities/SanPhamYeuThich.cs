using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("SanPhamYeuThich")]
    public class SanPhamYeuThich
    {
        [Key]
        public Guid Id { get; set; }
        public Guid NguoiDungId { get; set; }
        public Guid ChiTietSanPhamId { get; set; }
        public int TrangThai { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public virtual ChiTietSanPham? ChiTietSanPham { get; set; }
    }
}
