using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("ChiTietKhuyenMai")]
    public class ChiTietKhuyenMai
    {
        [Key]
        public Guid Id { get; set; }
        public Guid KhuyenMaiId { get; set; }
        public Guid ChiTietSanPhamId { get; set; }
        public string? Mota { get; set; }
        public int TrangThai { get; set; }
        public virtual Khuyenmai? Khuyenmai { get; set; }
    }
}
