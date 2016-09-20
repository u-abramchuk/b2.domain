using System;

namespace b2.Domain.Core
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}