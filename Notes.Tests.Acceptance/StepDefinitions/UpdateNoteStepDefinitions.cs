using Microsoft.AspNetCore.Http;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using Notes.WebApi.Models;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace Notes.Tests.Acceptance.StepDefinitions;

[Binding]
public class UpdateNoteStepDefinitions : WithPreparedNoteSteps
{
    private string _url;
    private string _getNoteUrl;
    private UpdateNoteDto _updateNoteDto;

    public UpdateNoteStepDefinitions(IRestRequestFactory requestFactory, IRestResponseFactory responseFactory)
        : base(requestFactory, responseFactory) { }


    [Given(@"The URL to update the note is ""([^""]*)""")]
    public void GivenTheURLToUpdateTheNoteIs(string url)
    {
        _url = url;
        _getNoteUrl = $"{_url}/{_preparedNoteId}";
    }

    [When(@"I send request to this URL with existing note id and following values")]
    public void WhenISendRequestToThisURLWithExistingNoteIdAndFollowingValues(Table table)
    {
        _updateNoteDto = table.CreateInstance<UpdateNoteDto>();
        _updateNoteDto.Id = _preparedNoteId;

        RestRequest request = _restRequestFactory.createPutRequest(_url, _updateNoteDto);
        RestResponse response = _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));

        Assert.AreEqual(StatusCodes.Status204NoContent, (int)response.StatusCode);
    }

    [Then(@"Updated note should contains new values")]
    public void ThenUpdatedNoteShouldContainsNewValues()
    {
        RestRequest request = _restRequestFactory.createGetRequest(_getNoteUrl);
        UpdateNoteDto updatedNote = _restResponseFactory.GetDeserializedResponse<UpdateNoteDto>(request, new Uri(AppSettings.BaseApiUrl));

        updatedNote.Should().BeEquivalentTo(_updateNoteDto);
    }
}
