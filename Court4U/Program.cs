using DataAccess.Repository;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using DataAccess.Repository.Interface;
using BusinessLogic.Service.Interface;
using BusinessLogic.Service;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Court4UDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));
//Service
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
//Repository
builder.Services.AddSingleton<IClubRepository, ClubRepository>();
builder.Services.AddSingleton<IClubService, ClubService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
