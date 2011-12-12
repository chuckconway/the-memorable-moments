namespace Momntz
{
    public interface IPrimaryKey<T>
    {
        T Id { get; set; }
    }
}
