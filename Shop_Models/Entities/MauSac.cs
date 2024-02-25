using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("MauSac")]
    public class MauSac
    {
        [Key]
        public Guid Guid { get; set; }
        public string? MaMauSac { get; set; }
        public string? TenMauSac { get; set; }
        public int TrangThai { get; set; }
        public virtual ICollection<ChiTietSanPham>? ChiTietSanPhams { get; set; }
    }
}
