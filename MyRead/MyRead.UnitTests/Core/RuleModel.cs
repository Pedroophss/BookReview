namespace MyRead.UnitTests.Core
{
    public class RuleModel<T>
    {
        public T Object { get; }

        public RuleModel(T obj) =>
            Object = obj;

        public static implicit operator T(RuleModel<T> model) =>
            model.Object;
    }
}