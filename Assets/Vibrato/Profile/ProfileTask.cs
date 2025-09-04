using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Vibrato.Profile
{
    partial class ProfileSection<TProfile>
    {
        /// <summary>
        /// Represents a task that can be performed on a profile section.
        /// </summary>
        /// <remarks>
        /// A task is a unit of work that can be performed on a profile section.
        /// </remarks>
        public abstract class ProfileTask<TSection>
        {
            protected readonly TSection section;
            public readonly List<ProfileTaskCondition> conditions;

            protected ProfileTask(TSection section)
            {
                this.section = section;
                this.conditions = new List<ProfileTaskCondition>();
            }
        }
        
        public abstract class ProfileTask<TSection, TResult> : ProfileTask<TSection>
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
                section.profile.Save();
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

        public abstract class AsyncProfileTask<TSection, TResult> : ProfileTask<TSection>
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
                section.profile.Save();
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
}