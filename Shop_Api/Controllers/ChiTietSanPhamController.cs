﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Entities;
using Shop_Models.Heplers;

namespace Shop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietSanPhamController : ControllerBase
    {
        private readonly IChiTietSanPhamRepository _repository;

        public ChiTietSanPhamController(IChiTietSanPhamRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int? status/*, int page = 1*/)
        {
            var result = await _repository.GetAsync(/*status*//*, page*/);
            return Ok(result);
        }
        
        [HttpGet("PGetProductDetail")]
        public async Task<IActionResult> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? tenSanPham, double? from, double? to, string? sortBy, int? page, string? tenLoai, string? tenThuongHieu, string? tenMauSac, string? tenXuatXu, string? chatLieu)
        {
            var result =  _repository.PGetProductDetail(getNumber,codeProductDetail,status,tenSanPham,from,to,sortBy,page, tenLoai,tenThuongHieu,tenMauSac,tenXuatXu, chatLieu).Result;
            return Ok(result);
        }

        [Authorize(Roles = AppRole.Admin)]
        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync(ChiTietSanPham obj)
        {
            var respon = await _repository.CreateAsync(obj);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }


        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(ChiTietSanPham obj)
        {
            var respon = await _repository.UpdateAsync(obj);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }

        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var respon = await _repository.DeleteAsync(Id);
            if (respon.IsSuccess == true)
            {
                return Ok(respon);
            }
            else return BadRequest(respon);
        }
    }
}
