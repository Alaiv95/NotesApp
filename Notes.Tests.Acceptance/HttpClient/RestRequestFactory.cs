using Microsoft.VisualStudio.TestPlatform.Common;
using RestSharp;

namespace Notes.Tests.Acceptance.Http;

public class RestRequestFactory : IRestRequestFactory
{
    public RestRequest createGetRequest(string url, IDictionary<string, object>? queryParams = null, string? authToken = null)
    {
        RestRequest request = new RestRequest(url) { Method = Method.Get };

        return RequestWithParameters(request, authToken, queryParams);
    }

    public RestRequest createPostRequest(string url, object? requestData = null, IDictionary<string, object>? queryParams = null, string? authToken = null)
    {
        RestRequest request = new RestRequest(url) 
        { 
            Method = Method.Post,
            RequestFormat = DataFormat.Json
        };

        request = requestData == null ? request : request.AddBody(requestData);

        return RequestWithParameters(request, authToken, queryParams);
    }

    private RestRequest RequestWithParameters(RestRequest request, string? authToken, IDictionary<string, object>? queryParams)
    {
        if (authToken is not null)
        {
            request.AddHeader("Authorization", authToken);
        }

        if (queryParams is null || !queryParams.Any())
        {
            return request;
        }

        foreach (var queryParam in queryParams)
        {
            request.AddQueryParameter(queryParam.Key, $"{queryParam.Value}");
        }

        return request;
    }
}
