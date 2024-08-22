using Chinchilla.ClickUp.Helpers;
using Newtonsoft.Json;
using System;

namespace Chinchilla.ClickUp.Responses.Model;

public class ResponseModelWebhookEvent
{
    [JsonProperty("event")]
    public string EventName { get; set; }

    [JsonProperty("history_items")]
    public HistoryItem[] HistoryItems { get; set; }
    
    [JsonProperty("task_id")]
    public string TaskId { get; set; }
    
    [JsonProperty("webhook_id")]
    public string WebhookId { get; set; }
}

public class HistoryItem
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public int ItemType { get; set; }

    [JsonProperty("date")]
    [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
    public DateTime Date { get; set; }

    [JsonProperty("field")]
    public string FieldName { get; set; }

    [JsonProperty("parent_id")]
    public string ParentId { get; set; }

    [JsonProperty("data")]
    public HistoryItemData HistoryItemData { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }

    [JsonProperty("user")]
    public ResponseModelUser User { get; set; }

    [JsonProperty("before")]
    public object ValueBefore { get; set; }

    [JsonProperty("after")]
    public object ValueAfter { get; set; }

    [JsonProperty("custom_field")]
    public ResponseModelCustomField CustomField { get; set; }
}

public class HistoryItemData
{
    [JsonProperty("createTask")]
    public bool IsCreateTask { get; set; }
}
