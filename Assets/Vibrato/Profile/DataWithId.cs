using Newtonsoft.Json;
using UnityEngine;
using Vibrato.Utils;

namespace Vibrato.Profile
{
    public abstract class DataWithId : IHasId
    {
        [JsonProperty("id")]
        [field: SerializeField]
        public string id { get; protected set; }

        protected DataWithId(string id)
        {
            this.id = id;
        }
    }
}