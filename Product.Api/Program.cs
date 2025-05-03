using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Application.Services;
using Product.Application;
using Product.Common.Interfaces;
using Product.Data.Context;
using Product.Data.Repository;



var builder = WebApplication.CreateBuilder(args);

var productConnectionStr = builder.Configuration.GetConnectionString("ProductConnection");
builder.Services.AddDbContextPool<ProductDbContext>(options =>
{
    options.UseMySql(productConnectionStr, ServerVersion.AutoDetect(productConnectionStr));
});

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDb();

app.Run();

void MigrateDb()
{
    using var scope = app.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    if (_db.Database.GetPendingMigrations().Any())
        _db.Database.Migrate();
}
