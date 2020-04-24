using System;

namespace EntityDemo
{
    public interface UpdateEntityMetadata
    {
        Guid EntityId { get; }
        int Index { get; }
        int Type { get; }
        byte[] Data { get; }
    }
}