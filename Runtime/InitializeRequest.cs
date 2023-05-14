using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class InitializeRequest : ARequest<InitializationResponse, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public InitializeRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.Initialize;
        protected override InitializationResponse ParseResult(string data)
        {
            return JsonConvert.DeserializeObject<InitializationResponse>(data);
        }

        protected override RequestError ParseError(string data)
        {
            return JsonConvert.DeserializeObject<RequestError>(data);
        }

        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnInitializationSuccess;
            set => _bridge.OnInitializationSuccess = value;
        }

        protected override Action<string> ErrorProvider {
            get => _bridge.OnInitializationError;
            set => _bridge.OnInitializationError = value;
        }
    }
}