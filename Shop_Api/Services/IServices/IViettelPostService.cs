namespace Shop_Api.Services.IServices
{
    public interface IViettelPostService
    {
        public Task<decimal> CalculateShippingFeeAsync(string fromProvince, string toProvince, decimal weight, decimal value);
    }
}
