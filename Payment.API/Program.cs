using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payment.Application;
using Payment.Application.Interfaces;
using Payment.Application.Services;
using Payment.Common.Repository;
using Payment.Data.Context;
using Payment.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

var paymentConnectionStr = builder.Configuration.GetConnectionString("PaymentConnection");
builder.Services.AddDbContextPool<PaymentDbContext>(options =>
{
    options.UseMySql(paymentConnectionStr, ServerVersion.AutoDetect(paymentConnectionStr));
});

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


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
    var _db = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    if (_db.Database.GetPendingMigrations().Any())
        _db.Database.Migrate();
}