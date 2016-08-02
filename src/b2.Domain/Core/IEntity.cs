using System;

namespace b2.Domain.Core 
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}