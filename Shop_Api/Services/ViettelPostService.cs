using Newtonsoft.Json;
using Shop_Api.Services.IServices;
using System.Net.Http.Headers;
using System.Text;

namespace Shop_Api.Services
{
    public class ViettelPostService : IViettelPostService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public ViettelPostService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<decimal> CalculateShippingFeeAsync(string fromProvince, string toProvince, decimal weight, decimal value)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee");

            var requestBody = new
            {
                // Populate request parameters according to Viettel Post API documentation
                from_province = fromProvince,
                to_province = toProvince,
                weight,
                value
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["ViettelPost:ApiKey"]);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseContent);

                // Adjust according to actual response structure
                return result.data.total_fee;
            }
            else
            {
                throw new Exception("Error fetching shipping fee from Viettel Post.");
            }
        }
    }
}
