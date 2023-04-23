using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class AuthenticationResponse
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("photo")] public string Photo { get; set; }
    }
}