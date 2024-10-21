using Microsoft.EntityFrameworkCore;
using WebApplication1.DbContextApi;
using WebApplication1.Interface;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IBooksService, BooksService>();
// Добавляем сервисы в контейнер.
builder.Services.AddControllers();
// Настраиваем Swagger/OpenAPI для документации
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Настраиваем подключение к базе данных
builder.Services.AddDbContext<TestApiDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")),
    ServiceLifetime.Scoped);

var app = builder.Build();

// Настраиваем конвейер обработки HTTP-запросов.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// Настраиваем маршрутизацию для контроллеров
app.MapControllers();

app.Run();
