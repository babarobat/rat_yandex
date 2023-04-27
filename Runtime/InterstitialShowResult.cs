using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class InterstitialShowResult
    {
        [JsonProperty("was_shown")] public bool WasShown { get; set; }
    }
}