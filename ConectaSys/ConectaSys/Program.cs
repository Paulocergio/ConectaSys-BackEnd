using ConectaSys.ConectaSys.Application.Services;
using ConectaSys.ConectaSys.Application.UserCases;
using ConectaSys.Infrastructure.Persistence;
using ConectaSys.Infrastructure.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ConectaSys.ConectaSys.Infrastructure.Logging.ConectaSys.Infrastructure.Logging;
using ConectaSys.ConectaSys.Infrastructure.Repositories.Users;
using ConectaSys.ConectaSys.Domain.Interfaces.Users;
using ConectaSys.ConectaSys.Domain.Interfaces.ProductRepository;
using ConectaSys.ConectaSys.Infrastructure.Repositories.ProductRepository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<GetAllUsersCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<DeleteUserCase>();
builder.Services.AddScoped<ICreateProduct, ProductRepository>();
builder.Services.AddScoped<CreateProductCase>();
builder.Services.AddScoped<CreateUserCase>();
builder.Services.AddScoped<LoginUserCase>();
builder.Services.AddScoped<JwtTokenGenerator>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"String de conexão: {connectionString}");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


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


builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.ClearProviders(); 
builder.Logging.AddProvider(new DatabaseLoggerProvider(
    new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlServer(connectionString)
        .Options
    )
));
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