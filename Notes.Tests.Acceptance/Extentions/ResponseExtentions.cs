using RestSharp;
using System.Text.Json;

namespace Notes.Tests.Acceptance.Utils;

public static class ResponseExtentions
{
    public static T DeserializeContent<T>(this RestResponse response, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<T>(response.Content, options);
    }
}
