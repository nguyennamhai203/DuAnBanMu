using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Api.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_Api.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<NguoiDung> GetByName(string name)
		{
			return await _context.NguoiDungs.FirstOrDefaultAsync(x => x.UserName == name);
		}
	}
}
