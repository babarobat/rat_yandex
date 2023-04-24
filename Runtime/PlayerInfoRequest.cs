using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class PlayerInfoRequest : ARequest<PlayerInfoResponse, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public PlayerInfoRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.GetPlayerInfo;
        
        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnPlayerInfoReceived;
            set => _bridge.OnPlayerInfoReceived = value;
        }

        protected override Action<string> ErrorProvider 
        {
            get => _bridge.OnPlayerInfoError;
            set => _bridge.OnPlayerInfoError = value;
        }
        
        protected override PlayerInfoResponse ParseResult(string data) =>
            JsonConvert.DeserializeObject<PlayerInfoResponse>(data);
        protected override RequestError ParseError(string data) =>
            JsonConvert.DeserializeObject<RequestError>(data);
    }
}