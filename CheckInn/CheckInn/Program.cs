using CheckInn.DataAccess.Data;
using CheckInn.Services.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CheckInn.Utilities;
using Microsoft.AspNetCore.Identity.UI.Services;
using CheckInn.Services.Contracts;
using CheckInn.Services.ApiModule;
using CheckInn.Services.StarsService;
using CheckInn.Services.HotelReservations;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IStarsRatingService, StarsRatingService>();
builder.Services.AddScoped<IHotelReservationsService, HotelReservationsService>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
