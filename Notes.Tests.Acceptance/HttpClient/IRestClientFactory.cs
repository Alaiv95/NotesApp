using RestSharp;

namespace Notes.Tests.Acceptance.Http;

public interface IRestClientFactory
{
    RestClient Create(Uri baseUri);
}
