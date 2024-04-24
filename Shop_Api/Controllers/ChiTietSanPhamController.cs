﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using Shop_Models.Heplers;
using System.Linq;
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

        [HttpGet("GetAllDto")]
        public async Task<IActionResult> GetAll(int? status/*, int page = 1*/)
        {
            var result = await _repository.GetAllAsync(status);
            return Ok(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.GetAsync();
            return Ok(result);
        }

        [HttpGet("PGetProductDetail")]
        public async Task<IActionResult> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? tenSanPham, double? from, double? to, string? sortBy, int? page, string? tenLoai, string? tenThuongHieu, string? tenMauSac, string? tenXuatXu, string? chatLieu, int PageSize)
        {
            var result = await _repository.PGetProductDetail(getNumber, codeProductDetail, status, tenSanPham, from, to, sortBy, page, tenLoai, tenThuongHieu, tenMauSac, tenXuatXu, chatLieu, PageSize);
            return Ok(result);
        }

        //[Authorize(Roles = AppRole.Admin)]
        [HttpPost("GetFilteredDaTaDSTongQuanAynsc")]
        public async Task<IActionResult> GetFilteredDaTaDSTongQuanAynsc(ParametersTongQuanDanhSach parameters)
        {
            try
            {
                var viewModelResult = await _repository.GetFilteredDaTaDSTongQuanAynsc(parameters);
                //var totalItems = await query.CountAsync();
                if (parameters.SumGuid != null)
                {
                    var totalPages = (int)Math.Ceiling(viewModelResult.Count / (double)parameters.Length);
                    var paginatedResult = viewModelResult
                        .Skip(parameters.Start)
                        .Take(parameters.Length)
                        .ToList();
                    var result = new ResponseDto()
                    {
                        Content = viewModelResult,
                        Count = viewModelResult.Count,
                        TotalPage = totalPages
                    };
                    return new ObjectResult(result);
                }
                else
                {
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpPost("GetFilteredDaTaDSKhuyenMaiAynsc")]
        public async Task<IActionResult> GetFilteredDaTaDSKhuyenMaiAynsc(ParametersTongQuanDanhSach parameters)
        {
            try
            {
                var viewModelResult = await _repository.GetFilteredDaTaDSTongQuanAynsc(parameters);
                //var totalItems = await query.CountAsync();
                if (parameters.SumGuid != null)
                {
                    var totalPages = (int)Math.Ceiling(viewModelResult.Count / (double)parameters.Length);
                    var paginatedResult = viewModelResult
                        .Skip(parameters.Start)
                        .Take(parameters.Length)
                        .ToList();
                    var result = new ResponseDto()
                    {
                        Content = viewModelResult,
                        Count = viewModelResult.Count,
                        TotalPage = totalPages
                    };
                    return new ObjectResult(result);
                }
                else
                {
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

        [HttpGet("GetItemShopViewModelAsync")]
        public async Task<IActionResult> GetItemShopViewModelAsync(string Id)
        {
            var viewModelResult = await _repository.GetItemShopViewModelAsync(Id);
            var result = new ResponseDto()
            {
                Content = viewModelResult,


            };
            return new ObjectResult(result);
        }
        [HttpGet("GetItemShopViewModelAsync2")]
        public async Task<IActionResult> GetItemShopViewModelAsync2(/*string? Id,*/ int pageNumber = 1)
        {
            int itemsPerPage = 12;
            var viewModelResult = await _repository.GetItemShopViewModelAsync2(/*Id*/);
            var pagedViewModelResult = viewModelResult.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            var result = new ResponseDto()
            {
                Content = viewModelResult,
                //Content = pagedViewModelResult,
                PagingInfo = new PagingInfo()
                {
                    TongSoItem = viewModelResult.Count(),
                    SoItemTrenMotTrang = itemsPerPage,
                    SoItemTrenTrangHienTai = pagedViewModelResult.Count(),
                    TrangHienTai = pageNumber,
                }
            };
            return new ObjectResult(result);
        }

        [HttpPost("GetItemShopGianHang")]
        public async Task<IActionResult> GetItemShopGianHang(ParameterGianHang parameter)
        {
            parameter.Page ??= 1;
            parameter.PageSize ??= 6;
            var viewModelResult = _repository.ParameterGianHang(parameter);
            var pagedViewModelResult = viewModelResult.Skip((int)((parameter.Page - 1) * parameter.PageSize)).Take((int)parameter.PageSize).ToList();

            var result = new ResponseDto()
            {
                Content = pagedViewModelResult,
                PagingInfo = new PagingInfo()
                {
                    TongSoItem = viewModelResult.Count(),
                    SoItemTrenMotTrang = (int)parameter.PageSize,
                    SoItemTrenTrangHienTai = pagedViewModelResult.Count(),
                    TrangHienTai = (int)parameter.Page,
                }
            };

            if (result.PagingInfo.TrangHienTai > result.PagingInfo.SoTrang)
            {
                result.Content = "Khong co ban ghi nao hop le";
            }
            else if (result.Content==null)
            {
                result.Content = "Khong co ban ghi nao hop le";
            }
            return new ObjectResult(result);
        }

        [HttpGet("GetItemDetailViewModelAynsc")]
        public async Task<IActionResult> GetItemDetailViewModelAynsc(string Id)
        {
            var viewModelResult = await _repository.GetItemDetailViewModelAynsc(Id);
            var result = new ResponseDto()
            {
                Content = viewModelResult,


            };
            return new ObjectResult(result);
        }

        [HttpGet("Get-ItemDetailViewModel/{id}/{mauSac}")]
        public async Task<IActionResult?> GetItemDetailViewModelWhenSelectColor(string id, string mauSac)
        {
            var respon = await _repository.GetItemDetailViewModelWhenSelectColorAynsc(id, mauSac);
            return Ok(respon);
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

        [Authorize(Roles = AppRole.Admin)]
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

        //[Authorize(Roles = AppRole.Admin)]
        [HttpPut("UpdateAsync2")]
        public async Task<IActionResult> UpdateAsync2(ChiTietSanPham obj)
        {
            var respon = await _repository.UpdateAsync2(obj);
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
        [HttpDelete("Ngung-kinh-doanh-san-pham/{id}")]
        public async Task<IActionResult> NgungKinhDoanhSanPham(Guid id)
        {
            var sanPhamChiTiet = _repository.GetAsync().Result.FirstOrDefault(x => x.Id == id);
            if (sanPhamChiTiet != null)
            {
                sanPhamChiTiet.TrangThai = 0;
                var result = await _repository.UpdateAsync(sanPhamChiTiet);
                if (result.IsSuccess == true)
                {
                    return Ok(result);
                }
                else return BadRequest(result);

            }
            return BadRequest(false);
        }
        [HttpDelete("Khoi-phuc-kinh-doanh-san-pham/{id}")]
        public async Task<IActionResult> KhoiPhucKinhDoanhSanPham(Guid id)
        {
            var sanPhamChiTiet = _repository.GetAsync().Result.FirstOrDefault(x => x.Id == id);
            if (sanPhamChiTiet != null)
            {
                sanPhamChiTiet.TrangThai = 1;
                var result = await _repository.UpdateAsync(sanPhamChiTiet);
                if (result.IsSuccess == true)
                {
                    return Ok(result);
                }
                else return BadRequest(result);

            }
            return BadRequest(false);
        }
    }
}
