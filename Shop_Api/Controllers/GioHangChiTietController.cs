using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangChiTietController : ControllerBase
    {
        private readonly IGioHangChiTietRepository _repos;
        public GioHangChiTietController(IGioHangChiTietRepository repos)
        {
            _repos = repos;
        }

        //[HttpPost("AddCart")]
        //public async Task<IActionResult> AddCart()
        //{

        //}

        [HttpGet("GetCartJoinForUser")]
        public async Task<IActionResult> GetCartJoinForUser(string userName)
        {
            //var (userId, userName) = TokenUtility.GetTokenInfor(Request);
            var reponse = await _repos.GetCartJoinForUser(userName);
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            else
            {
                return BadRequest(reponse);
            }
        }

        [HttpPost("AddProductToCart")]
        public async Task<IActionResult> AddProductToCart(GioHangChiTiet gioHangChiTiet)
        {
            var reponse = await _repos.CreateAsync(gioHangChiTiet);
            if (reponse==true)
            {
                return Ok(reponse);
            }
            else
            {
                return BadRequest(reponse);
            }

        }


        [HttpPut("UpdateProductToCart")]
        public async Task<IActionResult> UpdateProductToCart(GioHangChiTiet gioHangChiTiet)
        {
            var reponse = await _repos.Updatesync(gioHangChiTiet);
            if (reponse == true)
            {
                return Ok(reponse);
            }
            else
            {
                return BadRequest(reponse);
            }

        }
        
        [HttpDelete("DeleteProductToCart")]
        public async Task<IActionResult> DeleteProductToCart(Guid Id)
        {
            var reponse = await _repos.Deletesync(Id);
            if (reponse == true)
            {
                return Ok(reponse);
            }
            else
            {
                return BadRequest(reponse);
            }

        }



    }
}
