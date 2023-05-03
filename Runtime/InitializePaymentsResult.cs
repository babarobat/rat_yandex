using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class InitializePaymentsResult 
    {
        [JsonProperty("purchases")] public string Purchases { get; set; }
    }
}