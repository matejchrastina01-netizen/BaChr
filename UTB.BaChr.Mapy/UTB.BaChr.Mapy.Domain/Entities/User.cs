using Microsoft.AspNetCore.Identity;
using UTB.BaChr.Mapy.Domain.Entities.Interfaces;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    // Důležité: public
    public class User : IdentityUser<int>, IEntity<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}