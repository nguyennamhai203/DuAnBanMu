using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Shop_Api.AppDbContext;
using Shop_Models.Entities;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
//using Shop_API.Repository.IRepository;
//using Shop_API.Repository;
using Microsoft.OpenApi.Models;
using Shop_Api.Repository.IRepository;
using Shop_Api.Repository;
using Shop_Api.Services.IServices;
using Shop_Api.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentity<NguoiDung, ChucVu>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
   builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();



builder.Services.AddScoped<UserManager<NguoiDung>>();
builder.Services.AddScoped<SignInManager<NguoiDung>>();
builder.Services.AddScoped<ISanPhamRepository, SanPhamRepository>();
builder.Services.AddScoped<ISanPhamYeuThichServices, SanPhamYeuThichServices>();
builder.Services.AddScoped<IKhuyenMaiRepository, KhuyenMaiRepository>();
builder.Services.AddScoped<IGioHangChiTietRepository, GioHangChiTietRepository>();
builder.Services.AddScoped<ILoaiRepository, LoaiRepository>();
builder.Services.AddScoped<IPhuongThucThanhToanRepository, PhuongThucThanhToanRepository>();
builder.Services.AddScoped<IHoaDonChiTietRepository, HoaDonChiTietRepository>();
builder.Services.AddScoped<IThuongHieuRepository, ThuongHieuRepository>();
builder.Services.AddScoped<IChatLieuRepository, ChatLieuRepository>();
builder.Services.AddScoped<IThongKeRepository, ThongKeRepository>();
builder.Services.AddScoped<IThongKeSanPhamServices, ThongKeSanPhamServices>();
builder.Services.AddScoped<IChiTietKhuyenMaiRepository, ChiTietKhuyenMaiRepository>();
builder.Services.AddScoped<IChiTietSanPhamRepository, ChiTietSanPhamRepository>();
builder.Services.AddScoped<IXuatXuRepository, XuatXuRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<ISanPhamYeuThichRepository, SanPhamYeuThichRepository>();
builder.Services.AddScoped<IGioHangRepository, GioHangRepository>();
builder.Services.AddScoped<IAnhRepository, AnhRepository>();
builder.Services.AddScoped<IHoaDonRepository, HoaDonRepository>();
builder.Services.AddScoped<IMauSacRepository, MauSacRepository>();
builder.Services.AddScoped<IPhuongThucThanhToanChiTietRepository, PhuongThucThanhToanChiTietRepository>();
builder.Services.AddScoped<IGioHangChiTietServices, GioHangChiTietServices>();
builder.Services.AddScoped<IHoaDonServices, HoaDonServices>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddHostedService<PromotionUpdateService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Book API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();
app.UseStaticFiles();// cho phép s? d?ng file

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add this line to configure authentication

app.UseAuthorization();

app.MapControllers();

app.Run();
