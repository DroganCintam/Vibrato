using Newtonsoft.Json;
using Vibrato.Utils;

namespace Vibrato.Profile
{
    /// <summary>
    /// Represents a section in the user profile.
    /// </summary>
    /// <typeparam name="TProfile">The profile class that this section belongs to.</typeparam>
    /// <remarks>
    /// A section stores data related to a specific feature of the user profile.
    /// </remarks>
    public abstract partial class ProfileSection<TProfile>
        where TProfile : BaseProfile<TProfile>
    {
        [JsonIgnore]
        protected TProfile profile => DataSingleton<TProfile>.instance;

        /// <summary>
        /// Initializes the section.
        /// </summary>
        /// <remarks>
        /// Call this method in <see cref="BaseProfile{TDerived}.Initialize(IProfileSaver)"/>.
        /// </remarks>
        public virtual void Initialize() {}
    }
}