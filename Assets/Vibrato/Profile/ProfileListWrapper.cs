using System.Collections.Generic;
using Vibrato.Utils;

namespace Vibrato.Profile
{
    public class ProfileListWrapper<T> : ListWrapper<T>
        where T : IHasId
    {
        public ProfileListWrapper(List<T> data)
            : base(data)
        {
        }

        public void Add(T item)
        {
            Data.Add(item);
            Dict.Add(item.Id, item);
        }

        public void Remove(string id)
        {
            if (Dict.TryGetValue(id, out var item))
            {
                Data.Remove(item);
                Dict.Remove(id);
            }
        }

        public void Clear()
        {
            Data.Clear();
            Dict.Clear();
        }
    }
}