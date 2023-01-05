using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Malish.MalishModels
{
    public class MalishCategory
    {
        [JsonProperty("kls_unicode")]
        [JsonPropertyName("kls_unicode")]
        public string KlsUnicode { get; set; }

        [JsonProperty("kls_name")]
        [JsonPropertyName("kls_name")]
        public string KlsName { get; set; }

        [JsonProperty("kls_parent")]
        [JsonPropertyName("kls_parent")]
        public string KlsParent { get; set; }
    }
}