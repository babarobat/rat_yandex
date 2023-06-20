using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class ReviewShowRequest : ARequest<ShowReview, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public ReviewShowRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.ShowReview;

        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnShowReviewSuccess;
            set => _bridge.OnShowReviewSuccess = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnShowReviewError;
            set => _bridge.OnShowReviewError = value;
        }

        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
        protected override ShowReview ParseResult(string data) => JsonConvert.DeserializeObject<ShowReview>(data);
    }

    internal class ShowReview
    {
        [JsonProperty("is_sent")]public bool IsSent { get; set; }
    }
}