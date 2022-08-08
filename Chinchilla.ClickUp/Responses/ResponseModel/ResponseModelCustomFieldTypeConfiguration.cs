using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{
    public class ResponseModelCustomFieldTypeConfiguration
    {
        [JsonProperty("default")]
        public int Default { get; set; }

        [JsonProperty("options")]
        public List<ResponseModelCustomFieldTypeConfigOption> Options { get; set; }
    }
}