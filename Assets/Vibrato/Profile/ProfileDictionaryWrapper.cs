using System.Collections.Generic;

namespace Vibrato.Utils
{
    public class ProfileDictionaryWrapper<T> : DictionaryWrapper<T>
    {
        public ProfileDictionaryWrapper(List<Pair<string, T>> data)
            : base(data)
        {
        }

        public void Add(string key, T item)
        {
            data.Add(new Pair<string, T>(key, item));
            dict.Add(key, item);
        }

        public void Remove(string key)
        {
            if (dict.TryGetValue(key, out var item))
            {
                data.Remove(new Pair<string, T>(key, item));
                dict.Remove(key);
            }
        }

        public void Update(string key, T item)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = item;
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].Key == key)
                    {
                        data[i].Value = item;
                        break;
                    }
                }
            }
            else
            {
                Add(key, item);
            }
        }
    }
}