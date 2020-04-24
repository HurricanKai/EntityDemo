using System;
using System.Numerics;

namespace EntityDemo
{
    public interface UpdateTransform
    {
        Guid EntityId { get; }
        Vector3 Position { get; }
        Vector3 Velocity { get; }
    }
}