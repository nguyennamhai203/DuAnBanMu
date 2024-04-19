using Shop_Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class ThongKeViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public double GiaBan { get; set; }
        public double GiaNhap { get; set; }
        public int SoLuongTon { get; set; }
        public int SoLuongDaBan { get; set; }
        public string MaHoaDon { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public DateTime NgayShip { get; set; }
        public DateTime NgayNhan { get; set; }
        public int NgayThongKe { get; set; }
        public int ThangThongKe { get; set; }
        public int NamThongKe { get; set; }
        public double TienGiam { get; set; }
        public double TienShip { get; set; }
        public double TongTienThongKe { get; set; }
        public double PhanTramLoiNhuan { get; set; }
        public double PhanTramLo { get; set; }
        /*public ThongKeViewModel(SanPham sanPham, ChiTietSanPham chiTietSanPham, HoaDon hoaDon, ThongKe thongKe)
        {
            MaSanPham = sanPham.MaSanPham;
            TenSanPham = sanPham.TenSanPham;
            GiaNhap = chiTietSanPham.GiaNhap ?? 0;
            GiaBan = chiTietSanPham.GiaBan ?? 0;
            SoLuongTon = chiTietSanPham.SoLuongTon ?? 0;
            SoLuongDaBan = chiTietSanPham.SoLuongDaBan ?? 0;
            MaHoaDon = hoaDon.MaHoaDon;
            NgayTao = hoaDon.NgayTao;
            NgayThanhToan = hoaDon.NgayThanhToan;
            NgayShip = hoaDon.NgayShip;
            NgayNhan = hoaDon.NgayNhan;
            TienGiam = hoaDon.TienGiam;
            TienShip = hoaDon.TienShip;
            NgayThongKe = hoaDon.NgayThanhToan.Day;
            ThangThongKe = thongKe.Thang;
            NamThongKe = thongKe.Nam;

            // Tính tổng tiền hóa đơn từ thông tin trong hóa đơn, không tính từ chi tiết sản phẩm
            //hoaDon.TongTien = Convert.ToDouble(chiTietSanPham.GiaBan) * Convert.ToInt32(chiTietSanPham.SoLuongDaBan);
            TongTienThongKe = hoaDon.TongTien;

            // Tính phần trăm lợi nhuận
            double loiNhuan = (GiaBan - GiaNhap) * SoLuongDaBan; // Tính toán lợi nhuận từ mỗi mặt hàng
            double tongTienHoaDon = TongTienThongKe; // Tổng tiền của hóa đơn
            PhanTramLoiNhuan = tongTienHoaDon != 0 ? loiNhuan / tongTienHoaDon * 100 : 0;

            // Tính phần trăm lỗ
            double lo = (GiaNhap - GiaBan) * SoLuongDaBan; // Tính toán lỗ từ mỗi mặt hàng
            double tongTienHoaDon2 = TongTienThongKe; // Tổng tiền của hóa đơn
            PhanTramLo = tongTienHoaDon2 != 0 ? lo / tongTienHoaDon2 * 100 : 0;
        }*/

    }
}