using System;
using System.Numerics;

namespace EntityDemo
{
    public interface GetEntityTransformRequest
    {
        Guid EntityId { get; }
    }
    
    public interface GetEntityTransformResponse
    {
        Guid EntityId { get; }

        Vector3 Position { get; }
        Vector3 Velocity { get; }
    }
}