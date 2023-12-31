using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace Notes.Tests.Acceptance.StepDefinitions;

[Binding]
public class GetNoteDetailStepDefinitions
{
    private NoteDetailsVm _noteDetailsVm;
    private string _url;
    private IRestResponseFactory _restResponseFactory;
    private IRestRequestFactory _restRequestFactory;

    public GetNoteDetailStepDefinitions(IRestRequestFactory requestFactory, IRestResponseFactory restResponse)
    {
        _restResponseFactory = restResponse;
        _restRequestFactory = requestFactory;
    }

    [Given(@"The URL for the notes detail is ""([^""]*)""")]
    public void GivenTheURLForTheNotesDetailIs(string url)
    {
        _url = url;
    }


    [When(@"I send request to this URL with this id ""([^""]*)""")]
    public void WhenISendRequestToThisURLWithThisId(string id)
    {
        _url = _url.Replace("{id}", id);

        RestRequest request = _restRequestFactory.createGetRequest(_url);
        NoteDetailsVm deserializedResponse = _restResponseFactory.GetDeserializedResponse<NoteDetailsVm>(request, new Uri(AppSettings.BaseApiUrl));

        _noteDetailsVm = deserializedResponse;
    }

    [Then(@"I should get following note detail")]
    public void ThenIShouldGetFollowingNoteDetail(Table table)
    {
        table.CompareToInstance(_noteDetailsVm);
    }
}
