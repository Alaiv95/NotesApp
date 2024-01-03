using Notes.Tests.Acceptance.Authorization;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using NUnit.Framework;
using RestSharp;

namespace Notes.Tests.Acceptance.StepDefinitions;

[Binding]
public class GetNoteDetailNegativeStepDefinitions
{
    private RestResponse _response;
    private string _url;
    private IRestResponseFactory _restResponseFactory;
    private IRestRequestFactory _restRequestFactory;
    private string _token;

    public GetNoteDetailNegativeStepDefinitions(IRestRequestFactory requestFactory, IRestResponseFactory restResponse, IAuthorizationFactory authorizationFactory)
    {
        _restResponseFactory = restResponse;
        _restRequestFactory = requestFactory;
        _token = authorizationFactory.Auth();
    }

    [Given(@"The URL for the notes details is ""([^""]*)""")]
    public void GivenTheURLForTheNotesDetailsIs(string url)
    {
        _url = url;
    }

    [When(@"I send request to this URL with following note id ""([^""]*)""")]
    public void WhenISendRequestToThisURLWithFollowingNoteId(string id)
    {
        _url = _url.Replace("{id}", id);

        RestRequest request = _restRequestFactory.createGetRequest(_url, authToken: _token);
        _response = _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));
    }

    [Then(@"I should get error status code response")]
    public void ThenIShouldGetErrorStatusCodeResponse()
    {
        Assert.False(_response.IsSuccessStatusCode);
    }
}
