namespace Momntz
{
    public interface IPrimaryKey<out T>
    {
        T Id { get; }
    }
}
