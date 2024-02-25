using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("XuatXu")]
    public class XuatXu
    {
        [Key]
        public Guid Guid { get; set; }
        public string? MaXuatXu { get; set; }
        public string? TenXuatXu { get; set; }
        public int TrangThai { get; set; }
        public virtual ICollection<ChiTietSanPham>? ChiTietSanPhams { get; set; }
    }
}
