using System;
using System.Numerics;

namespace EntityDemo
{
    public interface IEntityManager
    {
        Guid Create(int typeId);
        void UpdateMetadata(Guid entityId, int index, int type, byte[] data);
        byte[] GetMetadata(Guid entityId, int index, int type);
        (Vector3, Vector3) GetTransform(Guid entityId);
        void UpdateTransform(Guid entityId, Vector3 position, Vector3 velocity);
    }
}