using BusinessLogic.Service.Interface;
using BusinessLogic.Service;
using DataAccess.Entity;
using DataAccess.Repository.Interface;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<Court4UDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Make the session cookie essential
});
// Register services and repositories
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IClubService, ClubService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddScoped<ICourtService, CourtService>();
builder.Services.AddScoped<ISubscriptionOptionService, SubscriptionOptionService>();
builder.Services.AddScoped<IMemberSubscriptionService, MemberSubscriptionService>();
builder.Services.AddScoped<ISlotService, SlotService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IBookedSlotService, BookedSlotService>();
builder.Services.AddScoped<IQRService, QRService>();
//Repository
builder.Services.AddSingleton<IClubRepository, ClubRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourtRepository, CourtRepository>();
builder.Services.AddScoped<ISubscriptionOptionRepository, SubscriptionOptionRepository>();
builder.Services.AddScoped<IMemberSubscriptionRepository, MemberSubscriptionRepository>();
builder.Services.AddScoped<ISlotRepository, SlotRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBookedSlotRepository, BookedSlotRepository>();

builder.Services.AddLogging(config =>
{
    config.ClearProviders();
    config.AddConsole();
    config.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
