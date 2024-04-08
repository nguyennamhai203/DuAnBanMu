using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System.Net.Http;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public CartController(ILogger<CartController> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpclient = _httpClientFactory.CreateClient("BeHat");

            HttpResponseMessage responseVoucher = await httpclient.GetAsync($"https://localhost:7050/api/Voucher/get-voucher?page=1");

            if (responseVoucher.IsSuccessStatusCode)
            {
                var resultString = await responseVoucher.Content.ReadAsStringAsync();
                // var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                ViewBag.listVoucher = JsonConvert.DeserializeObject<IEnumerable<Voucher>>(resultString);
            }
            var userName = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(userName))
            {
                var apiUrl = $"/api/GioHangChiTiet/GetCartJoinForUser?userName={userName}";
                var respone = await httpclient.GetAsync(apiUrl);
                var jsonRespone = await respone.Content.ReadAsStringAsync();
                var responsedto = JsonConvert.DeserializeObject<ResponseDto>(jsonRespone);
                var data = JsonConvert.DeserializeObject<List<GioHangChiTietViewModel>>(responsedto.Content.ToString());
                ViewBag.cartItem = data;
                return View();
            }
            else
            {
                var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");

                foreach (var product in Cart)
                {
                    string codeProductDetail = product.MaSPCT;
                    HttpResponseMessage response = await httpclient.GetAsync($"https://localhost:7050/api/ChiTietSanPham/PGetProductDetail?codeProductDetail={codeProductDetail}&status=1");
                    var resultString = await response.Content.ReadAsStringAsync();
                    var productDetail = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(resultString.ToString()).FirstOrDefault();

                    // Kiểm tra và cập nhật thông tin sản phẩm trong session
                    if (productDetail != null)
                    {
                        // Cập nhật thông tin sản phẩm trong session
                        product.GiaBan = (double)productDetail.GiaBan;
                        product.SoLuongBanSanPham = (int)productDetail.SoLuongTon;
                        // Hoặc các thuộc tính khác của sản phẩm
                    }
                }

                // Lưu lại thông tin sản phẩm đã được cập nhật trong session
                SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
                ViewBag.cartItem = Cart;

                return View();
            }


        }
        public async Task<IActionResult> CartPartialViewAsync()
        {
            var httpclient = _httpClientFactory.CreateClient("BeHat");

            var userName = HttpContext.Session.GetString("username");

            HttpResponseMessage responseVoucher = await httpclient.GetAsync($"https://localhost:7050/api/Voucher/get-voucher?page=1");

            if (responseVoucher.IsSuccessStatusCode)
            {
                var resultString = await responseVoucher.Content.ReadAsStringAsync();
                // var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                ViewBag.listVoucher = JsonConvert.DeserializeObject<IEnumerable<Voucher>>(resultString);
            }

            if (!string.IsNullOrEmpty(userName))
            {
                var apiUrl = $"/api/GioHangChiTiet/GetCartJoinForUser?userName={userName}";
                var respone = await httpclient.GetAsync(apiUrl);
                var jsonRespone = await respone.Content.ReadAsStringAsync();
                var responsedto = JsonConvert.DeserializeObject<ResponseDto>(jsonRespone);
                var data = JsonConvert.DeserializeObject<List<GioHangChiTietViewModel>>(responsedto.Content.ToString());
                ViewBag.cartItem = data;
                return PartialView("_CartPartialViewAsync");
            }
            else
            {
                var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");

                foreach (var product in Cart)
                {
                    string codeProductDetail = product.MaSPCT;
                    HttpResponseMessage response = await httpclient.GetAsync($"https://localhost:7050/api/ChiTietSanPham/PGetProductDetail?codeProductDetail={codeProductDetail}&status=1");
                    var resultString = await response.Content.ReadAsStringAsync();
                    var productDetail = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(resultString.ToString()).FirstOrDefault();

                    // Kiểm tra và cập nhật thông tin sản phẩm trong session
                    if (productDetail != null)
                    {
                        // Cập nhật thông tin sản phẩm trong session
                        product.GiaBan = (double)productDetail.GiaBan;
                        product.SoLuongBanSanPham = (int)productDetail.SoLuongTon;
                        // Hoặc các thuộc tính khác của sản phẩm
                    }
                }

                // Lưu lại thông tin sản phẩm đã được cập nhật trong session
                SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
                ViewBag.cartItem = Cart;

                return PartialView("_CartPartialViewAsync");
            }

        }

        public async Task<IActionResult> AddProductToCart(string codeProductDetail, int? soluong)
        {
            if (soluong == null || soluong == 0)
            {
                soluong = 1;
            }
            else soluong = soluong.Value;

            //var userName = "user@example1.com";
            var userName = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(userName))
            {

                var apiUrl = $"https://localhost:7050/api/GioHang/add-gio-hang?userName={userName}&codeProductDetail={codeProductDetail}&soluong={soluong}";
                var httpclient = _httpClientFactory.CreateClient("BeHat");
                var respone = await httpclient.GetAsync(apiUrl);



                if (respone.IsSuccessStatusCode)
                {
                    var result = await respone.Content.ReadAsStringAsync();

                    return Content(result, "application/json");
                }
                else
                {
                    var result = await respone.Content.ReadAsStringAsync();

                    return Content(result, "application/json");
                }
                //return View();
            }
            else
            {
                using (var client = _httpClientFactory.CreateClient("BeHat"))
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7050/api/ChiTietSanPham/PGetProductDetail?codeProductDetail={codeProductDetail}&status=1");
                    var resultString = await response.Content.ReadAsStringAsync();
                    //var resultResponse = JsonConvert.DeserializeObject<ResponseDto>(resultString);
                    var product = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(resultString.ToString()).FirstOrDefault(x => x.MaSanPhamChiTiet == codeProductDetail);
                    var Cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");
                    GioHangChiTietViewModel s = new GioHangChiTietViewModel();

                    if (SessionService.CheckObjInList(codeProductDetail, Cart))
                    {
                        GioHangChiTietViewModel ghct = Cart.FirstOrDefault(x => x.MaSPCT == codeProductDetail);
                        int newQuantity = ghct.SoLuong + (int)soluong;
                        if (newQuantity > product.SoLuongTon)
                        {
                            ghct.SoLuong = (int)product.SoLuongTon;
                            ghct.SoLuongBanSanPham = (int)product.SoLuongTon;
                            ghct.GiaBan = (int)product.GiaBan;
                        }
                        else
                        {
                            ghct.SoLuong = newQuantity;
                            ghct.GiaBan = (int)product.GiaBan; ghct.SoLuongBanSanPham = (int)product.SoLuongTon;

                        }
                        SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
                        return RedirectToAction("Index");
                    }



                    if (Cart.Count == 0)
                    {
                        s.Id = Guid.NewGuid();
                        s.MaSPCT = codeProductDetail;
                        s.TenSanPhamChiTiet = codeProductDetail + product.TenSanPham + product.TenLoai + product.TenThuongHieu;
                        s.SoLuong = (int)soluong;
                        s.SoLuongBanSanPham = (int)product.SoLuongTon;
                        s.GiaBan = (float)(product.GiaBan);
                        Cart.Add(s);
                        SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (SessionService.CheckObjInList(codeProductDetail, Cart))
                        {
                            GioHangChiTietViewModel ghct = Cart.FirstOrDefault(x => x.MaSPCT == codeProductDetail);
                            ghct.SoLuong += (int)soluong;
                            s.GiaBan = (float)(product.GiaBan);
                            s.SoLuongBanSanPham = (int)product.SoLuongTon;
                            SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            s.TenSanPhamChiTiet = codeProductDetail + product.TenSanPham + product.TenLoai + product.TenThuongHieu;
                            s.MaSPCT = codeProductDetail;
                            s.SoLuong = (int)soluong;
                            s.GiaBan = (float)(product.GiaBan);
                            s.SoLuongBanSanPham = (int)product.SoLuongTon;
                            Cart.Add(s);
                            SessionService.SetObjToSession(HttpContext.Session, "Cart", Cart);
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
        }

        public async Task<IActionResult> decreaseQuantity(string codeProductDetail2, Guid idCartDetail)
        {
            using (var client = _httpClientFactory.CreateClient("BeHat"))
            {
                var userName = HttpContext.Session.GetString("username");

                if (userName == null)
                {
                    // Get product details from the API
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7050/api/ChiTietSanPham/PGetProductDetail?codeProductDetail={codeProductDetail2}&status=1");
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(resultString.ToString()).FirstOrDefault(x => x.MaSanPhamChiTiet == codeProductDetail2);

                        // Get current cart from session or create a new one if it doesn't exist
                        var cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");

                        // Check if the product is already in the cart
                        GioHangChiTietViewModel existingProduct = cart.FirstOrDefault(p => p.Id == idCartDetail);

                        // If the product is already in the cart, increase its quantity
                        if (existingProduct != null)
                        {
                            if (existingProduct.SoLuong > 1)
                            {
                                existingProduct.SoLuong--;
                            }
                            else
                            {
                                cart.Remove(existingProduct);
                            }
                        }

                        // Update cart in session
                        SessionService.SetObjToSession(HttpContext.Session, "Cart", cart);
                    }
                    else
                    {
                        // Handle error response from the API
                        // For example, log the error or show an error message to the user
                        // You can also return a specific view indicating the error
                        return RedirectToAction("Error");
                    }
                }
                else
                {
                    HttpResponseMessage response = await client.GetAsync($"/api/GioHang/TruQuantityCartDetail?idCartDetail={idCartDetail}");
                }
            }

            return RedirectToAction("Index"); // Redirect to cart page or any other page you want
        }

        public async Task<IActionResult> IncreaseQuantityAsync(string codeProductDetail2, Guid idCartDetail)
        {
            using (var client = _httpClientFactory.CreateClient("BeHat"))
            {
                var userName = HttpContext.Session.GetString("username");
                if (userName == null)
                {
                    // Get product details from the API
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7050/api/ChiTietSanPham/PGetProductDetail?codeProductDetail={codeProductDetail2}&status=1");
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(resultString.ToString()).FirstOrDefault(x => x.MaSanPhamChiTiet == codeProductDetail2);

                        // Get current cart from session or create a new one if it doesn't exist
                        var cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");

                        // Check if the product is already in the cart
                        GioHangChiTietViewModel existingProduct = cart.FirstOrDefault(p => p.MaSPCT == product.MaSanPhamChiTiet);

                        // If the product is already in the cart, increase its quantity
                        if (existingProduct != null)
                        {
                            existingProduct.SoLuong++;
                        }
                        else
                        {
                            // If the product is not in the cart, add it to the cart with quantity = 1
                            GioHangChiTietViewModel s = new GioHangChiTietViewModel
                            {
                                Id = Guid.NewGuid(),
                                MaSPCT = codeProductDetail2,
                                TenSanPhamChiTiet = codeProductDetail2 + product.TenSanPham + product.TenLoai + product.TenThuongHieu,
                                SoLuong = 1,
                                SoLuongBanSanPham = (int)product.SoLuongTon,
                                GiaBan = (float)(product.GiaBan),

                            };
                            cart.Add(s);

                        }

                        // Update cart in session
                        SessionService.SetObjToSession(HttpContext.Session, "Cart", cart);
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                else
                {

                    //var url = $"/api/GioHang/CongQuantityCartDetail?idCartDetail={idCartDetail}";
                    HttpResponseMessage response = await client.GetAsync($"/api/GioHang/CongQuantityCartDetail?idCartDetail={idCartDetail}");

                }
            }

            return RedirectToAction("Index"); // Redirect to cart page or any other page you want
        }


        public async Task<IActionResult> updateSoLuong(string codeProductDetail2, Guid idCartDetail, int soLuong)
        {
            var user = HttpContext.Session.GetString("username");
            using (var client = _httpClientFactory.CreateClient("BeHat"))
            {

                if (user == null)
                {
                    // Get product details from the API
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7050/api/ChiTietSanPham/PGetProductDetail?codeProductDetail={codeProductDetail2}&status=1");
                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<List<SanPhamChiTietDto>>(resultString.ToString()).FirstOrDefault(x => x.MaSanPhamChiTiet == codeProductDetail2);

                        var cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");

                        // Check if the product is already in the cart
                        GioHangChiTietViewModel existingProduct = cart.FirstOrDefault(p => p.MaSPCT == product.MaSanPhamChiTiet);

                        // If the product is already in the cart, increase its quantity
                        if (existingProduct != null)
                        {
                            existingProduct.SoLuong = soLuong;
                        }
                        // Update cart in session
                        SessionService.SetObjToSession(HttpContext.Session, "Cart", cart);
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                else
                {
                    HttpResponseMessage response = await client.GetAsync($"/api/GioHang/CapNhatSoLuongCartDetail?idCartDetail={idCartDetail}&soLuong={soLuong}");
                }
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> delete(Guid idCartDetail)
        {
            using (var client = _httpClientFactory.CreateClient("BeHat"))
            {
                var userName = HttpContext.Session.GetString("username");

                if (userName == null)
                {

                    var cart = SessionService.GetObjFromSession(HttpContext.Session, "Cart");

                    GioHangChiTietViewModel existingProduct = cart.FirstOrDefault(p => p.Id == idCartDetail);


                    if (existingProduct != null)
                    {
                        cart.Remove(existingProduct);

                    }

                    SessionService.SetObjToSession(HttpContext.Session, "Cart", cart);

                }
                else
                {
                    HttpResponseMessage response = await client.GetAsync($"/api/GioHang/DeleteCartDetail?idCartDetail={idCartDetail}");
                }
            }

            return RedirectToAction("Index"); // Redirect to cart page or any other page you want
        }


    }
}
