using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class ReviewInfoRequest : ARequest<ReviewInfo>
    {
        private readonly YaApiBridge _bridge;

        public ReviewInfoRequest(YaApiBridge bridge) : base(bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.GetReviewInfo;
        protected override Action<string> Response
        {
            get => _bridge.OnReviewInfoReceived;
            set => _bridge.OnReviewInfoReceived = value;
        }

        protected override Action<string> Error {
            get => _bridge.OnReviewInfoError;
            set => _bridge.OnReviewInfoError = value;
        }
        protected override ReviewInfo Convert(string data) => JsonConvert.DeserializeObject<ReviewInfo>(data);
    }
}