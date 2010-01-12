namespace Machine.Specifications.AutoMocking.Core.Observations
{
    public interface IObservationContext
    {
        Dependency dependency_of<Dependency>() where Dependency : class;
        void provide_a_basic_subject_constructor_argument<ArgumentType>(ArgumentType value);
        Contract build_subject<Contract, Class>();
        InterfaceType an<InterfaceType>() where InterfaceType : class;
    }
}