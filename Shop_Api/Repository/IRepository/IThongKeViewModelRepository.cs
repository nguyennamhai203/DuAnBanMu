using Shop_Models.Dto;

namespace Shop_Api.Repository.IRepository
{
    public interface IThongKeViewModelRepository
    {
        public Task<List<ThongKeViewModel>> ThongKeSanPham();
        // thống kê theo ngày
        public Task<List<ThongKeViewModel>> ThongKeSanPhamTheoNgay(int ngay);
        // thống kê theo tháng
        public Task<List<ThongKeViewModel>> ThongKeSanPhamTheoThang(int thang);
        // // thống kê theo năm
        public Task<List<ThongKeViewModel>> ThongKeSanPhamTheoNam(int nam);
    }
}