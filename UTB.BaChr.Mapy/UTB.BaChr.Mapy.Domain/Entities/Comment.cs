using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UTB.BaChr.Mapy.Domain.Entities.Interfaces;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    public class Comment : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        public DateTime DateTimeCreated { get; set; } = DateTime.Now;

        // Vazba na Fotku
        [ForeignKey(nameof(Photo))]
        public int PhotoId { get; set; }
        public virtual Photo? Photo { get; set; }

        // Vazba na Autora komentáře
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}