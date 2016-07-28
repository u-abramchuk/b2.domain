namespace b2.Domain.Core
{
    public interface IDomainEntity : IEntity
    {
        string Name { get; }
        string Url { get; }
        string Status { get; }
    }
}