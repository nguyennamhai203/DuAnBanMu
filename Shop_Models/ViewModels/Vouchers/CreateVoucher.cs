namespace Shop_Models.ViewModels.Vouchers
{
    public class CreateVoucher
    {
        public string? MaVoucher { get; set; }
        public string? TenVoucher { get; set; }
        public int? PhanTramGiam { get; set; }
        public int? SoLuong { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public int? TrangThai { get; set; }
    }
}
