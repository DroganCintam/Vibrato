using System;
using System.Collections.Generic;

namespace Vibrato.Utils
{
    public class ListWrapper<T> where T : IHasId
    {
        protected readonly List<T> data;
        protected readonly Dictionary<string, T> dict;

        public ListWrapper(List<T> data)
        {
            this.data = data;
            dict = new Dictionary<string, T>();
            foreach (var item in data)
            {
                dict[item.id] = item;
            }
        }

        public int Count => data.Count;

        public bool Contains(string id)
        {
            return dict.ContainsKey(id);
        }

        public T TryGet(string id)
        {
            if (id == null) return default;
            dict.TryGetValue(id, out T item);
            return item;
        }

        public IList<T> GetAll()
        {
            return data;
        }

        public IList<T> GetAllPredicate(Predicate<T> predicate)
        {
            var result = new List<T>();
            foreach (var item in data)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }

    public static class ListExtensions
    {
        public static Dictionary<string, T> ToDictionary<T>(this List<T> list)
            where T : IHasId
        {
            var dict = new Dictionary<string, T>();
            foreach (var item in list)
            {
                dict[item.id] = item;
            }
            return dict;
        }
    }
}