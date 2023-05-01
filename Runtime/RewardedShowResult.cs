using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class RewardedShowResult
    {
        [JsonProperty("is_rewarded")] public bool IsRewarded { get; set; }
    }
}