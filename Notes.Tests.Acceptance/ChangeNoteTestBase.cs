using Notes.Tests.Acceptance.Authorization;
using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using Notes.WebApi.Models;
using RestSharp;

namespace Notes.Tests.Acceptance;

[Binding]
public class ChangeNoteTestBase : IDisposable
{
    protected List<Guid> _preparedNoteIds = new List<Guid>();
    protected IRestResponseFactory _restResponseFactory;
    protected IRestRequestFactory _restRequestFactory;
    protected string token;
    protected Guid noteId;
    private const string _endpoint = "/note";

    public ChangeNoteTestBase(IRestRequestFactory requestFactory, IRestResponseFactory restResponse, IAuthorizationFactory authorizationFactory)
    {
        _restResponseFactory = restResponse;
        _restRequestFactory = requestFactory;
        token = authorizationFactory.Auth();
        PrepareNotes();
    }

    private void PrepareNotes()
    {
        var noteDto = new List<CreateNoteDto>() {
            new CreateNoteDto { Details = "test1", Title = "test1" }
        };

        foreach (var note in noteDto)
        {
            RestRequest request = _restRequestFactory.createPostRequest(_endpoint, note, authToken: token);
            CreationResponseVm response = _restResponseFactory.GetDeserializedResponse<CreationResponseVm>(request, new Uri(AppSettings.BaseApiUrl));

            _preparedNoteIds.Add(response.Id);
        }

        noteId = _preparedNoteIds[0];
    }

    public void Dispose()
    {
        foreach (var id in _preparedNoteIds)
        {
            RestRequest request = _restRequestFactory.createDeleteRequest($"{_endpoint}/{id}", authToken: token);
            _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));
        }
    }
}
