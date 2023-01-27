using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Delivery.SelfServiceKioskApi.Models.Malish
{
    public class MalishNomenclatureCategory
    {
        [JsonProperty("ExternalId")]
        [JsonPropertyName("ExternalId")]
        public string ExternalId { get; set; }
        
        [JsonProperty("ExternalParentId")]
        [JsonPropertyName("ExternalParentId")]
        public string ExternalParentId { get; set; }
        
        [JsonProperty("CategoryName")]
        [JsonPropertyName("CategoryName")]
        public string CategoryName { get; set; }
        
        [JsonProperty("CategoryIsVisible")]
        [JsonPropertyName("CategoryIsVisible")]
        public bool CategoryIsVisible { get; set; }
    }
}