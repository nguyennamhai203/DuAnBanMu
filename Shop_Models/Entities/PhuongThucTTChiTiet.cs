using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("PhuongThucTTChiTiet")]
    public class PhuongThucTTChiTiet
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HoaDonId { get; set; }
        public Guid PTTToanId { get; set; }
        public double? SoTien { get; set; }
        public int TrangThai { get; set; }
        public virtual PhuongThucThanhToan? PhuongThucThanhToan { get; set; }
        public virtual HoaDon? HoaDon { get; set; }

    }
}
