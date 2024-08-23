using System;
using Chinchilla.ClickUp.Helpers;
using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{
    public class ResponseModelCustomField
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string FieldName { get; set; }

        [JsonProperty("type")]
        public string FieldType { get; set; }

        [JsonProperty("type_config")]
        public ResponseModelCustomFieldTypeConfiguration CustomFieldTypeConfiguration { get; set; }

        [JsonProperty("date_created")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("hide_from_guests")]
        public bool HideFromGuests { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        public override string ToString() => $"{FieldType}: {FieldName} = {Value}";
    }
}