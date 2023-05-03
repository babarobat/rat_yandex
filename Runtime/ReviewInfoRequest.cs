using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class ReviewInfoRequest : ARequest<ReviewInfoResponse, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public ReviewInfoRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.GetReviewInfo;
        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnReviewInfoReceived;
            set => _bridge.OnReviewInfoReceived = value;
        }

        protected override Action<string> ErrorProvider {
            get => _bridge.OnReviewInfoError;
            set => _bridge.OnReviewInfoError = value;
        }
        protected override ReviewInfoResponse ParseResult(string data) => JsonConvert.DeserializeObject<ReviewInfoResponse>(data);
        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}