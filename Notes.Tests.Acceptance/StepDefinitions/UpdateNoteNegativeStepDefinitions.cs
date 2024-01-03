using Notes.Tests.Acceptance.Authorization;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using Notes.WebApi.Models;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace Notes.Tests.Acceptance.StepDefinitions;

[Binding]
public class UpdateNoteNegativeStepDefinitions : ChangeNoteTestBase
{
    private RestResponse _response;
    private string _url;
    private string _token;

    public UpdateNoteNegativeStepDefinitions(IRestRequestFactory requestFactory, IRestResponseFactory restResponse, IAuthorizationFactory authorizationFactory)
        : base(requestFactory, restResponse, authorizationFactory) { }

    [Given(@"The URL to update note is ""([^""]*)""")]
    public void GivenTheURLToUpdateNoteIs(string url)
    {
        _url = url;
    }

    [When(@"I send request to this URL with following data")]
    public void WhenISendRequestToThisURLWithFollowingData(Table table)
    {
        UpdateNoteDto updateNoteDto = table.CreateInstance<UpdateNoteDto>();

        if (updateNoteDto?.Id == Guid.Empty)
            updateNoteDto.Id = noteId;
        
        RestRequest request = _restRequestFactory.createDeleteRequest(_url, authToken: _token);
        _response = _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));
    }


    [Then(@"I should get error status code from response")]
    public void ThenIShouldGetErrorStatusCodeFromResponse()
    {
        Assert.False(_response.IsSuccessStatusCode);
    }
}
