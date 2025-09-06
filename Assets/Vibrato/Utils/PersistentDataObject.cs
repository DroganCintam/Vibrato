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
        public string Filename { get; protected set; } = "p.dat";

        [field: SerializeField]
        public TData Data { get; protected set; }

        private string _cachedPath;

        public TData Load()
        {
            _cachedPath = Path.Combine(PersistentPath.Get(), Filename);
            if (!File.Exists(_cachedPath))
            {
                Debug.Log($"{Filename} does not exist, creating new");
                Data = new TData();
                
                string directory = Path.GetDirectoryName(_cachedPath);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                return Data;
            }

            try
            {
                var json = File.ReadAllText(_cachedPath);
                Data = JsonConvert.DeserializeObject<TData>(json, new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                });
            }
            catch (Exception e)
            {
                Debug.LogError($"Cannot load {Filename}: {e.Message}");
                Debug.LogError(e.StackTrace);

                Data = new TData();
            }

            return Data;
        }

        public void Save()
        {
            try
            {
                var json = JsonConvert.SerializeObject(Data);
                File.WriteAllText(_cachedPath, json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Cannot save {Filename}: {e.Message}");
                Debug.LogError(e.StackTrace);
            }
        }
    }
}