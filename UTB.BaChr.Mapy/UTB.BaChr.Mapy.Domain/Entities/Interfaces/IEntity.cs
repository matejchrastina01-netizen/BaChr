namespace UTB.BaChr.Mapy.Domain.Entities.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}