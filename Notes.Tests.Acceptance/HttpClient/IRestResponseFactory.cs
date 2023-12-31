using RestSharp;

namespace Notes.Tests.Acceptance.Http;

public interface IRestResponseFactory
{
    public T GetDeserializedResponse<T>(RestRequest request, Uri baseUri);
    public RestResponse GetRestResponse(RestRequest request, Uri baseUri);
}
