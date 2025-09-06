using System;
using System.Collections.Generic;

namespace Vibrato.Utils
{
    public class ListWrapper<T> where T : IHasId
    {
        protected readonly List<T> Data;
        protected readonly Dictionary<string, T> Dict;

        public ListWrapper(List<T> data)
        {
            Data = data;
            Dict = new Dictionary<string, T>();
            foreach (var item in data)
            {
                Dict[item.Id] = item;
            }
        }

        public int Count => Data.Count;

        public bool Contains(string id)
        {
            return Dict.ContainsKey(id);
        }

        public T TryGet(string id)
        {
            if (id == null) return default;
            Dict.TryGetValue(id, out T item);
            return item;
        }

        public IList<T> GetAll()
        {
            return Data;
        }

        public IList<T> GetAllPredicate(Predicate<T> predicate)
        {
            var result = new List<T>();
            foreach (var item in Data)
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
                dict[item.Id] = item;
            }
            return dict;
        }
    }
}