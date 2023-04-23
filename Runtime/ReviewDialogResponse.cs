using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class ReviewDialogResponse
    {
        [JsonProperty("is_review_sent")] public bool IsReviewSent { get; set; }
    }
}