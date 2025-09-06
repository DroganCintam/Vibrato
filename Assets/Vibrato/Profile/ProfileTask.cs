using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Vibrato.Profile
{
    /// <summary>
    /// Represents a task that can be performed on a profile section.
    /// </summary>
    /// <remarks>
    /// A task is a unit of work that can be performed on a profile section.
    /// </remarks>
    public abstract class ProfileTask
    {
        public readonly List<ProfileTaskCondition> Conditions;

        protected ProfileTask()
        {
            Conditions = new List<ProfileTaskCondition>();
        }
    }
    
    public abstract class ProfileTask<TProfile, TSection> : ProfileTask
        where TProfile : BaseProfile<TProfile>
        where TSection : ProfileSection<TProfile>
    {
        protected readonly TSection Section;

        protected TProfile Profile => Section.Profile;

        protected ProfileTask(TSection section)
            : base()
        {
            Section = section;
        }
    }
    
    public abstract class ProfileTask<TProfile, TSection, TResult> : ProfileTask<TProfile, TSection>
        where TProfile : BaseProfile<TProfile>
        where TSection : ProfileSection<TProfile>
    {
        protected ProfileTask(TSection section)
            : base(section)
        {
        }

        public TResult Proceed()
        {
            if (!this.CanProceed()) throw new ProfileTaskConditionNotSatisfiedException();

            var result = ProceedInternal();
            Profile.Save();
            return result;
        }

        /// <summary>
        /// Performs the task.
        /// </summary>
        /// <returns>The result of the task.</returns>
        /// <remarks>
        /// Implement the task logic here.
        /// </remarks>
        protected abstract TResult ProceedInternal();
    }

    public abstract class AsyncProfileTask<TProfile, TSection, TResult> : ProfileTask<TProfile, TSection>
        where TProfile : BaseProfile<TProfile>
        where TSection : ProfileSection<TProfile>
    {
        protected AsyncProfileTask(TSection section)
            : base(section)
        {
        }

        public async UniTask<TResult> Proceed()
        {
            if (!this.CanProceed()) throw new ProfileTaskConditionNotSatisfiedException();

            var result = await ProceedInternal();
            Profile.Save();
            return result;
        }

        /// <summary>
        /// Performs the task.
        /// </summary>
        /// <returns>The result of the task.</returns>
        /// <remarks>
        /// Implement the task logic here.
        /// </remarks>
        protected abstract UniTask<TResult> ProceedInternal();
    }

    public sealed class ProfileTaskConditionNotSatisfiedException : Exception
    {
    }
}