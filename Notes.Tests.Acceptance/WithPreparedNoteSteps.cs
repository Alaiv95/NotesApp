using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using Notes.WebApi.Models;
using RestSharp;
using System.Security.Policy;


namespace Notes.Tests.Acceptance;

[Binding]
public class WithPreparedNoteSteps
{
    protected Guid _preparedNoteId;
    protected IRestResponseFactory _restResponseFactory;
    protected IRestRequestFactory _restRequestFactory;
    private const string _endpoint = "/note";

    public WithPreparedNoteSteps(IRestRequestFactory requestFactory, IRestResponseFactory restResponse)
    {
        _restResponseFactory = restResponse;
        _restRequestFactory = requestFactory;
    }

    [BeforeScenario]
    public void PrepareNote()
    {
        var noteDto = new CreateNoteDto { Details = "test1", Title = "test2" };

        RestRequest request = _restRequestFactory.createPostRequest(_endpoint, noteDto);
        CreationResponseVm response = _restResponseFactory.GetDeserializedResponse<CreationResponseVm>(request, new Uri(AppSettings.BaseApiUrl));

        _preparedNoteId = response.Id;
    }

    [AfterScenario]
    public void Teardown()
    {
        RestRequest request = _restRequestFactory.createDeleteRequest($"{_endpoint}/{_preparedNoteId}");
        _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));
    }
}
