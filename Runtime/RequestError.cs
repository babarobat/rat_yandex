using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class RequestError
    {
        [JsonProperty("message")] public string Message { get; set; }
    }
}