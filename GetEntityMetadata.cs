using System;

namespace EntityDemo
{
    public interface GetEntityMetadataRequest
    {
        Guid EntityId { get; }
        int Index { get; }
        int Type { get; }
    }

    public interface GetEntityMetadataResponse
    {
        Guid EntityId { get; }
        int Index { get; }
        int Type { get; }
        byte[] Data { get; }
    }
}