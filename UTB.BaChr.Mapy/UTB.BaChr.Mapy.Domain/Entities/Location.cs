using System.ComponentModel.DataAnnotations;
using UTB.BaChr.Mapy.Domain.Entities.Interfaces;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    public class Location : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; } // required vyřeší varování

        public string? Description { get; set; } // ? znamená, že může být null

        // Tady opravíme chybějící definice
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}