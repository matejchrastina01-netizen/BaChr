using System.ComponentModel.DataAnnotations;
using UTB.BaChr.Mapy.Domain.Entities.Interfaces;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    public class Tag : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } // Např. "Zima"

        public virtual ICollection<PhotoTag> PhotoTags { get; set; }
    }
}