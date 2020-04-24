namespace EntityDemo
{
    public interface IEntityResolver
    {
        IEntityFactory GetFactory(int id);
    }
}