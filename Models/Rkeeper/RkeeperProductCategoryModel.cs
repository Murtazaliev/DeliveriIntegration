using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Rkeeper
{
    public class RkeeperProductCategoryModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }
    }
}