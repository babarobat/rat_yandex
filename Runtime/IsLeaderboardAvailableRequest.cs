using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class IsLeaderboardAvailableRequest : ARequestWithPayLoad<string, IsLeaderboardAvailableResult>
    {
        private readonly YaApiBridge _bridge;

        public IsLeaderboardAvailableRequest(YaApiBridge bridge, string payLoad) : base(payLoad)
        {
            _bridge = bridge;
        }

        protected override Action<string> Request => _bridge.IsLeaderboardAvailable;

        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnIsLeaderboardAvailableSuccess;
            set => _bridge.OnIsLeaderboardAvailableSuccess = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnIsLeaderboardAvailableError;
            set => _bridge.OnIsLeaderboardAvailableError = value;
        }

        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
        protected override IsLeaderboardAvailableResult ParseResult(string data) => JsonConvert.DeserializeObject<IsLeaderboardAvailableResult>(data);
    }

    public class IsLeaderboardAvailableResult
    {
        [JsonProperty("value")] public bool Value { get; set; }
        [JsonProperty("value")] public string Reason { get; set; }
    }
}