namespace Vibrato.Utils
{
    public interface IHasType<out TType>
    {
        TType type { get; }
    }
}