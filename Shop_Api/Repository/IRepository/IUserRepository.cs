using Shop_Models.Entities;

namespace Shop_Api.Repository.IRepository
{
	public interface IUserRepository
	{
		public Task<NguoiDung> GetByName(string name);
	}
}