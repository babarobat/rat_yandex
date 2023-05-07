using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class BuyNonConsumableRequest : ARequestWithPayloadEmptyResult<string, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public BuyNonConsumableRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action<string> Request => _bridge.BuyNonConsumable;

        protected override Action ResponseProvider
        {
            get => _bridge.OnBuyNonConsumableSuccess;
            set => _bridge.OnBuyNonConsumableSuccess = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnBuyNonConsumableError;
            set => _bridge.OnBuyNonConsumableError = value;
        }

        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}