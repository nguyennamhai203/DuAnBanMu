using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface IChiTietSanPhamRepository
    {
        public Task<ResponseDto> CreateAsync(ChiTietSanPham model);
        public Task<ResponseDto> UpdateAsync(ChiTietSanPham model);
        public Task<ResponseDto> UpdateAsync2(ChiTietSanPham model);
        public Task<ResponseDto> DeleteAsync(Guid Id);
        public Task<List<ChiTietSanPham>> GetAsync();
        public Task<List<SanPhamChiTietDto>> GetAllAsync(int? status/*, int page = 1*/);
        public Task<List<ChiTietSanPham>> GetAllAsync2(int? status, int page);
        public Task<List<SanPhamChiTietDto>> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? tenSanPham, double? from, double? to, string? sortBy, int? page, string? tenLoai, string? tenThuongHieu, string? tenMauSac, string? tenXuatXu, string? chatLieu,int?PageSize);
        public Task<List<SPDanhSachViewModel>> GetFilteredDaTaDSTongQuanAynsc(ParametersTongQuanDanhSach parametersTongQuanDanhSach);
        public Task<List<SPDanhSachViewModel>> GetItemShopViewModelAsync(string sumguid);
        public Task<List<ItemShopViewModel>> GetItemShopViewModelAsync2(/*string? sumguid*/);
        public  Task<ItemDetailViewModel?> GetItemDetailViewModelAynsc(string id);
        public  Task<ItemDetailViewModel?> GetItemDetailViewModelWhenSelectColorAynsc(string id,string color);
        //public Task<(List<SPDanhSachViewModel>, int)> GetFilteredDataDSTongQuanAsync(ParametersTongQuanDanhSach parametersTongQuanDanhSach);
        public List<SanPhamChiTietDto> GetRelatedProducts(string sumGuid);
        public Task<SanPhamChiTietDto> DetailSanPhamChiTietDto(Guid Id);



    }
}