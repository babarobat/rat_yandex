using Newtonsoft.Json;

namespace RatYandex.Runtime
{
    public class InitializePaymentsResult 
    {
        [JsonProperty("purchases")] public object[] Purchases { get; set; }
        [JsonProperty("catalog")] public YaInAppCatalogItem[] Catalog { get; set; }
    }
    
    public class YaInAppCatalogItem
    {
        [JsonProperty("id")] public string ID { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("image_uri")] public string ImageUri { get; set; }
        [JsonProperty("price")] public string Price { get; set; }
        [JsonProperty("price_value")] public string PriceValue { get; set; }
        [JsonProperty("price_currency_code")] public string PriceCurrencyCode { get; set; }

        public override string ToString()
        {
            return $"{nameof(ID)}: {ID}, {nameof(Title)}: {Title}, {nameof(Description)}: {Description}, {nameof(ImageUri)}: {ImageUri}, {nameof(Price)}: {Price}, {nameof(PriceValue)}: {PriceValue}, {nameof(PriceCurrencyCode)}: {PriceCurrencyCode}";
        }
    }
}