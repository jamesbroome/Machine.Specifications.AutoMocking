namespace Machine.Specifications.AutoMocking.Rhino
{
    public abstract class Specification<TSubject> : Specification<TSubject, TSubject>
    {
    }

    public abstract class Specification<TContract, TSubject> :
        Specification<TContract, TSubject, RhinoMocksMockFactory> where TSubject : TContract
    {
    }
}