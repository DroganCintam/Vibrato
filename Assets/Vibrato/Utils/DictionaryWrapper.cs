using System.Collections.Generic;

namespace Vibrato.Utils
{
    public class DictionaryWrapper<T>
    {
        protected readonly List<Pair<string, T>> Data;
        protected readonly Dictionary<string, T> Dict;

        public DictionaryWrapper(List<Pair<string, T>> data)
        {
            Data = data;
            Dict = data.ToDictionary();
        }

        public T TryGet(string key)
        {
            if (key == null) return default;
            Dict.TryGetValue(key, out var item);
            return item;
        }
    }
}