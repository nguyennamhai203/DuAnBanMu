using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_API.Repository.IRepository;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly IGioHangChiTietRepository _repos;
        public GioHangController(IGioHangChiTietRepository repos)
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

    }
}
