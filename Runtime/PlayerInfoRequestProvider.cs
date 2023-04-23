using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class PlayerInfoRequest: ARequest<PlayerInfo>
    {
        private readonly YaApiBridge _bridge;

        public PlayerInfoRequest(YaApiBridge bridge): base(bridge)
        {
            _bridge = bridge;
        }
        
        protected override Action Request => _bridge.GetPlayerInfo;
        protected override Action<string> Response
        {
            get => _bridge.OnPlayerInfoReceived;
            set => _bridge.OnPlayerInfoReceived = value;
        }

        protected override Action<string> Error {
            get => _bridge.OnPlayerInfoError;
            set => _bridge.OnPlayerInfoError = value;
        }
        protected override PlayerInfo Convert(string data) => JsonConvert.DeserializeObject<PlayerInfo>(data);
    }
}