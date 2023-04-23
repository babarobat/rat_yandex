using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class InitializeRequest : ARequest<InitializationResponse>
    {
        private readonly YaApiBridge _bridge;

        public InitializeRequest(YaApiBridge bridge) : base(bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.Initialize;
        protected override Action<string> Response
        {
            get => _bridge.OnInitializationSuccess;
            set => _bridge.OnInitializationSuccess = value;
        }

        protected override Action<string> Error {
            get => _bridge.OnInitializationError;
            set => _bridge.OnInitializationError = value;
        }

        protected override InitializationResponse Convert(string data) => JsonConvert.DeserializeObject<InitializationResponse>(data);
    }
}