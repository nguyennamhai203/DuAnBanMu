using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("Khuyenmai")]
    public class Khuyenmai
    {
        [Key]
        public Guid Id { get; set; }
        public string? MaKhuyenMai { get; set; }
        public string? TenKhuyenMai { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? LoaiHinhKhuyenMai { get; set; }
        public double? MucGiam { get; set; }
        public int? TrangThai { get; set; }
        public virtual ICollection<ChiTietKhuyenMai>? ChiTietKhuyenMais { get; set; }
    }
}
