using ConectaSys.ConectaSys.Application.Services;
using ConectaSys.ConectaSys.Application.UserCases;
using ConectaSys.ConectaSys.Domain.Interfaces;
using ConectaSys.Infrastructure.Persistence;
using ConectaSys.Infrastructure.Repositories;
using ConectaSys.Infrastructure.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ConectaSys.ConectaSys.Infrastructure.Logging.ConectaSys.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

// Registro de serviços
builder.Services.AddScoped<GetAllUsersCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CreateUserCase>();
builder.Services.AddScoped<LoginUserCase>();
builder.Services.AddScoped<JwtTokenGenerator>();

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuração do DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do JWT
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("Jwt:Key não configurada no appsettings.json.");
}

var key = Encoding.ASCII.GetBytes(jwtKey);
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

// Configuração de autorização
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração de logging
builder.Logging.ClearProviders(); // Remove provedores padrão
builder.Logging.AddProvider(new DatabaseLoggerProvider(
    new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .Options
    )
));
builder.Logging.AddConsole(); // Adiciona logs no console (opcional)

var app = builder.Build();

// Configuração de middlewares
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
