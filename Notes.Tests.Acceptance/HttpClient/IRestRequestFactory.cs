using RestSharp;

namespace Notes.Tests.Acceptance.Http;

public interface IRestRequestFactory
{
    public RestRequest createGetRequest(string url, IDictionary<string, object>? queryParams = null, string? authToken = null);
    public RestRequest createPostRequest(string url, object? requestData = null, IDictionary<string, object>? queryParams = null, string? authToken = null);
}
