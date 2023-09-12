using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class SetLeaderboardEntryRequestPayload
    {
        [JsonProperty("leaderboard_id")] public string LeaderBoardId;
        [JsonProperty("value")] public int Value;
        [JsonProperty("payLoad")] public string PayLoad;
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