using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("ChiTietSanPham")]
    public class ChiTietSanPham
    {
        [Key]
        public Guid Id { get; set; }
        public string? MaSanPham { get; set; }
        public double? GiaNhap { get; set; }
        public double? GiaBan { get; set; }
        public double? GiaThucTe { get; set; }
        public int? SoLuongTon { get; set; }
        public int? SoLuongDaBan { get; set; }
        public int? TrangThai { get; set; }
        //public int? TrangThaiKhuyenMai { get; set; }
        public Guid? SanPhamId { get; set; }
        public Guid? LoaiId { get; set; }
        public Guid? ThuongHieuId { get; set; }
        public Guid? XuatXuId { get; set; }
        public Guid? MauSacId { get; set; }
        public Guid? ChatLieuId { get; set; }

        public virtual SanPham? SanPham { get; set; }
        public virtual Loai? Loai { get; set; }
        public virtual ThuongHieu? ThuongHieu { get; set; }
        public virtual XuatXu? XuatXu { get; set; }
        public virtual MauSac? MauSac { get; set; }
        public virtual ChatLieu? ChatLieu { get; set; }
        public virtual ICollection<Anh>? Anhs { get; set; }
        public virtual ICollection<GioHangChiTiet>? GioHangChiTiets { get; set; }
        public virtual ICollection<HoaDonChiTiet>? HoaDonChiTiets { get; set; }
        public virtual ICollection<SanPhamYeuThich>? SanPhamYeuThich { get; set; }
        public virtual ICollection<ThongKe>? ThongKes { get; set; }
      
    }
}
