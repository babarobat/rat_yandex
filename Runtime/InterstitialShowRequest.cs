using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class InterstitialShowRequest : ARequest<InterstitialShowResult, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public InterstitialShowRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.ShowInterstitial;

        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnAdsInterstitialShow;
            set => _bridge.OnAdsInterstitialShow = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnAdsInterstitialShowError;
            set => _bridge.OnAdsInterstitialShowError = value;
        }

        protected override InterstitialShowResult ParseResult(string data) => JsonConvert.DeserializeObject<InterstitialShowResult>(data);
        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}