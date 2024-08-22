using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{
    public class ResponseModelCustomFieldTypeConfiguration
    {
        [JsonProperty("default")]
        public int Default { get; set; }

        [JsonProperty("placeholder")]
        public object Placeholder { get; set; }

        [JsonProperty("new_drop_down")]
        public bool IsNewDropDown { get; set; }

        [JsonProperty("options")]
        public List<ResponseModelCustomFieldTypeConfigOption> Options { get; set; }
    }
}