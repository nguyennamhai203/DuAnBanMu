using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Shop_Api.Repository.IRepository;
using Shop_Models.Entities;
using Shop_Models.Heplers;
using Shop_Models.ViewModels.ChatLieu;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatLieuController : ControllerBase
    {
        private readonly IChatLieuRepository _repository;

        public ChatLieuController(IChatLieuRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int? status, int page = 1)
        {
            var result = await _repository.GetAsync(status, page);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAllAsync();
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(result, settings);
            JToken parsedJson = JToken.Parse(json);
            string formattedJson = parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);
            return Ok(formattedJson);
        }
        [HttpGet("GetById/{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return Ok(result);
        }

        /*[Authorize(Roles = AppRole.Admin)]*/
        [HttpPost("post-chatlieu")]
        public async Task<IActionResult> PostAsync(CreateChatLieu obj)
        {
            if (obj == null)
            {
                return BadRequest("Du lieu them bi trong");
            }
            try
            {
                ChatLieu chatLieu = new ChatLieu();
                chatLieu.Guid = Guid.NewGuid();
                chatLieu.MaChatLieu = obj.MaChatLieu;
                chatLieu.TenChatLieu = obj.TenChatLieu;
                chatLieu.TrangThai = 1;
                await _repository.CreateAsync(chatLieu);
                return CreatedAtAction("Get", "ChatLieu", chatLieu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /*[Authorize(Roles = AppRole.Admin)]*/
        [HttpPut("Put/{id:Guid}")]
        public async Task<IActionResult> PutAsync(ChatLieu obj, Guid id)
        {
            var respon = await _repository.UpdateAsync(obj, id);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        /*[Authorize(Roles = AppRole.Admin)]*/
        [HttpDelete("Delete/{id:Guid}")]
        public async Task<IActionResult> DeleteChatLieuAsync(Guid id)
        {
            var respon = await _repository.DeleteAsync(id);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }
    }
}
