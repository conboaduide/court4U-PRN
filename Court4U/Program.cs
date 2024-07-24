using DataAccess.Repository;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using DataAccess.Repository.Interface;
using BusinessLogic.Service.Interface;
using BusinessLogic.Service;

using DataAccess.Repository.Request;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Court4UDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

// Service
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

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();
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

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
