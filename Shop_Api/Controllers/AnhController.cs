using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Api.Repository;
using Shop_Models.Entities;
using Shop_Models.Dto;
using System;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Entity;
using Shop_Api.AppDbContext;

namespace Shop_Api.Controllers
{
    
    public class AnhController : ControllerBase
    {
        private readonly IAnhRepository db2;
		private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;

		public AnhController(IAnhRepository _db1, IWebHostEnvironment hostingEnvironment, ApplicationDbContext dbContext)
		{
			db2 = _db1;
			_hostingEnvironment = hostingEnvironment;
            _context = dbContext;
		}
		[HttpGet("Get-All-Anh")]
        public IEnumerable<Anh> GetAllAnh()
        {
            
                return  db2.GetAll();
            
        }
        [HttpPost("Create-Anh")]
        public async Task<IActionResult> CreateAnh( string MaAnh, string Url)
        {
            var obj = new Anh();
            obj.Guid = new Guid();
            obj.MaAnh=MaAnh;
            obj.URL = Url;  
            
            if ( MaAnh == null || Url == null)
            {
                return BadRequest("Du lieu them bi trong");
            }
            try
                
            {
               await db2.CreateAnh(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Update-Anh")]
        public async Task<IActionResult> UpdateAnh(Guid id, string MaAnh, string Url)
        {
            var obj = await db2.GetAnhById(id);
            obj.MaAnh = MaAnh;
            obj.URL = Url;
            try
            {
                await db2.UpdateAnh(id,obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete-Anh")]
        public async Task<IActionResult> DeleteAnh(Guid id)
        {
            
           var a= await db2.DeleteAnh(id);
            return Ok(a);
        }


		[HttpPost]
		[Route("uploadManyProductDetailImages")] // Test
		public async Task<IActionResult> UploadImages([FromForm] List<IFormFile> files, [FromForm] Guid ProductId)
		{
			var objectFolder = "product_images";
			//var ProductId = Guid.Empty;
			try
			{
				var uploadedFiles = new List<string>();
				var spct = await _context.ChiTietSanPhams.FindAsync(ProductId);

				if (spct != null)
				{
					int i = 1;
					foreach (var file in files)
					{
						if (file != null && file.Length > 0)
						{
							string basePath = _hostingEnvironment.WebRootPath;
							// Tạo tên tệp duy nhất bằng cách sử dụng Guid và đuôi mở rộng
							string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
							//string filePath = Path.Combine(basePath, fileName);



							string filePath = Path.Combine(basePath, objectFolder, fileName);


							using (var stream = new FileStream(filePath, FileMode.Create))
							{
								await file.CopyToAsync(stream);
							}

							uploadedFiles.Add(fileName);



							var image = new Anh
							{
								Guid = Guid.NewGuid(),
								MaAnh = "Anh" + i++,
								URL = $"/{objectFolder}/{fileName}",
								ChiTietSanPhamId = (Guid)ProductId,
								//Status = 1

							};

							// Lưu ảnh vào cơ sở dữ liệu ở đây (sử dụng dịch vụ _imageUploadService)
							await db2.CreateAnh(image);

						}
					}
				}

				return Ok(new { Message = "Tải lên thành công", Files = uploadedFiles });
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi: {ex.Message}");
			}
		}


	}
}
