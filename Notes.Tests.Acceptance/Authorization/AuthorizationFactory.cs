using Notes.Tests.Acceptance.Http;
using Notes.Tests.Acceptance.Infrastructure;
using Notes.WebApi.Models;
using RestSharp;
using System.Text;

namespace Notes.Tests.Acceptance.Authorization;

public class AuthorizationFactory : IAuthorizationFactory
{
    private IRestResponseFactory _restResponseFactory;
    private IRestRequestFactory _restRequestFactory;
    private const string registerEndpoint = "/auth/Register";
    private const string loginEndpoint = "/auth/SignIn";
    private UserModel _defaultUser = new UserModel { UserName = "Pops", Password = "Qwerty123!" };
    public AuthorizationFactory()
    {
        _restResponseFactory = new RestResponseFactory(new RestClientFactory());
        _restRequestFactory = new RestRequestFactory();
    }

    public string Auth(UserModel? userModel)
    {
        return SignIn(userModel);
    }

    public string Auth()
    {
        return SignIn(_defaultUser);
    }

    private string SignIn(UserModel? user)
    {
        RestRequest request = _restRequestFactory.createPostRequest(loginEndpoint, user);
        RestResponse response = _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));

        return AppSettings.TokenPrefix + response.Content;
    }

    public void Register(UserModel userModel)
    {
        RestRequest request = _restRequestFactory.createPostRequest(registerEndpoint, userModel);
        _restResponseFactory.GetRestResponse(request, new Uri(AppSettings.BaseApiUrl));
    }
}
