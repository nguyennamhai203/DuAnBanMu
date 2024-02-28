﻿using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Repository
{
    public class GioHangChiTietRepository : IGioHangChiTietRepository
    {
        private readonly ApplicationDbContext _context;

        public GioHangChiTietRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(GioHangChiTiet obj)
        {
            if (obj == null)
            {
                return false;
            }
            try
            {
                await _context.GioHangChiTiets.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Deletesync(Guid id)
        {
            var cartDT = await _context.GioHangChiTiets.FindAsync(id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                _context.GioHangChiTiets.Remove(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<GioHangChiTiet>> GetAll()
        {
            return await _context.GioHangChiTiets.ToListAsync();
        }

        public async Task<GioHangChiTiet> GetById(Guid id)
        {
            return await _context.GioHangChiTiets.FindAsync(id);
        }

        public async Task<bool> Updatesync(GioHangChiTiet obj)
        {
            var cartDT = await _context.GioHangChiTiets.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.SoLuong = obj.SoLuong;
                _context.GioHangChiTiets.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<ResponseDto> GetCartJoinForUser(string username)
        {
            var cartItem = await GetCartItem(username);
            if (cartItem == null)
            {          
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Code = 404,
                    Message = "Lỗi",
                };
            }
            else
            {
                
                return new ResponseDto
                {
                    Result = cartItem,
                    IsSuccess = true,
                    Code = 200,
                };
            }

        }
        public async Task<IEnumerable<GioHangChiTiet>> GetCartItem(string username)
        {
            // Truyền vào tên tào khoản của người dùng
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<GioHangChiTiet> cartItem = new List<GioHangChiTiet>();// Khởi tao 1 list
                cartItem = (
                           // Join các bảng lại để lấy dữ liệu
                           from x in await _context.GioHangs.ToListAsync()
                           join y in await _context.GioHangChiTiets.ToListAsync() on x.IdNguoiDung equals y.GioHangId
                           join a in await _context.ChiTietSanPhams.ToListAsync() on y.ChiTietSanPhamId equals a.Id

                           select new GioHangChiTiet// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               GioHangId = x.IdNguoiDung,
                               SoLuong = y.SoLuong,
                               TrangThai = y.TrangThai,
                               ChiTietSanPhamId = a.Id,
                               GiaBan = (double)a.GiaBan,
                           }
                    ).ToList();
                return cartItem.Where(x => x.GioHangId == user.Id);// Trả về list với điểu kiện 
            }
            catch (Exception)
            {
                // Nếu idUser bị null tức là không tìm thấy, sẽ xảy ra Exception
                // Sau đó trả về null
                return null;
            }

        }
    }
}