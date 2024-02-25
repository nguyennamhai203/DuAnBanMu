using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("Voucher")]
    public class Voucher
    {
        [Key]
        public Guid Guid { get; set; }
        public string? MaVoucher { get; set; }
        public string? TenVoucher { get; set; }
        public int? PhanTramGiam { get; set; }
        public int? SoLuong { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public int? TrangThai { get; set; }
        public virtual ICollection<HoaDon>? HoaDon { get; set; }
    }
}
