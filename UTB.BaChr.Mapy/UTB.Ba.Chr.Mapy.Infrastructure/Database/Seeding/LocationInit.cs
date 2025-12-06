using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Infrastructure.Database.Seeding
{
    public class LocationInit
    {

        public List<Location> GenerateLocation()
        {
            List<Location> locations = new List<Location>();

            var location1 = new Location()
            {
                Id = 1,
                Name = "Two Forks Lookout",
                Description = "Lookout",
                Latitude = 44.12,   // <-- TOTO PŘIDAT
                Longitude = -109.55 // <-- TOTO PŘIDAT
            };

            locations.Add(location1);

            return locations;
        }
    }
}
