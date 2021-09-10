using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Rkeeper
{
    public class RkeeperOrganizationModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public int ObjectId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("actualAddress")]
        public string ActualAddress { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}