using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IChiTietSanPhamRepository
    {
        public Task<ResponseDto> CreateAsync(ChiTietSanPham model);
        public Task<ResponseDto> UpdateAsync(ChiTietSanPham model);
        public Task<ResponseDto> DeleteAsync(Guid Id);
        public Task<List<ChiTietSanPham>> GetAsync();
        public Task<List<SanPhamChiTietDto>> GetAsync(int? status/*, int page = 1*/);
        public Task<List<SanPhamChiTietDto>> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? tenSanPham, double? from, double? to, string? sortBy, int? page, string? tenLoai, string? tenThuongHieu, string? tenMauSac, string? tenXuatXu, string? chatLieu,int?PageSize);

        public Task<List<SPDanhSachViewModel>> GetFilteredDaTaDSTongQuanAynsc(ParametersTongQuanDanhSach parametersTongQuanDanhSach);


    }
}
