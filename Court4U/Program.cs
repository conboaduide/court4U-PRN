using DataAccess.Repository;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using DataAccess.Repository.Interface;
using BusinessLogic.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Court4UDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));
// Register the ClubRepository
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IClubService, ClubService>();
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
