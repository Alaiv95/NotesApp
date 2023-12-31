using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace Notes.Tests.Acceptance.StepDefinitions;

[Binding]
public class GetNotesListStepDefinitions
{
    private IEnumerable<NoteLookupDto> _notes;
    private string _url;
    private IRestResponseFactory _restResponseFactory;
    private IRestRequestFactory _restRequestFactory;

    public GetNotesListStepDefinitions(IRestRequestFactory requestFactory, IRestResponseFactory restResponse)
    {
        _restResponseFactory = restResponse;
        _restRequestFactory = requestFactory;
    }

    [Given(@"The URL for the list of notes is ""([^""]*)""")]
    public void GivenTheURLForTheListOfNotesIs(string url)
    {
        _url = url;
    }

    [When(@"I send request to this URL")]
    public void WhenISendRequestToThisURL()
    {
        RestRequest request = _restRequestFactory.createGetRequest(_url);
        NoteListVm deserializedResponse = _restResponseFactory
            .GetDeserializedResponse<NoteListVm>(request, new Uri(AppSettings.BaseApiUrl));

        _notes = deserializedResponse.Notes;
    }

    [Then(@"I should get list of notes that contains following data")]
    public void ThenIShouldGetListOfNotesThatContainsFollowingData(Table table)
    {
        IEnumerable<Guid> expectedNoteIds = table.CreateSet<NoteLookupDto>().Select(note => note.Id);
        IEnumerable<NoteLookupDto> actualNotesList = _notes.Where(n => expectedNoteIds.Contains(n.Id));

        table.CompareToSet(actualNotesList);
    }
}
