using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("Loai")]
    public class Loai
    {
        [Key]
        public Guid Id { get; set; }
        public string? MaLoai { get; set; }
        public string? TenLoai { get; set; }
        public int TrangThai { get; set; }
        public virtual ICollection<ChiTietSanPham>? ChiTietSanPhams { get; set; }
    }
}
