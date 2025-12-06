using System.ComponentModel.DataAnnotations.Schema;
using UTB.BaChr.Mapy.Domain.Entities.Interfaces;

namespace UTB.BaChr.Mapy.Domain.Entities
{
    // Tato entita nepotřebuje nutně jedno ID, často má kompozitní klíč,
    // ale pro IEntity<int> jí můžeme dát ID, nebo IEntity neimplementovat a řešit to jinak.
    // Pro jednoduchost implementace repozitářů jí dáme ID.
    public class PhotoTag : IEntity<int>
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Photo))]
        public int PhotoId { get; set; }
        public virtual Photo? Photo { get; set; }

        [ForeignKey(nameof(Tag))]
        public int TagId { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}