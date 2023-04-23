using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class PlayerDataLoadRequest : ARequest<PlayerDataLoadResponse>
    {
        private readonly YaApiBridge _bridge;

        public PlayerDataLoadRequest(YaApiBridge bridge) : base(bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.GetPlayerData;
        protected override Action<string> Response
        {
            get => _bridge.OnPlayerDataReceived;
            set => _bridge.OnPlayerDataReceived = value;
        }

        protected override Action<string> Error {
            get => _bridge.OnPlayerDataError;
            set => _bridge.OnPlayerDataError = value;
        }

        protected override PlayerDataLoadResponse Convert(string data) =>
            JsonConvert.DeserializeObject<PlayerDataLoadResponse>(data);
    }
}