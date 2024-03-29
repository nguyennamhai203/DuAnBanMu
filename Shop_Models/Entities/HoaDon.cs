using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? VoucherId { get; set; }
        public string? MaHoaDon { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public DateTime NgayShip { get; set; }
        public DateTime NgayNhan { get; set; }
        public string? MoTa { get; set; }
        public double TienGiam { get; set; }
        public double TienShip { get; set; }
        public double TongTien { get; set; }
        public int TrangThaiThanhToan { get; set; }
        public int TrangThaiGiaoHang { get; set; }
        public DateTime NgayGiaoDuKien { get; set; }
        public string? LiDoHuy { get; set; }
        public virtual Voucher? Vouchers { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public virtual ICollection<PhuongThucTTChiTiet>? PhuongThucTTChiTiet { get; set; }
        public virtual ICollection<ThongKe>? ThongKes { get; set; }

    }
}
