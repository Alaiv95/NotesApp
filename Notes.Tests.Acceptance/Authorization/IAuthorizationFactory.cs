using Notes.WebApi.Models;

namespace Notes.Tests.Acceptance.Authorization;

public interface IAuthorizationFactory
{
    public string Auth(UserModel userModel);
    public string Auth();
    public void Register(UserModel userModel);
}
