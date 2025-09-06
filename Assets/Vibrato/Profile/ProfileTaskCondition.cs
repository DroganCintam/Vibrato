using System.Linq;

namespace Vibrato.Profile
{
    public abstract class ProfileTaskCondition
    {
        public abstract bool Satisfied { get; }
    }

    public sealed class ObjectExistsCondition<T> : ProfileTaskCondition
        where T : class
    {
        public readonly T Object;

        public ObjectExistsCondition(T obj)
        {
            Object = obj;
        }

        public override bool Satisfied => Object != null;
    }

    public static class ProfileTaskConditionExtensions
    {
        public static bool AddCondition<TProfile, TSection, TCondition>(
                this ProfileTask<TProfile, TSection> task, TCondition condition)
            where TProfile : BaseProfile<TProfile>
            where TSection : ProfileSection<TProfile>
            where TCondition : ProfileTaskCondition
        {
            task.Conditions.Add(condition);
            return condition.Satisfied;
        }

        public static bool CanProceed(this ProfileTask task)
        {
            return task.Conditions.All(condition => condition.Satisfied);
        }

        public static TCondition GetCondition<TCondition>(
                this ProfileTask task)
            where TCondition : ProfileTaskCondition
        {
            return task.Conditions.OfType<TCondition>().FirstOrDefault();
        }
    }
}