using BoDi;
using Notes.Tests.Acceptance.Authorization;
using Notes.Tests.Acceptance.Http;
using TechTalk.SpecFlow;

namespace Notes.Tests.Acceptance
{
    [Binding]
    public sealed class SpecFlowHooks
    {
        private static IRestRequestFactory _restRequestFactory;
        private static IRestResponseFactory _restResponseFactory;
        private static IAuthorizationFactory _authorizationFactory;
        private readonly IObjectContainer _container;

        public SpecFlowHooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _restRequestFactory = new RestRequestFactory();
            _restResponseFactory = new RestResponseFactory(new RestClientFactory());
            _authorizationFactory = new AuthorizationFactory();
        }

        [BeforeScenario]
        public void FirstBeforeScenario()
        {
            _container.RegisterInstanceAs<IRestRequestFactory>(_restRequestFactory);
            _container.RegisterInstanceAs<IRestResponseFactory>(_restResponseFactory);
            _container.RegisterInstanceAs<IAuthorizationFactory>(_authorizationFactory);
        }
    }
}