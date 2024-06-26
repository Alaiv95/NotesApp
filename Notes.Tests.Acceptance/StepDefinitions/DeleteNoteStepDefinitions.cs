using Microsoft.AspNetCore.Http;
using Notes.Tests.Acceptance.Authorization;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using Notes.WebApi.Models;
using NUnit.Framework;
using RestSharp;

namespace Notes.Tests.Acceptance.StepDefinitions;

[Binding]
public class DeleteNoteStepDefinitions : ChangeNoteTestBase
{
    private string _url;
    private int _statusCode;

    public DeleteNoteStepDefinitions(IRestRequestFactory requestFactory, IRestResponseFactory responseFactory, IAuthorizationFactory authorizationFactory)
        : base(requestFactory, responseFactory, authorizationFactory) { }

    [Given(@"The URL to delete the note is ""([^""]*)""")]
    public void GivenTheURLToDeleteTheNoteIs(string url)
    {
        _url = url.Replace("{id}", noteId.ToString());
    }

    [When(@"I send request to this URL with created note id")]
    public void WhenISendRequestToThisURLWithCreatedNoteId()
    {
        RestRequest request = _restRequestFactory.createDeleteRequest(_url, authToken: token);
        RestResponse response = _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));

        _statusCode = (int)response.StatusCode;
    }

    [Then(@"I should get success status code")]
    public void ThenIShouldGetSuccesStatusCode()
    {
        Assert.AreEqual(StatusCodes.Status204NoContent, _statusCode);
    }

    [Then(@"Created note should not exist in database")]
    public void ThenCreatedNoteShouldNotExistInDatabase()
    {
        RestRequest request = _restRequestFactory.createGetRequest(_url, authToken: token);
        RestResponse response = _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));

        Assert.AreEqual(StatusCodes.Status404NotFound, (int)response.StatusCode);
    }
}
