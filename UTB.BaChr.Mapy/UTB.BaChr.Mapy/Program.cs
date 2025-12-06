using Microsoft.EntityFrameworkCore;
using UTB.BaChr.Mapy.Application.Abstraction;
using UTB.BaChr.Mapy.Application.Implementation;
using UTB.BaChr.Mapy.Infrastructure.Database;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. Nastavení pøipojení k databázi (MySQL)
var connectionString = builder.Configuration.GetConnectionString("MySQL");

// DÙLEŽITÉ: Místo AutoDetect (který zkouší pøipojení) nastavíme verzi ruènì.
// Pokud nevíte pøesnou verzi, 8.0.38 je bezpeèná volba pro moderní MySQL.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 38));

builder.Services.AddDbContext<MapyDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// 2. Konfigurace Identity (Uživatelé a Role)
builder.Services.AddIdentity<User, Role>(options =>
{
    // Nastavení hesel (pro vývoj volnìjší, pro produkci zpøísnit)
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<MapyDbContext>()
.AddDefaultTokenProviders();

// Konfigurace cest pro pøesmìrování (když uživatel není pøihlášen)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Security/Account/Login";
    options.AccessDeniedPath = "/Security/Account/AccessDenied";
});

// 3. Registrace vlastních služeb (Dependency Injection)
// Zde propojujeme Interface s Implementací
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IFileUploadService>(provider => new FileUploadService(builder.Environment.WebRootPath));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Poøadí je dùležité: Authentication (kdo to je) -> Authorization (co mùže dìlat)
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Mapování rout pro Areas (Admin, Security)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Mapování defaultní routy
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();