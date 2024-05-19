using Microsoft.AspNetCore.Mvc;
using Shop_Api.Services.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IViettelPostService _viettelPostService;

        public ShippingController(IViettelPostService viettelPostService)
        {
            _viettelPostService = viettelPostService;
        }

        [HttpPost]
        public async Task<IActionResult> CalculateShippingFee(string fromProvince, string toProvince, decimal weight, decimal value)
        {
            try
            {
                var fee = await _viettelPostService.CalculateShippingFeeAsync(fromProvince, toProvince, weight, value);
                return Ok(new { fee });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
