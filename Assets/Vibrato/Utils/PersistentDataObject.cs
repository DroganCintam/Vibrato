using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Vibrato.Utils
{
    public abstract class PersistentDataObject<TData> : ScriptableObject
        where TData : class, new()
    {
        [field: SerializeField]
        public string filename { get; protected set; } = "p.dat";

        [field: SerializeField]
        public TData data { get; protected set; }

        private string cachedPath;

        public TData Load()
        {
            cachedPath = Path.Combine(PersistentPath.Get(), filename);
            if (!File.Exists(cachedPath))
            {
                Debug.Log($"{filename} does not exist, creating new");
                data = new TData();
                
                string directory = Path.GetDirectoryName(cachedPath);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                return data;
            }

            try
            {
                var json = File.ReadAllText(cachedPath);
                data = JsonConvert.DeserializeObject<TData>(json, new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                });
            }
            catch (Exception e)
            {
                Debug.LogError($"Cannot load {filename}: {e.Message}");
                Debug.LogError(e.StackTrace);

                data = new TData();
            }

            return data;
        }

        public void Save()
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                File.WriteAllText(cachedPath, json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Cannot save {filename}: {e.Message}");
                Debug.LogError(e.StackTrace);
            }
        }
    }
}