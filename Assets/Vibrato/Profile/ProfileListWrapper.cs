using System.Collections.Generic;

namespace Vibrato.Utils
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
            data.Add(item);
            dict.Add(item.id, item);
        }

        public void Remove(string id)
        {
            if (dict.TryGetValue(id, out var item))
            {
                data.Remove(item);
                dict.Remove(id);
            }
        }

        public void Clear()
        {
            data.Clear();
            dict.Clear();
        }
    }
}