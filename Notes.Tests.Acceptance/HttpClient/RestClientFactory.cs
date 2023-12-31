using RestSharp;

namespace Notes.Tests.Acceptance.Http;

public class RestClientFactory : IRestClientFactory
{
    public RestClient Create(Uri baseUri)
    {
        return new RestClient(baseUri);
    }
}
