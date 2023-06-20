using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class CanReviewRequest : ARequest<CanReviewResult, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public CanReviewRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.CanReview;

        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnCanReviewSuccess;
            set => _bridge.OnCanReviewSuccess = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnCanReviewError;
            set => _bridge.OnCanReviewError = value;
        }

        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
        protected override CanReviewResult ParseResult(string data) => JsonConvert.DeserializeObject<CanReviewResult>(data);
    }

    public class CanReviewResult
    {
        [JsonProperty("value")]public bool Value { get; set; }
        [JsonProperty("reason")]public string Reason { get; set; }
    }
}