using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Data.Entity;

namespace Shop_Api.Repository
{
    public class ChatLieuRepository : IChatLieuRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public static int PAGE_SIZE { get; set; } = 2;
        public ChatLieuRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<ResponseDto> CreateAsync(ChatLieu model)
        {
            var checkMa = _dbContext.ChatLieus.Any(x => x.MaChatLieu == model.MaChatLieu);
            if (model == null || checkMa == true)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 400,
                    Message = "Trùng Mã",
                };
            }
            try
            {
                await _dbContext.ChatLieus.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

        public async Task<ResponseDto> UpdateAsync(ChatLieu model, Guid id)
        {
            var chatLieu = await _dbContext.ChatLieus.FindAsync(id);
            if (chatLieu == null)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 404,
                    Message = "Không Tim Thấy Bản Ghi",
                };
            }
            try
            {
                chatLieu.MaChatLieu = model.MaChatLieu;
                chatLieu.TenChatLieu = model.TenChatLieu;
                chatLieu.TrangThai = model.TrangThai;
                _dbContext.ChatLieus.Update(chatLieu);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

        public async Task<ResponseDto> DeleteAsync(Guid Id)
        {
            var chatLieu = await _dbContext.ChatLieus.FindAsync(Id);
            if (chatLieu == null)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 404,
                    Message = "Không Tim Thấy Bản Ghi",
                };
            }
            try
            {
                _dbContext.ChatLieus.Remove(chatLieu);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = true,
                    Code = 200,
                    Message = "Xóa Thành Công",
                };
            }
            catch (Exception)
            {
                return new ResponseDto
                {
                    Content = null,
                    IsSuccess = false,
                    Code = 500,
                    Message = "Lỗi Hệ Thống",
                };
            }
        }

        public async Task<List<ChatLieu>> GetAllAsync()
        {
            var list = _dbContext.ChatLieus.AsQueryable();
            return list.ToList();
        }

        public async Task<List<ChatLieu>> GetAsync(int? status, int page = 1)
        {
            var list = _dbContext.ChatLieus.AsQueryable();
            if (status.HasValue)
            {
                list = list.Where(x => x.TrangThai == status);
            }

            #region Paging
            list = list.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            var result = list.Select(sp => new ChatLieu
            {
                MaChatLieu = sp.MaChatLieu,
                TenChatLieu = sp.TenChatLieu,
                TrangThai = sp.TrangThai
            });
            return result.ToList();
        }

        public async Task<ChatLieu> GetByIdAsync(Guid id)
        {
            return await _dbContext.ChatLieus.FindAsync(id);
        }
    }
}
