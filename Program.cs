using Microsoft.EntityFrameworkCore;
using ProjectBuoi7.Data;

var builder = WebApplication.CreateBuilder(args);

// Thêm DbContext vào container dịch vụ
builder.Services.AddDbContext<SQLDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

// Các cấu hình khác
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Cấu hình Swagger trong môi trường phát triển
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
