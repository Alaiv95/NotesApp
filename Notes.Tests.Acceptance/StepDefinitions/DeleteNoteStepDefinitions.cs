using Microsoft.AspNetCore.Http;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using Notes.WebApi.Models;
using NUnit.Framework;
using RestSharp;

namespace Notes.Tests.Acceptance.StepDefinitions;

[Binding]
public class DeleteNoteStepDefinitions
{
    private string _url;
    private Guid _noteId;
    private int _statusCode;
    private IRestResponseFactory _restResponseFactory;
    private IRestRequestFactory _restRequestFactory;
    
    public DeleteNoteStepDefinitions(IRestRequestFactory requestFactory, IRestResponseFactory restResponse)
    {
        _restResponseFactory = restResponse;
        _restRequestFactory = requestFactory;
    }

    [Given(@"Created note and its id with Url ""([^""]*)""")]
    public void GivenCreatedNoteAndItsIdWithUrl(string createNoteurl)
    {
        var noteDto = new CreateNoteDto { Details = "test1", Title = "test2" };

        RestRequest request = _restRequestFactory.createPostRequest(createNoteurl, noteDto);
        CreationResponseVm response = _restResponseFactory.GetDeserializedResponse<CreationResponseVm>(request, new Uri(AppSettings.BaseApiUrl));

        _noteId = response.Id;
    }

    [Given(@"The URL to delete the note is ""([^""]*)""")]
    public void GivenTheURLToDeleteTheNoteIs(string url)
    {
        _url = url.Replace("{id}", _noteId.ToString());
    }

    [When(@"I send request to this URL with created note id")]
    public void WhenISendRequestToThisURLWithCreatedNoteId()
    {
        RestRequest request = _restRequestFactory.createDeleteRequest(_url);
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
        RestRequest request = _restRequestFactory.createGetRequest(_url);
        RestResponse response = _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));

        Assert.AreEqual(StatusCodes.Status404NotFound, (int)response.StatusCode);
    }
}
