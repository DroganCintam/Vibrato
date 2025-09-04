using System.IO;
using UnityEngine;

namespace Vibrato.Utils
{
    public static class PersistentPath
    {
        public static string Get()
        {
#if UNITY_EDITOR
            return Path.Combine(Application.dataPath, "../Library/[Data]");
#else
            return Application.persistentDataPath;
#endif
        }
    }
}