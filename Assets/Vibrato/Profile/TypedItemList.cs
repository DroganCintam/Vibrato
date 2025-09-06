using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Vibrato.Utils;

namespace Vibrato.Profile
{
    [Serializable]
    public class TypedItemList<TItem, TItemType>
        where TItem : IHasType<TItemType>, IHasId
    {
        [JsonProperty("list")]
        [SerializeField]
        protected List<TItem> _list = new();

        [JsonIgnore]
        protected Dictionary<string, TItem> _itemById;
        
        [JsonIgnore]
        protected Dictionary<TItemType, TItem> _singleItemByType;
        
        [JsonIgnore]
        protected Dictionary<TItemType, List<TItem>> _multipleItemByType;

        protected void EnsureMapping()
        {
            if (_itemById != null) return;

            _itemById = new Dictionary<string, TItem>();
            _singleItemByType = new Dictionary<TItemType, TItem>();
            _multipleItemByType = new Dictionary<TItemType, List<TItem>>();
            
            for (int i = 0; i < _list.Count; ++i)
            {
                var item = _list[i];
                _itemById.Add(item.Id, item);
                _singleItemByType[item.Type] = item;
                if (!_multipleItemByType.TryGetValue(item.Type, out var typedList))
                {
                    typedList = new List<TItem>();
                    _multipleItemByType.Add(item.Type, typedList);
                }
                typedList.Add(item);
            }
        }

        public List<TItem> GetAll()
        {
            return _list;
        }

        public TItem TryGetById(string id)
        {
            EnsureMapping();
            
            return _itemById.GetValueOrDefault(id);
        }

        public TItem GetSingleByType(TItemType type)
        {
            EnsureMapping();
            
            return _singleItemByType.GetValueOrDefault(type);
        }

        public List<TItem> GetMultipleByType(TItemType type)
        {
            EnsureMapping();
            
            if (!_multipleItemByType.TryGetValue(type, out var list))
            {
                list = new List<TItem>();
                _multipleItemByType.Add(type, list);
            }
            return list;
        }

        public bool Add(TItem item, out TItem existingItem)
        {
            EnsureMapping();
            
            if (_itemById.TryGetValue(item.Id, out existingItem))
            {
                return false;
            }

            _list.Add(item);
            _itemById.Add(item.Id, item);
            _singleItemByType.TryAdd(item.Type, item);
            GetMultipleByType(item.Type).Add(item);
            
            return true;
        }
    }
}