using Newtonsoft.Json;
using UnityEngine;
using Vibrato.Utils;

namespace Vibrato.Profile
{
    /// <summary>
    /// The base class for user profile.
    /// </summary>
    /// <typeparam name="TDerived">The actual profile class that inherits from this one.</typeparam>
    public abstract class BaseProfile<TDerived> : DataSingleton<TDerived>
        where TDerived : class
    {
        [JsonProperty("created_at")]
        [SerializeField]
        protected int _createdAt;
        
        [JsonIgnore]
        public int CreatedAt => _createdAt;
        
        [JsonIgnore]
        public IProfileSaver Saver { get; private set; }

        /// <summary>
        /// Initializes the profile.
        /// </summary>
        /// <param name="saver">The service responsible for saving the profile.</param>
        /// <remarks>
        /// Call this method after successfully loaded the profile from the storage.
        /// </remarks>
        public virtual void Initialize(IProfileSaver saver)
        {
            Saver = saver;

            InitializeSections();

            if (_createdAt == 0)
            {
                _createdAt = GetCurrentTime();
                InitializeAtCreation();
            }
        }

        /// <summary>
        /// Initializes profile sections.
        /// </summary>
        protected abstract void InitializeSections();

        /// <summary>
        /// Initializes the profile at creation.
        /// </summary>
        /// <remarks>
        /// Implement profile creation logic here.
        /// </remarks>
        protected abstract void InitializeAtCreation();

        public abstract int GetCurrentTime();

        public void Save()
        {
            Saver?.Save();
        }
    }
}