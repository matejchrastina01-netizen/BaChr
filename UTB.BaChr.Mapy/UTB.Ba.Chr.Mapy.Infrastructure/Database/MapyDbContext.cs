using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Infrastructure.Identity;

namespace UTB.BaChr.Mapy.Infrastructure.Database
{
    // Dědíme z IdentityDbContext a specifikujeme naše vlastní třídy User, Role a typ klíče (int)
    public class MapyDbContext : IdentityDbContext<User, Role, int>
    {
        public MapyDbContext(DbContextOptions options) : base(options)
        {
        }

        // --- Zde přidáváme tabulky (DbSety) ---
        public DbSet<Location> Locations { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PhotoTag> PhotoTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // TOTO JE NUTNÉ PRO IDENTITY!

            // Přejmenování tabulek Identity (aby se v DB jmenovaly hezky "Users", "Roles" atd.)
            builder.Entity<User>().ToTable(nameof(Users));
            builder.Entity<Role>().ToTable(nameof(Roles));
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            // --- SEEDING DAT (Počáteční data pro Firewatch) ---

            // 1. Lokace
            // 1. Lokace - Upravené souřadnice pro Leaflet mapu (Y, X)
            // Představ si mapu jako čtverec 1000x1500.
            builder.Entity<Location>().HasData(
                new Location
                {
                    Id = 1,
                    Name = "Two Forks Lookout",
                    Description = "Henryho věž. Hlavní stanoviště s výhledem na Shoshone National Forest.",
                    Latitude = 313,  // Y souřadnice (střed)
                    Longitude = 929  // X souřadnice
                },
                new Location
                {
                    Id = 2,
                    Name = "Wapiti Station",
                    Description = "Oplocená biologická výzkumná stanice. Vstup přísně zakázán.",
                    Latitude = 626,
                    Longitude = 411
                },
                new Location
                {
                    Id = 3,
                    Name = "Jonesy Lake",
                    Description = "Klidné jezero na úpatí hor. Ideální místo pro odpočinek.",
                    Latitude = 398,
                    Longitude = 203
                },
                new Location
                {
                    Id = 4,
                    Name = "Thorofare Lookout",
                    Description = "Věž, kde sídlí Delilah. Viditelná pouze dalekohledem.",
                    Latitude = 886,
                    Longitude = 830
                }
            );

            // 2. Tagy (Štítky pro fotky)
            builder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "Příroda" },
                new Tag { Id = 2, Name = "Věž" },
                new Tag { Id = 3, Name = "Zvěř" },
                new Tag { Id = 4, Name = "Západ slunce" },
                new Tag { Id = 5, Name = "Tajemství" }
            );

            // 3. Role (Admin a Customer)
            // NormalizedName musí být VELKÝMI PÍSMENY, jinak nefunguje přihlášení
            builder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" }
            );
        }
    }
}