using EduNova.Infrastructure;
using EduNova.Infrastructure.Entities;
using EduNova.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Entity framework & Identity services
builder.Services.AddDbContext<NovaDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDBConnection")));

builder.Services.AddIdentity<CustomUser, IdentityRole>().AddEntityFrameworkStores<NovaDBContext>();
builder.Services.AddTransient<IdentitySeeding>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


//Run identity seeding
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeding>();
    UserManager<CustomUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();
    RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await seeder.IdentitySeedingAsync(userManager, roleManager);
}

app.Run();
