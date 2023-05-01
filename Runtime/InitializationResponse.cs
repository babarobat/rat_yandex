using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class InitializationResponse
    {
        [JsonProperty("is_player_authorized")] public bool IsPlayerAuthorized { get; set; } 
        [JsonProperty("is_payments_initialized")] public bool IsPaymentsInitialized { get; set; } 
    }
}