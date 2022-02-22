using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Malish.MalishModels
{
    public class MalishCategory
    {
        [JsonProperty("kls_unicode")]
        public string KlsUnicode { get; set; }

        [JsonProperty("kls_name")]
        public string KlsName { get; set; }

        [JsonProperty("kls_parent")]
        public string KlsParent { get; set; }
    }
}