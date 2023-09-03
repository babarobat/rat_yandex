using System;
using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    internal class GetPurchasesRequest : ARequest<GetPurchasesResult>
    {
        private readonly YaApiBridge _bridge;

        public GetPurchasesRequest(YaApiBridge bridge)
        {
            _bridge = bridge;
        }

        protected override Action Request => _bridge.GetPurchases;

        protected override Action<string> ResponseProvider
        {
            get => _bridge.OnGetPurchasesSuccess;
            set => _bridge.OnGetPurchasesSuccess = value;
        }
        
        protected override Action<string> ErrorProvider
        {
            get => _bridge.OnGetPurchasesError;
            set => _bridge.OnGetPurchasesError = value;
        }

        protected override RequestError ParseError(string data) => JsonConvert.DeserializeObject<RequestError>(data);
        protected override GetPurchasesResult ParseResult(string data) => JsonConvert.DeserializeObject<GetPurchasesResult>(data);
    }

    internal class GetPurchasesResult
    {
        [JsonProperty("purchases")]public Purchase [] Purchases { get; set; }
    }

    public class Purchase
    {
        [JsonProperty("productID")] public string Id { get; set; }
        [JsonProperty("purchaseToken")] public string Token { get; set; }
        [JsonProperty("developerPayload")] public string Payload { get; set; }
    }
}