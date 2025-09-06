using System.Collections.Generic;
using Vibrato.Utils;

namespace Vibrato.Profile
{
    public class ProfileDictionaryWrapper<T> : DictionaryWrapper<T>
    {
        public ProfileDictionaryWrapper(List<Pair<string, T>> data)
            : base(data)
        {
        }

        public void Add(string key, T item)
        {
            Data.Add(new Pair<string, T>(key, item));
            Dict.Add(key, item);
        }

        public void Remove(string key)
        {
            if (Dict.TryGetValue(key, out var item))
            {
                Data.Remove(new Pair<string, T>(key, item));
                Dict.Remove(key);
            }
        }

        public void Update(string key, T item)
        {
            if (Dict.ContainsKey(key))
            {
                Dict[key] = item;
                for (int i = 0; i < Data.Count; i++)
                {
                    if (Data[i].Key == key)
                    {
                        Data[i].Value = item;
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