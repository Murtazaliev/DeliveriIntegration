using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Malish.MalishModels
{
    public class MalishCategory
    {
        [JsonPropertyName("kls_unicode")]
        public string KlsUnicode { get; set; }

        [JsonPropertyName("kls_name")]
        public string KlsName { get; set; }

        [JsonPropertyName("kls_parent")]
        public string KlsParent { get; set; }
    }
}