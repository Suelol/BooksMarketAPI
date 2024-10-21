using Microsoft.EntityFrameworkCore;
using WebApplication1.DbContextApi;
using WebApplication1.Interface;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IBooksService, BooksService>();
// ��������� ������� � ���������.
builder.Services.AddControllers();
// ����������� Swagger/OpenAPI ��� ������������
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ����������� ����������� � ���� ������
builder.Services.AddDbContext<TestApiDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")),
    ServiceLifetime.Scoped);

var app = builder.Build();

// ����������� �������� ��������� HTTP-��������.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// ����������� ������������� ��� ������������
app.MapControllers();

app.Run();
