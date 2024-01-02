using Notes.Tests.Acceptance.Authorization;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using Notes.WebApi.Models;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace Notes.Tests.Acceptance.StepDefinitions;

[Binding]
public class CreateNoteStepDefinitions
{
    private string _url;
    private string _getCreatedNoteUrl;
    private IRestResponseFactory _restResponseFactory;
    private IRestRequestFactory _restRequestFactory;
    private string _token;
    private CreateNoteDto _createNoteDto;

    public CreateNoteStepDefinitions(IRestRequestFactory requestFactory, IRestResponseFactory restResponse, IAuthorizationFactory authorizationFactory)
    {
        _restResponseFactory = restResponse;
        _restRequestFactory = requestFactory;
        _token = authorizationFactory.Auth();
    }

    [Given(@"The URL to create note is ""([^""]*)""")]
    public void GivenTheURLToCreateNoteIs(string url)
    {
        _url = url;
    }

    [When(@"I send request to this endpoint with given values")]
    public void WhenISendRequestToThisEndpointWithGivenValues(Table data)
    {
        _createNoteDto = data.CreateInstance<CreateNoteDto>();

        RestRequest request = _restRequestFactory.createPostRequest(_url, _createNoteDto, authToken: _token);
        CreationResponseVm response = _restResponseFactory.GetDeserializedResponse<CreationResponseVm>(request, new Uri(AppSettings.BaseApiUrl));

        _getCreatedNoteUrl = $"{_url}/{response.Id}";
    }

    [Then(@"I can get created note from database with id from response")]
    public void ThenICanGetCreatedNoteFromDatabaseWithIdFromResponse()
    {
        RestRequest request = _restRequestFactory.createGetRequest(_getCreatedNoteUrl, authToken: _token);
        CreateNoteDto createdNoteDto = _restResponseFactory.GetDeserializedResponse<CreateNoteDto>(request, new Uri(AppSettings.BaseApiUrl));

        createdNoteDto.Should().BeEquivalentTo(_createNoteDto);
    }
}
