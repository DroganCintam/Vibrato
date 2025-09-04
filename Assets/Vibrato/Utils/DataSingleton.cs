namespace Vibrato.Utils
{
    public abstract class DataSingleton<T>
        where T : class
    {
        public static T instance { get; private set; }

        public static void Setup(T inst)
        {
            instance = inst;
        }

        public static void Shutdown()
        {
            instance = null;
        }
    }
}