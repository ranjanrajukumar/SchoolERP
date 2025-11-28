using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolERP.Application.Interfaces;
using SchoolERP.Application.Interfaces.Utilities;
using SchoolERP.Application.Services;
using SchoolERP.Application.Services.Utilities;
using SchoolERP.Infrastructure.Persistence;
using SchoolERP.Infrastructure.Repositories;
using SchoolERP.Infrastructure.Repositories.Utilities;
using System.Text;
using IUserService = SchoolERP.Application.Interfaces.Utilities.IUserService;

var builder = WebApplication.CreateBuilder(args);

// ---------------------- Controllers ----------------------
builder.Services.AddControllers();

// ---------------------- Swagger ----------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------- DbContext ----------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ---------------------- Dependency Injection ----------------------
// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();

// JWT Settings from appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// TokenService (Clean Architecture: pass settings from API)
builder.Services.AddScoped<ITokenService>(sp =>
    new TokenService(
        jwtSettings["SecretKey"],
        jwtSettings["Issuer"],
        jwtSettings["Audience"],
        Convert.ToDouble(jwtSettings["ExpiryMinutes"])
    )
);

// ---------------------- JWT Authentication ----------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// ---------------------- Apply Pending Migrations (Optional) ----------------------
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     db.Database.Migrate(); // Applies any pending migrations
// }

// ---------------------- Middleware ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// JWT authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
