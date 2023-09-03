using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class InterstitialShowRequest : ARequest<InterstitialShowResult>
    {
        private readonly YaApiBridge _bridge;

        public InterstitialShowRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.ShowInterstitial;
        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnAdsInterstitialSuccess;
            set => _bridge.OnAdsInterstitialSuccess = value;
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