
using ConectaSys.ConectaSys.Domain.Interfaces.Users;
using ConectaSys.ConectaSys.Domain.Interfaces.ProductRepository;

using ConectaSys.ConectaSys.Infrastructure.Security;
using ConectaSys.ConectaSys.Infrastructure.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using ConectaSys.ConectaSys.Infrastructure.Logging.ConectaSys.Infrastructure.Logging;
using ConectaSys.Infrastructure.Persistence;
using ConectaSys.Infrastructure.Repositories.Users;
using ConectaSys.ConectaSys.Application.Users.UseCases;
using ConectaSys.ConectaSys.Application.DTOs.Products;
using ConectaSys.ConectaSys.Infrastructure.Repositories.Products;
using ConectaSys.ConectaSys.Application.Products.Services;
using ConectaSys.ConectaSys.Application.Products.UseCases;

var builder = WebApplication.CreateBuilder(args);

// ?? Configuração do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string não configurada no appsettings.json.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddScoped<UserService>();


builder.Services.AddScoped<GetAllUsersUseCase>();
builder.Services.AddScoped<UpdateUserUseCase>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<DeleteUserUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CreateProductUseCase>(); 


var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("Jwt:Key não configurada no appsettings.json.");
}

var key = Encoding.ASCII.GetBytes(jwtKey);
builder.Services.AddSingleton<JwtTokenGenerator>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddAuthorization();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

