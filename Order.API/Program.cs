using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Order.API.Utility;
using Order.Application;
using Order.Application.Interfaces;
using Order.Application.Services;
using Order.Common.Repository;
using Order.Data.Context;
using Order.Data.Repository;
using RabbitMqHandler;
using RabbitMqHandler.Interfaces;
using RabbitMqHandler.Services;
using System.Security.Claims;
using System.Text;

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
builder.Services.AddScoped<BearerTokenHandler>();
builder.Services.AddHttpContextAccessor(); 

builder.Services.AddHttpClient("CartApi", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartAPI"]))
.AddHttpMessageHandler<BearerTokenHandler>();


builder.Services.AddHttpClient("ProductApi", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]))
.AddHttpMessageHandler<BearerTokenHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var key = Encoding.ASCII.GetBytes(builder.Configuration["ApiSettings:JwtOptions:Secret"]);

var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = false,
    RequireExpirationTime = false,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddSingleton(tokenValidationParams);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireClaim(ClaimTypes.NameIdentifier);
    });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParams;
    // We have to hook the OnMessageReceived event in order to
    // allow the JWT authentication handler to read the access
    // token from the query string when a WebSocket or 
    // Server-Sent Events request comes in.
    jwt.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            if (!string.IsNullOrEmpty(accessToken) &&
                (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
            {
                context.Token = context.Request.Query["access_token"];
            }

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/ordershub")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Order", Version = "v1" });
    //s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    s.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API v1");
    });
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
