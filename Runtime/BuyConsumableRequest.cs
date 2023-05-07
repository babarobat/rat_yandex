using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class BuyConsumableRequest : ARequestWithPayloadEmptyResult<string, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public BuyConsumableRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action<string> Request => _bridge.BuyConsumable;

        protected override Action ResponseProvider
        {
            get => _bridge.OnBuyConsumableSuccess;
            set => _bridge.OnBuyConsumableSuccess = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnBuyConsumableError;
            set => _bridge.OnBuyConsumableError = value;
        }

        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}