using System;

namespace EntityDemo
{
    public interface IEntityFactory
    {
        Entity Create(Guid guid);
    }
}