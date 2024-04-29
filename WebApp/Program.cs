using WebApp.Services.IServices;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpClient("BeHat", hat =>
            {
                hat.BaseAddress = new Uri(builder.Configuration["UrlApiAdmin"]);

            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession();
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpClient();


            // đăng kí PayClient dạng Singleton() - chỉ có 1 instance duy nhất trong toàn ứng dụng
            //builder.Services.AddSingleton();

            builder.Services.AddSingleton<IVNPayService, VNPayService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}