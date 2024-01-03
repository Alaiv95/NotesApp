using Notes.Tests.Acceptance.Authorization;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using Notes.WebApi.Models;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace Notes.Tests.Acceptance.StepDefinitions;

[Binding]
public class CreateNoteWithInvalidDataStepDefinitions
{
    private RestResponse _response;
    private string _url;
    private IRestResponseFactory _restResponseFactory;
    private IRestRequestFactory _restRequestFactory;
    private string _token;

    public CreateNoteWithInvalidDataStepDefinitions(IRestRequestFactory requestFactory, IRestResponseFactory restResponse, IAuthorizationFactory authorizationFactory)
    {
        _restResponseFactory = restResponse;
        _restRequestFactory = requestFactory;
        _token = authorizationFactory.Auth();
    }

    [Given(@"The URL to create new note is ""([^""]*)""")]
    public void GivenTheURLToCreateNewNoteIs(string url)
    {
        _url = url;
    }

    [When(@"I send request to this url with empty Title")]
    public void WhenISendRequestToThisUrlWithEmptyTitle(Table table)
    {
        CreateNoteDto note = table.CreateInstance<CreateNoteDto>();

        RestRequest request = _restRequestFactory.createPostRequest(_url, note, authToken: _token);
        _response = _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));
    }

    [Then(@"I should get error status response")]
    public void ThenIShouldGetErrorStatusResponse()
    {
        Assert.False(_response.IsSuccessStatusCode);
    }
}
