using Notes.Tests.Acceptance.Authorization;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using NUnit.Framework;
using RestSharp;

namespace Notes.Tests.Acceptance.StepDefinitions;

[Binding]
public class DeleteNoteNegativeStepDefinitions
{
    private RestResponse _response;
    private string _url;
    private IRestResponseFactory _restResponseFactory;
    private IRestRequestFactory _restRequestFactory;
    private string _token;

    public DeleteNoteNegativeStepDefinitions(IRestRequestFactory requestFactory, IRestResponseFactory restResponse, IAuthorizationFactory authorizationFactory)
    {
        _restResponseFactory = restResponse;
        _restRequestFactory = requestFactory;
        _token = authorizationFactory.Auth();
    }

    [Given(@"The URL to delete note is ""([^""]*)""")]
    public void GivenTheURLToDeleteNoteIs(string url)
    {
        _url = url;
    }

    [When(@"I send request to this URL with following id ""([^""]*)""")]
    public void WhenISendRequestToThisURLWithFollowingId(string id)
    {
        _url = _url.Replace("{id}", id);

        RestRequest request = _restRequestFactory.createDeleteRequest(_url, authToken: _token);
        _response = _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));
    }

    [Then(@"I should get response with error status code")]
    public void ThenIShouldGetResponseWithErrorStatusCode()
    {
        Assert.False(_response.IsSuccessStatusCode);
    }
}
