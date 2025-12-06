using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Infrastructure.Database.Seeding; // Zde jsou naše init třídy
using UTB.BaChr.Mapy.Infrastructure.Identity;

namespace UTB.BaChr.Mapy.Infrastructure.Database
{
    public class MapyDbContext : IdentityDbContext<User, Role, int>
    {
        public MapyDbContext(DbContextOptions options) : base(options)
        {
        }

        // --- Definice tabulek (DbSet) ---
        public DbSet<Location> Locations { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PhotoTag> PhotoTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Přejmenování tabulek Identity na hezčí názvy (volitelné, ale doporučené)
            builder.Entity<User>().ToTable(nameof(Users));
            builder.Entity<Role>().ToTable(nameof(Roles));
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            // --- KONFIGURACE VAZEB (pokud nejsou plně definované atributy) ---

            // Kompozitní klíč pro vazební tabulku PhotoTag (M:N)
            // Pokud používáš Id v entitě PhotoTag, tento řádek není nutný, ale nic nezkazí.
            // Pokud Id nemáš, je tento řádek povinný.
            // builder.Entity<PhotoTag>().HasKey(pt => new { pt.PhotoId, pt.TagId });

            // --- DATA SEEDING (Počáteční data) ---

            // 1. Role (Admin, Customer)
            // Vytvořili jsme pro to pomocnou třídu RolesInit, nebo to můžeme dát přímo sem.
            // Zde je varianta přímo v kódu pro jednoduchost a jistotu:
            builder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            // 2. Uživatelé (Admin a Klient)
            // Voláme metodu z UserInit.cs, kterou jsi vytvořil v Infrastructure
            builder.Entity<User>().HasData(UserInit.GetUsers());

            // 3. Přiřazení Rolí Uživatelům (Vazba UserId <-> RoleId)
            // Voláme metodu z UserRolesInit.cs
            builder.Entity<IdentityUserRole<int>>().HasData(UserRolesInit.GetRoles());

            // 4. Lokace (Firewatch mapa)
            // Zde jsou souřadnice, které si případně upravíš podle klikání na mapu
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

            // 5. Tagy
            builder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "Příroda" },
                new Tag { Id = 2, Name = "Věž" },
                new Tag { Id = 3, Name = "Zvěř" },
                new Tag { Id = 4, Name = "Západ slunce" },
                new Tag { Id = 5, Name = "Jezero" }
            );
        }
    }
}