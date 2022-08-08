using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{
    public class ResponseModelCustomFieldTypeConfigOption
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("orderindex")]
        public int OrderIndex { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}

