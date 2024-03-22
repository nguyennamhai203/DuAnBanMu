using Shop_Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
	public class SanPhamChiTietDto
	{
		[Key]
		public Guid Id { get; set; }
		public string? MaSanPhamChiTiet { get; set; }
		public double? GiaNhap { get; set; }
		public double? GiaBan { get; set; }
		public double? GiaThucTe { get; set; }
		public int? SoLuongTon { get; set; }
		public int? SoLuongDaBan { get; set; }
		public int? TrangThai { get; set; }
		public bool? TrangThaiKhuyenMai { get; set; }
		//public string? Mota { get; set; }

		public Guid? SanPhamId { get; set; }
		public string? MaSanPham {get; set; }
		public string? TenSanPham {get; set; }

		public Guid? LoaiId { get; set; }
		public string? MaLoai { get; set; }
		public string? TenLoai { get; set; }

		public Guid? ThuongHieuId { get; set; }
		public string? MaThuongHieu { get; set; }
		public string? TenThuongHieu { get; set; }

		public Guid? XuatXuId { get; set; }
		public string? MaXuatXu { get; set; }
		public string? TenXuatXu { get; set; }

		public Guid? MauSacId { get; set; }
		public string? MaMauSac { get; set; }
		public string? TenMauSac { get; set; }

		public Guid? ChatLieuId { get; set; }
		public string? MaChatLieu { get; set; }
		public string? TenChatLieu { get; set; }

		public string? AnhThuNhat { get; set; }
		public List<string>? OtherImages { get; set; }
	}
}
