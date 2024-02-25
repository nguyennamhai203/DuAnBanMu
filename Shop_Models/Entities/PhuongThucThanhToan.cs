using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("PhuongThucThanhToan")]
    public class PhuongThucThanhToan
    {
        [Key]
        public Guid Id { get; set; }
        public string? MaPTThanhToan {get; set; }
        public string? TenMaPTThanhToan {get; set; }
        public string? MoTa {get; set; }
        public int TrangThai {get; set; }
        public virtual ICollection<PhuongThucTTChiTiet>? PhuongThucTTChiTiets {get; set; }

    }
}
