using Microsoft.AspNetCore.Identity;
using UTB.BaChr.Mapy.Domain.Entities.Interfaces;

namespace UTB.BaChr.Mapy.Infrastructure.Identity
{
    // Musí dědit od IdentityRole<int> aby fungovala v DbContextu
    public class Role : IdentityRole<int>, IEntity<int>
    {
    }
}