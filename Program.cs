// Advanced .NET API with Clean Architecture + Entity Framework Core + JWT Authentication + Docker

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AppDB"));

// Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();

// Authentication (JWT)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "dotnet-api",
            ValidAudience = "dotnet-api",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key"))
        };
    });

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// DbContext
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

// User Entity
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

// User Service
public interface IUserService
{
    string Authenticate(string username, string password);
}

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context) { _context = context; }

    public string Authenticate(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user == null) return null;

        return "dummy-jwt-token";
    }
}