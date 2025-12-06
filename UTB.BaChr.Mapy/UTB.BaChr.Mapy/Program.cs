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
    // Zrušíme složité požadavky na heslo pro testování
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false; // Nemusí mít znaky jako !@#
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;

    // Dùležité: Uživatel se mùže pøihlásit i bez potvrzeného emailu
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<MapyDbContext>()
.AddDefaultTokenProviders();

// Nastavení cookies (aby pøihlášení vydrželo)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Security/Account/Login";
    options.LogoutPath = "/Security/Account/Logout";
    options.SlidingExpiration = true;
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