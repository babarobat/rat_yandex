using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class InitializePaymentsRequest : ARequest<InitializePaymentsResult>
    {
        private readonly YaApiBridge _bridge;

        public InitializePaymentsRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.InitializePayments;

        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnInitializePaymentsSuccess;
            set => _bridge.OnInitializePaymentsSuccess = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnInitializePaymentsError;
            set => _bridge.OnInitializePaymentsError = value;
        }

        protected override InitializePaymentsResult ParseResult(string data) => JsonConvert.DeserializeObject<InitializePaymentsResult>(data);
        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}