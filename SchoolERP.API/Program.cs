using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Interfaces;
using SchoolERP.Application.Interfaces.Utilities;
using SchoolERP.Application.Services;
using SchoolERP.Application.Services.Utilities;
using SchoolERP.Infrastructure.Persistence;
using SchoolERP.Infrastructure.Repositories;
using SchoolERP.Infrastructure.Repositories.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

#region ✔ Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#endregion

#region  Dependency Injection
// ✔ Add Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// ✔ Add Services
builder.Services.AddScoped<IUserService, UserService>();

#endregion




// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


#region Apply Migrations Automatically
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    db.Database.Migrate(); // This applies any pending migrations
//}
#endregion

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
