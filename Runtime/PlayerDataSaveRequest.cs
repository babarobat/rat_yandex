using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class PlayerDataSaveRequest : ARequestWithPayloadEmptyResult<string, RequestError>
    {
        private readonly YaApiBridge _bridge;

        public PlayerDataSaveRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action<string> Request => _bridge.SetPlayerData;

        protected override Action ResponseProvider
        {
            get => _bridge.OnPlayerDataSaved;
            set => _bridge.OnPlayerDataSaved = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnPlayerDataSaveError;
            set => _bridge.OnPlayerDataSaveError = value;
        }

        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}