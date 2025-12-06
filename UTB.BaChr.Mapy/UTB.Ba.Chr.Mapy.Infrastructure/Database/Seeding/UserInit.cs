using Microsoft.AspNetCore.Identity;
using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Infrastructure.Database.Seeding
{
    public static class UserInit
    {
        public static List<User> GetUsers()
        {
            var users = new List<User>
            {
                // 1. ADMIN
                new User
                {
                    Id = 1, // Důležité: Pevné ID pro vazbu
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@admin.cz",
                    NormalizedEmail = "ADMIN@ADMIN.CZ",
                    EmailConfirmed = true,
                    FirstName = "Hlavní",
                    LastName = "Administrátor",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                },
                // 2. KLIENT (Zákazník)
                new User
                {
                    Id = 2,
                    UserName = "klient",
                    NormalizedUserName = "KLIENT",
                    Email = "klient@klient.cz",
                    NormalizedEmail = "KLIENT@KLIENT.CZ",
                    EmailConfirmed = true,
                    FirstName = "Jan",
                    LastName = "Novák",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                }
            };

            // Nastavení hesla (hash)
            var passwordHasher = new PasswordHasher<User>();
            foreach (var user in users)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, "Admin123!");
            }

            return users;
        }
    }
}