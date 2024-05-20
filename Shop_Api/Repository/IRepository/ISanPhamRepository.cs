﻿using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
    public interface ISanPhamRepository
    {
        public Task<ResponseDto> CreateAsync(SanPham model);
        public Task<ResponseDto> UpdateAsync(SanPham model);
        public Task<ResponseDto> DeleteAsync(Guid Id);
        public Task<List<SanPham>> GetAsync();
        public Task<SanPham> GetByIdAsync(Guid id);
        public Task<List<SanPham>> GetAsync(int? status, int page = 1);
    }
}
