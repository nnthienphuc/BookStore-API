using BookStoreAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookStoreAPI.Services.AuthService.Interfaces;
using BookStoreAPI.Services.AuthService;
using BookStoreAPI.Services.EmailService;
using BookStoreAPI.Services.AuthService.Repositories;
using BookStoreAPI.Middlewares;
using BookStoreAPI.Services.CategoryService.Interfaces;
using BookStoreAPI.Services.CategoryService;
using BookStoreAPI.Services.CategoryService.Repositories;
using BookStoreAPI.Services.AuthorService;
using BookStoreAPI.Services.AuthorService.Repositories;
using BookStoreAPI.Services.AuthorService.Interfaces;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
});

// Connect front-end
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Bảo đảm ASP.NET Core đọc được biến môi trường
builder.Configuration.AddEnvironmentVariables();

// Lấy biến môi trường
var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? builder.Configuration["Jwt:Key"];
var smtpUser = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? builder.Configuration["EmailSettings:SmtpUsername"];
var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? builder.Configuration["EmailSettings:SmtpPassword"];

// Lấy chuỗi kết nối từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Đăng ký ApplicationDbContext với Dependency Injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Cấu hình Authentication với JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Thêm các dịch vụ vào container
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<EmailSenderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

var app = builder.Build();

// Cấu hình đường dẫn yêu cầu HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();

app.UseCors("AllowFrontend");

// Sử dụng Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
