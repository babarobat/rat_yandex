using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class SetLeaderboardEntryRequestPayload
    {
        public string LeaderBoardId;
        public int Value;
    }
    
    internal class SetLeaderboardEntryRequest : ARequestWithPayloadEmptyResult<SetLeaderboardEntryRequestPayload>
    {
        private readonly YaApiBridge _bridge;

        public SetLeaderboardEntryRequest(YaApiBridge bridge, SetLeaderboardEntryRequestPayload payload) : base(payload)
        {
            _bridge = bridge;
        }

        protected override Action<SetLeaderboardEntryRequestPayload> Request => _bridge.SetLeaderboardEntry;

        protected override Action ResponseProvider
        {
            get => _bridge.OnSetLeaderboardEntrySuccess;
            set => _bridge.OnSetLeaderboardEntrySuccess = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnSetLeaderboardEntryError;
            set => _bridge.OnSetLeaderboardEntryError = value;
        }

        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }
}