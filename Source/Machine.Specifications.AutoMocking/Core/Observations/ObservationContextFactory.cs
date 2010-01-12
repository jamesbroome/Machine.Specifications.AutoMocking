namespace Machine.Specifications.AutoMocking.Core.Observations
{
    public class ObservationContextFactory
    {
        public ObservationContext<Contract> create_from<Contract>(ObservationContextArgs<Contract> args)
        {
            var dependency_builder = new SubjectDependencyBuilder(args.state, args.mock_factory);

            return new ObservationContext<Contract>(
                args.state,
                dependency_builder,
                args.mock_factory,
                new SubjectFactory(dependency_builder));
        }
    }
}