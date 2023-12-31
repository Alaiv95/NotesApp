using Notes.Tests.Acceptance.Utils;
using RestSharp;
using System.Text.Json;

namespace Notes.Tests.Acceptance.Http;

public class RestResponseFactory : IRestResponseFactory
{
    private IRestClientFactory _clientFactory;
    private JsonSerializerOptions _options;

    public RestResponseFactory(IRestClientFactory client)
    {
        _clientFactory = client;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public T GetDeserializedResponse<T>(RestRequest request, Uri baseUri)
    {
        RestClient restClient = _clientFactory.Create(baseUri);
        RestResponse response = restClient.Execute(request);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new Exception($"Request failed with status code {response.StatusCode}");
        }

        return response.DeserializeContent<T>(_options);
    }

    public RestResponse GetRestResponse(RestRequest request, Uri baseUri)
    {
        RestClient restClient = _clientFactory.Create(baseUri);
        return restClient.Execute(request);
    }
}
