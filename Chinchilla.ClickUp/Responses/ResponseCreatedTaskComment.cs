using Newtonsoft.Json;
using Chinchilla.ClickUp.Helpers;
using System;

namespace Chinchilla.ClickUp.Responses
{
    /// <summary>
    /// Response object of the method CreateTaskComment()
    /// </summary>
    public class ResponseCreatedTaskComment
        : IResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("hist_id")]
        public string HistId { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        public DateTime Date { get; set; }

        [JsonProperty("version")]
        public Version Version { get; set; }
    }

    public class Version
    {
        [JsonProperty("object_type")]
        public string ObjectType { get; set; }

        [JsonProperty("object_id")]
        public string ObjectId { get; set; }

        [JsonProperty("workspace_id")]
        public int WorkspaceId { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("data")]
        public VersionData VersionData { get; set; }

        [JsonProperty("master_id")]
        public int MasterId { get; set; }

        [JsonProperty("version")]
        public long VersionNumber { get; set; }

        [JsonProperty("deleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("traceparent")]
        public string TraceParent { get; set; }

        [JsonProperty("date_created")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_updated")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        public DateTime DateUpdated { get; set; }
    }

    public class VersionData
    {
        [JsonProperty("relationships")]
        public Relationship[] Relationships { get; set; }
    }

    public class Relationship
    {
        [JsonProperty("type")]
        public string RelationType { get; set; }

        [JsonProperty("object_type")]
        public string ObjectType { get; set; }

        [JsonProperty("object_id")]
        public string ObjectId { get; set; }

        [JsonProperty("workspace_id")]
        public string WorkspaceId { get; set; }
    }

}