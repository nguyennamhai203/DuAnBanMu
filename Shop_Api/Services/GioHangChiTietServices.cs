using Microsoft.AspNetCore.Identity;
using Shop_Api.Repository.IRepository;
using Shop_Api.Services.IServices;
using Shop_Models.Dto;
using Shop_Models.Entities;

namespace Shop_Api.Services
{
	public class GioHangChiTietServices : IGioHangChiTietServices
	{
		private readonly IGioHangChiTietRepository _reposGioHangChiTiet;
		private readonly IGioHangRepository _reposGioHang;
		private readonly IChiTietSanPhamRepository _reposSanPhamChiTiet;
		private readonly UserManager<NguoiDung> _userManager;
		public GioHangChiTietServices(IGioHangChiTietRepository reposGioHangChiTiet, IGioHangRepository reposGioHang,
			IChiTietSanPhamRepository reposSanPhamChiTiet, UserManager<NguoiDung> userManager)
		{
			_reposGioHangChiTiet = reposGioHangChiTiet;
			_reposGioHang = reposGioHang;
			_reposSanPhamChiTiet = reposSanPhamChiTiet;
			_userManager = userManager;


		}
		public async Task<ResponseDto> AddCart(string userName, string codeProductDetail)
		{
			try
			{
				var sanPhamChiTietDTO = _reposSanPhamChiTiet.PGetProductDetail(null, codeProductDetail, null, null, null, null, null, null, null, null, null, null, null,null).Result.FirstOrDefault();
				var sanPhamChiTiet = await _reposSanPhamChiTiet.GetAsync();
				var user = await _userManager.FindByNameAsync(userName);

				if (user == null)
				{
					return ErrorResponse("Không tìm thấy tài khoản", 404);
				}

				if (sanPhamChiTietDTO == null)
				{
					return ErrorResponse("Không tìm thấy sản phẩm", 404);
				}


				var userCart = _reposGioHang.GetByIdGioHang(user.Id).Result;
				if (userCart.IsSuccess != false)
				{
					var checkProductInCart = await _reposGioHangChiTiet.GetAll();
					var userCartDetail = checkProductInCart.FirstOrDefault(a => a.GioHangId == user.Id && a.ChiTietSanPhamId == sanPhamChiTietDTO.Id);
					if (userCartDetail != null)
					{
						userCartDetail.SoLuong += 1;
						if (await _reposGioHangChiTiet.Updatesync(userCartDetail))
						{
							return SuccessResponse(userCartDetail, 200, "Thêm sản phẩm vào giỏ hàng thành công");
						}
						return ErrorResponse("Không thể cập nhật số lượng sản phẩm trong giỏ hàng", 404);

					}
					else
					{
						var newCartDetail = new GioHangChiTiet
						{
							Id = Guid.NewGuid(),
							ChiTietSanPhamId = sanPhamChiTietDTO.Id,
							GioHangId = user.Id,
							SoLuong = 1,
							TrangThai = 1
						};

						if (await _reposGioHangChiTiet.CreateAsync(newCartDetail))
						{
							return SuccessResponse(newCartDetail, 201, "Thêm sản phẩm vào giỏ hàng thành công");
						}

						return ErrorResponse("Không thể thêm sản phẩm vào trong giỏ hàng", 404);
					}
				}
				else
				{
					var newCart = new GioHang
					{
						IdNguoiDung = user.Id,
						TrangThai = 1,
					};
					var createGioHang = await _reposGioHang.CreateGioHang(newCart);
					if (createGioHang.IsSuccess == true)
					{
						var newCartDetail = new GioHangChiTiet
						{
							Id = Guid.NewGuid(),
							ChiTietSanPhamId = sanPhamChiTietDTO.Id,
							GioHangId = user.Id,
							SoLuong = 1,
							TrangThai = 1
						};

						if (await _reposGioHangChiTiet.CreateAsync(newCartDetail))
						{
							return SuccessResponse(newCartDetail, 201, "Thêm sản phẩm vào giỏ hàng thành công");
						}
					}

					return ErrorResponse("Không thể thêm sản phẩm vào trong giỏ hàng", 404);
				}
			}
			catch (Exception e)
			{

				return ErrorResponse(e.Message, 500);
			}
		}

		private ResponseDto ErrorResponse(string message, int code)
		{
			return new ResponseDto
			{
				Content = null,
				IsSuccess = false,
				Code = code,
				Message = message
			};
		}

		private ResponseDto SuccessResponse(object result, int code, string message)
		{
			return new ResponseDto
			{
				Content = null,
				IsSuccess = true,
				Code = code,
				Message = message
			};
		}

		public Task<ResponseDto> CongQuantityCartDetail(Guid idCartDetail)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseDto> GetAllCarts()
		{
			throw new NotImplementedException();
		}

		public Task<ResponseDto> GetCartById(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseDto> GetCartJoinForUser(string username)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseDto> TruQuantityCartDetail(Guid idCartDetail)
		{
			throw new NotImplementedException();
		}
	}
}
