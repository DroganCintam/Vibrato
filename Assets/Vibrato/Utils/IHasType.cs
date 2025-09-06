namespace Vibrato.Utils
{
    public interface IHasType<out TType>
    {
        TType Type { get; }
    }
}