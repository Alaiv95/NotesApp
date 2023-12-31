using BoDi;
using Notes.Tests.Acceptance.Http;
using TechTalk.SpecFlow;

namespace Notes.Tests.Acceptance
{
    [Binding]
    public sealed class SpecFlowHooks
    {
        private static IRestRequestFactory _restRequestFactory;
        private static IRestResponseFactory _restResponseFactory;
        private readonly IObjectContainer _container;

        public SpecFlowHooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeTestRun]
        public void BeforeTestRun()
        {
            _restRequestFactory = new RestRequestFactory();
            _restResponseFactory = new RestResponseFactory(new RestClientFactory());
        }

        [BeforeScenario]
        public void FirstBeforeScenario()
        {
            _container.RegisterInstanceAs<IRestRequestFactory>(_restRequestFactory);
            _container.RegisterInstanceAs<IRestResponseFactory>(_restResponseFactory);
        }
    }
}