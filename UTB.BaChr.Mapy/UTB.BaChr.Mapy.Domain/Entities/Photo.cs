using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http; // Nutné pro IFormFile
using UTB.BaChr.Mapy.Domain.Entities.Interfaces;
using UTB.BaChr.Mapy.Domain.Validations;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    public class Photo : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public required string ImageSrc { get; set; }

        [NotMapped]
        [FileContent("image/jpeg", "image/png")]
        public IFormFile? ImageFile { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public int LocationId { get; set; }
        public virtual Location? Location { get; set; }

        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<PhotoTag> PhotoTags { get; set; } = new List<PhotoTag>();
    }
}