using System;

namespace EntityDemo
{
    public interface CreateEntityRequest
    {
        public int TypeId { get; }
    }

    public interface CreateEntityResponse
    {
        public Guid EntityId { get; }
    }
}