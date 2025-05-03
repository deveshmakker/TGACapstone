using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order.Application;
using Order.Application.Interfaces;
using Order.Application.Services;
using Order.Common.Repository;
using Order.Data.Context;
using Order.Data.Repository;
using RabbitMqHandler;
using RabbitMqHandler.Interfaces;
using RabbitMqHandler.Services;

var builder = WebApplication.CreateBuilder(args);

var orderConnectionStr = builder.Configuration.GetConnectionString("OrderConnection");
builder.Services.AddDbContextPool<OrderDbContext>(options =>
{
    options.UseMySql(orderConnectionStr, ServerVersion.AutoDetect(orderConnectionStr));
});

builder.Services.Configure<MqSettings>(builder.Configuration.GetSection("ApiSettings:RabbitMqSettings"));
builder.Services.AddScoped(typeof(IMqUtilityService), typeof(MqUtilityService));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddHttpContextAccessor(); // not sure
builder.Services.AddHttpClient("CartApi", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartAPI"]));
//.AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>()
//.AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(3, TimeSpan.FromMilliseconds(120000)));
builder.Services.AddHttpContextAccessor(); // not sure
builder.Services.AddHttpClient("ProductApi", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]));

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
    var _db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    if (_db.Database.GetPendingMigrations().Any())
        _db.Database.Migrate();
}
