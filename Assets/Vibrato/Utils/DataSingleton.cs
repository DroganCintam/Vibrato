namespace Vibrato.Utils
{
    public abstract class DataSingleton<T>
        where T : class
    {
        public static T Instance { get; private set; }

        public static void Setup(T instance)
        {
            Instance = instance;
        }

        public static void Shutdown()
        {
            Instance = null;
        }
    }
}