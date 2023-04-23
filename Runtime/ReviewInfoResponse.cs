using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class ReviewInfoResponse
    {
       [JsonProperty("is_review_valid")] public bool IsReviewValid { get; set; }
       [JsonProperty("reason")] public string Reason { get; set; }
    }
}