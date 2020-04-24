using System;

namespace EntityDemo
{
    public interface IEntityManager
    {
        Guid Create(int typeId);
        void UpdateMetadata(Guid entityId, int index, int type, byte[] data);
        byte[] GetMetadata(Guid entityId, int index, int type);
    }
}