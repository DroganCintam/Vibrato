using System.Collections.Generic;
using UnityEngine;

namespace Vibrato.UI
{
    public abstract class ListBase<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private T _itemPrefab;

        [SerializeField]
        private T[] _existingItems;

        protected readonly List<T> _items = new();

        public IList<T> Items => _items;

        protected void Setup()
        {
            Clear();
        }

        protected T SpawnItem()
        {
            T item;
            if (_items.Count < (_existingItems?.Length ?? 0))
            {
                item = _existingItems[_items.Count];
                item.gameObject.SetActive(true);
            }
            else
            {
                item = Instantiate(_itemPrefab.gameObject, _container, false).GetComponent<T>();
            }

            _items.Add(item);
            return item;
        }

        protected void RemoveItem(T item)
        {
            if (_items.Count <= (_existingItems?.Length ?? 0))
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                Destroy(item.gameObject);
            }

            _items.Remove(item);
        }

        public void Clear()
        {
            for (int i = 0; i < _items.Count; ++i)
            {
                RemoveItem(_items[i]);
            }
        }
    }
}