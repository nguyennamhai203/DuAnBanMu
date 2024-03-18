﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using Shop_Models.Heplers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<IActionResult> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? tenSanPham, double? from, double? to, string? sortBy, int? page, string? tenLoai, string? tenThuongHieu, string? tenMauSac, string? tenXuatXu, string? chatLieu, int PageSize)
        {
            var result = await _repository.PGetProductDetail(getNumber, codeProductDetail, status, tenSanPham, from, to, sortBy, page, tenLoai, tenThuongHieu, tenMauSac, tenXuatXu, chatLieu, PageSize);
            return Ok(result);
        }

        [Authorize(Roles =AppRole.Admin)]
        [HttpPost("GetFilteredDaTaDSTongQuanAynsc")]
        public async Task<IActionResult> GetFilteredDaTaDSTongQuanAynsc(ParametersTongQuanDanhSach parameters)
        {
            try
            {
                var viewModelResult = await _repository.GetFilteredDaTaDSTongQuanAynsc(parameters);
                //var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(viewModelResult.Count / (double)parameters.Length);
                var paginatedResult = viewModelResult
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .ToList();
                var result = new ResponseDto()
                {
                    Content = paginatedResult,
                    Count = viewModelResult.Count,
                    TotalPage = totalPages
                };
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("Get-List-RelatedProduct")]
        public List<SanPhamChiTietDto> GetRelatedProducts(string sumGuild)
        {
            return _repository.GetRelatedProducts(sumGuild);
        }

        [HttpGet("DetailSanPhamChiTietDto")]
        public async Task<IActionResult> DetailSanPhamChiTietDto(Guid Id)
        {
            var reuslt = await _repository.DetailSanPhamChiTietDto(Id);
            return Ok(reuslt);
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
