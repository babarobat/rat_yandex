using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class PlayerDataLoadResponse
    {
        [JsonProperty("data")] public string Data { get; set; }
    }
}