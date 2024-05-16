using Shop_Api.AppDbContext;
using Shop_Models.Entities;

namespace Shop_Api.Services.Models
{
    public class FavoriteProductService
    {
        ApplicationDbContext _context;
        public FavoriteProductService()
        {
            _context = new ApplicationDbContext();
        }
        public void SaveFavoriteProducts(Guid userId, List<SanPhamYeuThich> favoriteProducts)
        {
            try
            {
                // Retrieve existing favorite products for the user
                var existingFavorites = _context.SanPhamYeuThichs
                .Where(f => f.NguoiDungId == userId)
                .ToList();
                // Remove existing favorite products
                _context.SanPhamYeuThichs.RemoveRange(existingFavorites);

                // Add new favorite products
                foreach (var favoriteProduct in favoriteProducts)
                {
                    _context.SanPhamYeuThichs.Add(favoriteProduct);
                }

                // Save changes to the database
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions and log errors
                Console.WriteLine($"Error saving favorite products: {ex.Message}");
                throw; // Re-throw the exception to be handled by the caller
            }
        }
    }
}