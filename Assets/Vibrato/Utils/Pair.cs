using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vibrato.Utils
{
    [Serializable]
    public class Pair<TKey, TValue>
    {
        [JsonProperty("key")]
        public TKey Key;
        
        [JsonProperty("value")]
        public TValue Value;
        
        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public static class PairExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IList<Pair<TKey, TValue>> list)
        {
            var dict = new Dictionary<TKey, TValue>();
            for (int i = 0; i < list.Count; ++i)
            {
                var p = list[i];
                dict.Add(p.Key, p.Value);
            }
            return dict;
        }

        public static TValue TryGet<TKey, TValue>(this IList<Pair<TKey, TValue>> list, TKey key)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (object.Equals(list[i].Key, key)) return list[i].Value;
            }

            return default;
        }
    }
}