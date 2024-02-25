﻿using Microsoft.EntityFrameworkCore;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_API.Repository
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public static int PAGE_SIZE { get; set; } = 2;
        public SanPhamRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<ResponseDto> CreateAsync(SanPham model)
        {
            var checkMa = await _dbContext.SanPhams.AnyAsync(x => x.MaSanPham == model.MaSanPham);
            if (model == null || checkMa == true)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Trùng Mã",
                };
            }
            try
            {
                await _dbContext.SanPhams.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

        public async Task<List<SanPham>> GetAsync()
        {
            var list = await _dbContext.SanPhams.ToListAsync();
            return list;
        }

        public async Task<List<SanPham>> GetAsync(int? status, int page = 1)
        {
            var list = _dbContext.SanPhams.AsQueryable();
            if (status.HasValue)
            {
                list = list.Where(x => x.TrangThai == status);
            }

            #region Paging
            list = list.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            var result = list.Select(sp => new SanPham
            {
                MaSanPham = sp.MaSanPham,
                TenSanPham = sp.TenSanPham,
                TrangThai = sp.TrangThai
            });
            return result.ToList();
        }

        public async Task<ResponseDto> UpdateAsync(SanPham model)
        {
            var sanPham = await _dbContext.SanPhams.FindAsync(model.IdSanPham);
            if (sanPham == null)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 404,
                    Message = "Không Tim Thấy Bản Ghi",
                };
            }
            try
            {
                sanPham.MaSanPham = model.MaSanPham;
                sanPham.TenSanPham = model.TenSanPham;
                sanPham.TrangThai = model.TrangThai;
                _dbContext.SanPhams.Update(sanPham);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }


        public async Task<ResponseDto> DeleteAsync(Guid Id)
        {
            var sanPham = await _dbContext.SanPhams.FindAsync(Id);
            if (sanPham == null)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 404,
                    Message = "Không Tim Thấy Bản Ghi",
                };
            }
            try
            {
                _dbContext.SanPhams.Remove(sanPham);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Xóa Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

    }
}
