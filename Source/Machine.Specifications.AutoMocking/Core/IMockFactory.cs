namespace Machine.Specifications.AutoMocking.Core
{
    using System;

    public interface IMockFactory
    {
        Dependency create_stub<Dependency>() where Dependency : class;
        object create_stub(Type type);
    }
}