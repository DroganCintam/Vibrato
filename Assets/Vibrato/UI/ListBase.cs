using System.Collections.Generic;
using UnityEngine;

namespace Vibrato.UI
{
    public abstract class ListBase<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        [SerializeField]
        private Transform container;

        [SerializeField]
        private T itemPrefab;

        [SerializeField]
        private T[] existingItems;

        public readonly List<T> items = new();

        protected void Setup()
        {
            Clear();
        }

        protected T SpawnItem()
        {
            T item;
            if (items.Count < (existingItems?.Length ?? 0))
            {
                item = existingItems[items.Count];
                item.gameObject.SetActive(true);
            }
            else
            {
                item = Instantiate(itemPrefab.gameObject, container, false).GetComponent<T>();
            }

            items.Add(item);
            return item;
        }

        protected void RemoveItem(T item)
        {
            if (items.Count <= (existingItems?.Length ?? 0))
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                Destroy(item.gameObject);
            }

            items.Remove(item);
        }

        public void Clear()
        {
            for (int i = 0; i < items.Count; ++i)
            {
                RemoveItem(items[i]);
            }
        }
    }
}