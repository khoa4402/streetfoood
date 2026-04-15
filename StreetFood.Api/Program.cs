using Microsoft.EntityFrameworkCore;
using StreetFood.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// --- 1. DỊCH VỤ (SERVICES) ---

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        }));

// Cấu hình CORS: Phải thật sự "mở toang cửa" thì Web mới vào được
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// --- 2. CẤU HÌNH PIPELINE (MIDDLEWARE) ---

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// 🚩 LƯU Ý QUAN TRỌNG: 
// Nếu bạn gặp lỗi "Failed to fetch", hãy thử tạm thời comment (vô hiệu hóa) dòng dưới đây.
// Vì đôi khi nó ép từ HTTP sang HTTPS làm trình duyệt chặn CORS.
// app.UseHttpsRedirection(); 

// Kích hoạt CORS (Phải nằm TRƯỚC UseAuthorization)
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();