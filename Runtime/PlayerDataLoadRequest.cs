using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class PlayerDataLoadRequest : ARequest<PlayerDataLoadResponse, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public PlayerDataLoadRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.GetPlayerData;
        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnPlayerDataReceived;
            set => _bridge.OnPlayerDataReceived = value;
        }

        protected override Action<string> ErrorProvider 
        {
            get => _bridge.OnPlayerDataError;
            set => _bridge.OnPlayerDataError = value;
        }

        protected override PlayerDataLoadResponse ParseResult(string data) => JsonConvert.DeserializeObject<PlayerDataLoadResponse>(data);
        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}