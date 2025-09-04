using System.Collections.Generic;

namespace Vibrato.Utils
{
    public class DictionaryWrapper<T>
    {
        protected readonly List<Pair<string, T>> data;
        protected readonly Dictionary<string, T> dict;

        public DictionaryWrapper(List<Pair<string, T>> data)
        {
            this.data = data;
            dict = data.ToDictionary();
        }

        public T TryGet(string key)
        {
            if (key == null) return default;
            dict.TryGetValue(key, out var item);
            return item;
        }
    }
}