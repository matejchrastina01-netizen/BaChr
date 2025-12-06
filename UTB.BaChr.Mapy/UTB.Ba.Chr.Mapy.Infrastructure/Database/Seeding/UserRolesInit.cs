using Microsoft.AspNetCore.Identity;

namespace UTB.BaChr.Mapy.Infrastructure.Database.Seeding
{
    public static class UserRolesInit
    {
        public static List<IdentityUserRole<int>> GetRoles()
        {
            var userRoles = new List<IdentityUserRole<int>>
            {
                // Propojení Admina (Id=1) s Rolí Admin (Id=1)
                new IdentityUserRole<int>
                {
                    UserId = 1,
                    RoleId = 1
                },
                // Propojení Klienta (Id=2) s Rolí Customer (Id=2)
                new IdentityUserRole<int>
                {
                    UserId = 2,
                    RoleId = 2
                }
            };

            return userRoles;
        }
    }
}