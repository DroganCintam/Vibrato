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
        [JsonProperty("items")]
        [SerializeField]
        protected List<TItem> list = new();

        [JsonIgnore]
        protected Dictionary<string, TItem> itemById;
        
        [JsonIgnore]
        protected Dictionary<TItemType, TItem> singleItemByType;
        
        [JsonIgnore]
        protected Dictionary<TItemType, List<TItem>> multipleItemByType;

        protected void EnsureMapping()
        {
            if (itemById != null) return;

            itemById = new Dictionary<string, TItem>();
            singleItemByType = new Dictionary<TItemType, TItem>();
            multipleItemByType = new Dictionary<TItemType, List<TItem>>();
            
            for (int i = 0; i < list.Count; ++i)
            {
                var item = list[i];
                itemById.Add(item.id, item);
                singleItemByType[item.type] = item;
                if (!multipleItemByType.TryGetValue(item.type, out var typedList))
                {
                    typedList = new List<TItem>();
                    multipleItemByType.Add(item.type, typedList);
                }
                typedList.Add(item);
            }
        }

        public List<TItem> GetAll()
        {
            return list;
        }

        public TItem TryGetById(string id)
        {
            EnsureMapping();
            
            return itemById.GetValueOrDefault(id);
        }

        public TItem GetSingleByType(TItemType type)
        {
            EnsureMapping();
            
            return singleItemByType.GetValueOrDefault(type);
        }

        public List<TItem> GetMultipleByType(TItemType type)
        {
            EnsureMapping();
            
            if (!multipleItemByType.TryGetValue(type, out var list))
            {
                list = new List<TItem>();
                multipleItemByType.Add(type, list);
            }
            return list;
        }

        public bool Add(TItem item, out TItem existingItem)
        {
            EnsureMapping();
            
            if (itemById.TryGetValue(item.id, out existingItem))
            {
                return false;
            }

            list.Add(item);
            itemById.Add(item.id, item);
            singleItemByType.TryAdd(item.type, item);
            GetMultipleByType(item.type).Add(item);
            
            return true;
        }
    }
}