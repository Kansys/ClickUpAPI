using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{

    /// <summary>
    /// Model object of Task information response
    /// </summary>
    public class ResponseModelAccessibleCustomFields
        : Helpers.IResponse
    {
        /// <summary>
        /// Custom Fields
        /// </summary>
        [JsonProperty("fields")]
        public ResponseModelCustomField[] CustomFields { get; set; }
    }
}