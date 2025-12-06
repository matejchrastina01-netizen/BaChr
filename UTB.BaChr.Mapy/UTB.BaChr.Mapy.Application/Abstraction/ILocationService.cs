using System.Collections.Generic;
using UTB.BaChr.Mapy.Domain.Entities; // Tady musí být jen namespace, ne název třídy!

namespace UTB.BaChr.Mapy.Application.Abstraction
{
    public interface ILocationService
    {
        IList<Location> Select();
        Location? GetById(int id);
        void Create(Location location);
        void Update(Location location);
        bool Delete(int id);
    }
}