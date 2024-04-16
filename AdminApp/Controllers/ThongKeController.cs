using AdminApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using OfficeOpenXml;
using Shop_Models.Dto;
using Shop_Models.Entities;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;

namespace AdminApp.Controllers
{
    public class ThongKeController : Controller
    {
        public async Task<IActionResult> ExportToExcel()
        {
            // Check if ViewData.Model is not null and is of the correct type
            if (ViewData.Model is IEnumerable<ChiTietSanPham> data)
            {
                // Tạo một package Excel
                ExcelPackage excelPackage = new ExcelPackage();
                var worksheet = excelPackage.Workbook.Worksheets.Add("Thống kê doanh thu");

                // Thêm tiêu đề cột
                string[] headers = { "Mã sản phẩm", "Giá Nhập", "Giá bán", "Giá thực tế", "Số lượng tồn", "Số lượng đã bán", "Tổng doanh thu", "Lãi", "Lỗ" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                // Thêm dữ liệu từ view vào Excel
                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.MaSanPham;
                    worksheet.Cells[row, 2].Value = item.GiaNhap;
                    worksheet.Cells[row, 3].Value = item.GiaBan;
                    worksheet.Cells[row, 4].Value = item.GiaThucTe;
                    worksheet.Cells[row, 5].Value = item.SoLuongTon;
                    worksheet.Cells[row, 6].Value = item.SoLuongDaBan;

                    decimal revenue = Convert.ToDecimal(item.GiaBan * item.SoLuongDaBan);
                    decimal profit = Convert.ToDecimal((item.GiaBan - item.GiaNhap) * item.SoLuongDaBan);
                    decimal loss = Convert.ToDecimal((item.GiaNhap - item.GiaBan) * item.SoLuongDaBan);

                    worksheet.Cells[row, 7].Value = revenue;
                    worksheet.Cells[row, 8].Value = profit >= 0 ? profit : 0;
                    worksheet.Cells[row, 9].Value = profit < 0 ? profit : 0;

                    row++;
                }

                // Xuất file Excel
                var stream = new MemoryStream();
                excelPackage.SaveAs(stream);
                stream.Position = 0;

                // Trả về file Excel như một file download
                return File(stream, "\"C:\\Users\\admin\\Desktop\\file\"", "ThongKeDoanhThu.xlsx");
            }
            else
            {
                // Handle the case where ViewData.Model is null or not of the correct type
                // Return an appropriate response, such as a view with an error message
                return NotFound();
            }
        }
        // GET: ThongKeController
        public async Task<IActionResult> Index()
        {
            // Để call được API thì chúng ta cần lấy được URL request
            string requestURL = "https://localhost:7050/api/ChiTietSanPham/Get";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestURL); // lấy response
            // Đọc từ response chuỗi Json là kết quả của phép trả về
            string apiData = await response.Content.ReadAsStringAsync();
            // Có data rồi thì ta sẽ convert về dữ liệu mình cần để đưa sang view
            var thongkes = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiData);
            return View(thongkes);
        }

        public async Task<IActionResult> IndexMain()
        {
            // Để call được API thì chúng ta cần lấy được URL request
            //string requestURL = "https://localhost:7050/api/ThongKe/Get?page=1";
            string requestURL = "https://localhost:7050/api/ChiTietSanPham/Get";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestURL); // lấy response
            // Đọc từ response chuỗi Json là kết quả của phép trả về
            string apiData = await response.Content.ReadAsStringAsync();
            // Có data rồi thì ta sẽ convert về dữ liệu mình cần để đưa sang view
            var thongkes = JsonConvert.DeserializeObject<List<ChiTietSanPham>>(apiData);
            return View(thongkes);
        }

        // GET: ThongKeController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            string requestURL =
                $"api";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestURL); // lấy response
            // Đọc từ response chuỗi Json là kết quả của phép trả về
            string apiData = await response.Content.ReadAsStringAsync();
            // Có data rồi thì ta sẽ convert về dữ liệu mình cần để đưa sang view
            var thongkes = JsonConvert.DeserializeObject<ThongKe>(apiData);
            return View(thongkes);
        }

        // GET: ThongKeController/Create
        public async Task<IActionResult> ViewCreate()
        {
            return View();
        }
        public async Task<IActionResult> ViewCreate2()
        {
            return View();
        }

        // POST: ThongKeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThongKe tk)
        {
            try
            {
                // Cách dùng obj
                string requestURL =
                    $"api"; // truyền bằng object
                var httpClient = new HttpClient();
                var obj = JsonConvert.SerializeObject(tk);
                var response = await httpClient.PostAsJsonAsync(requestURL, tk); // lấy response
                                                                                 // Đọc từ response chuỗi Json là kết quả của phép trả về
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else return BadRequest(response);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create2(ThongKe tk)
        {
            // Cách dùng obj
            ThongKe tk2 = tk;
            string requestURL =
                @$"api" +
                $"&Ngay={tk.Ngay}&Thang={tk.Thang}" +
                $"&Nam={tk.Nam}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestURL); // lấy response
            // Đọc từ response chuỗi Json là kết quả của phép trả về
            string apiData = await response.Content.ReadAsStringAsync();
            // Có data rồi thì ta sẽ convert về dữ liệu mình cần để đưa sang view
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else return BadRequest(response);
        }

        // GET: ThongKeController/Edit/5
        public async Task<IActionResult> ViewEdit(Guid id)
        {
            return View();
        }

        // POST: ThongKeController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id,ThongKe tk)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ThongKeController/Delete/5
        public async Task<IActionResult> ViewDelete(Guid id)
        {
            return View();
        }

        // POST: ThongKeController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                string requestURL =
                $"api";
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(requestURL); // lấy response
                                                                      // Đọc từ response chuỗi Json là kết quả của phép trả về
                string apiData = await response.Content.ReadAsStringAsync();
                // Có data rồi thì ta sẽ convert về dữ liệu mình cần để đưa sang view
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else return BadRequest(response);
            }
            catch
            {
                return View();
            }
        }
    }
}