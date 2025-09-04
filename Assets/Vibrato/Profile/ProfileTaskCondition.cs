using System.Linq;

namespace Vibrato.Profile
{
    public abstract class ProfileTaskCondition
    {
        public abstract bool satisfied { get; }
    }

    public sealed class ObjectExistsCondition<T> : ProfileTaskCondition
        where T : class
    {
        public readonly T obj;

        public ObjectExistsCondition(T obj)
        {
            this.obj = obj;
        }

        public override bool satisfied => obj != null;
    }

    public static class ProfileTaskConditionExtensions
    {
        public static bool AddCondition<TProfile, TSection, TCondition>(
                this ProfileSection<TProfile>.ProfileTask<TSection> task, TCondition condition)
            where TProfile : BaseProfile<TProfile>
            where TSection : ProfileSection<TProfile>
            where TCondition : ProfileTaskCondition
        {
            task.conditions.Add(condition);
            return condition.satisfied;
        }

        public static bool CanProceed<TProfile, TSection>(
                this ProfileSection<TProfile>.ProfileTask<TSection> task)
            where TProfile : BaseProfile<TProfile>
            where TSection : ProfileSection<TProfile>
        {
            return task.conditions.All(condition => condition.satisfied);
        }

        public static TCondition GetCondition<TProfile, TSection, TCondition>(
                this ProfileSection<TProfile>.ProfileTask<TSection> task)
            where TProfile : BaseProfile<TProfile>
            where TSection : ProfileSection<TProfile>
            where TCondition : ProfileTaskCondition
        {
            return task.conditions.OfType<TCondition>().FirstOrDefault();
        }
    }
}