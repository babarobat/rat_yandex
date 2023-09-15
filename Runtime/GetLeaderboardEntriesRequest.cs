using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class GetLeaderboardEntriesRequestPayload
    {
        public string LeaderboardId;
        public bool IncludeUser;
        public int QuantityAround;
        public int QuantityTop;
    }
    
    internal class GetLeaderboardEntriesRequest : ARequestWithPayLoad<GetLeaderboardEntriesRequestPayload, GetLeaderboardEntriesResult>
    {
        private readonly YaApiBridge _bridge;

        public GetLeaderboardEntriesRequest(YaApiBridge bridge, GetLeaderboardEntriesRequestPayload payload) : base(payload)
        {
            _bridge = bridge;
        }

        protected override Action<GetLeaderboardEntriesRequestPayload> Request => _bridge.GetLeaderboardEntries;

        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnGetLeaderboardEntriesSuccess;
            set => _bridge.OnGetLeaderboardEntriesSuccess = value;
        }

        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnGetLeaderboardEntriesError;
            set => _bridge.OnGetLeaderboardEntriesError = value;
        }

        protected override GetLeaderboardEntriesResult ParseResult(string data) => JsonConvert.DeserializeObject<GetLeaderboardEntriesResult>(data);
        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
    }

    public class GetLeaderboardEntriesResult
    {
        [JsonProperty("entries")] public Entries[] Entries { get; set; }
        [JsonProperty("leaderboard")] public Leaderboard Leaderboard { get; set; }
        [JsonProperty("ranges")] public Ranges[] Ranges { get; set; }
        [JsonProperty("userRank")] public int UserRank { get; set; }
    }

    public class Entries
    {
        [JsonProperty("extraData")] public string ExtraData { get; set; }
        [JsonProperty("score")] public int Score { get; set; }
        [JsonProperty("rank")] public int Rank { get; set; }
        [JsonProperty("player")] public Player Player { get; set; }
        [JsonProperty("formattedScore")] public string FormattedScore { get; set; }
    }

    public class Player
    {
        [JsonProperty("lang")] public string Lang { get; set; }
        [JsonProperty("publicName")] public string PublicName { get; set; }
        [JsonProperty("scopePermissions")] public ScopePermissions ScopePermissions { get; set; }
        [JsonProperty("uniqueID")] public string UniqueID { get; set; }
    }

    public class ScopePermissions
    {
        [JsonProperty("avatar")] public string Avatar { get; set; }
        [JsonProperty("public_name")] public string PublicName { get; set; }
    }

    public class Leaderboard
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("appID")] public int AppID { get; set; }
        [JsonProperty("title")] public Title Title { get; set; }
        [JsonProperty("description")] public Description Description { get; set; }
    }

    public class Title
    {
        [JsonProperty("ru")] public string Ru { get; set; }
    }

    public class Description
    {
        [JsonProperty("score_format")] public Score_format ScoreFormat { get; set; }
        [JsonProperty("invert_sort_order")] public bool InvertSortOrder { get; set; }
    }

    public class Score_format
    {
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("options")] public Options Options { get; set; }
    }

    public class Options
    {
        [JsonProperty("decimal_offset")] public int DecimalOffset { get; set; }
    }

    public class Ranges
    {
        [JsonProperty("start")] public int Start { get; set; }
        [JsonProperty("size")] public int Size { get; set; }
    }
}