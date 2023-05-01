using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class RewardedShowRequest : ARequest<RewardedShowResult, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public RewardedShowRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.ShowRewarded;

        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnRewardedShowSuccess;
            set => _bridge.OnRewardedShowSuccess = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnRewardedShowError;
            set => _bridge.OnRewardedShowError = value;
        }

        protected override RewardedShowResult ParseResult(string data) => JsonConvert.DeserializeObject<RewardedShowResult>(data);
        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}