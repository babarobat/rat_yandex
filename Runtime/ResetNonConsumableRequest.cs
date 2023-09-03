using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class ResetNonConsumableRequest : ARequestWithPayloadEmptyResult<string>
    {
        private readonly YaApiBridge _bridge;

        public ResetNonConsumableRequest(YaApiBridge bridge, string payload) : base(payload)
        {
            _bridge = bridge;
        }

        protected override Action<string> Request => _bridge.ResetNonConsumable;

        protected override Action ResponseProvider
        {
            get => _bridge.OnResetNonConsumableSuccess;
            set => _bridge.OnResetNonConsumableSuccess = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnResetNonConsumableError;
            set => _bridge.OnResetNonConsumableError = value;
        }

        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}