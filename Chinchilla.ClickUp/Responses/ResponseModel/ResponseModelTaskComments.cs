using Newtonsoft.Json;
using System.Runtime.Serialization;
using System;
using Chinchilla.ClickUp.Helpers;

namespace Chinchilla.ClickUp.Responses.Model;

[Serializable]
[DataContract]
public class ResponseModelTaskComments : IResponse
{
    [JsonProperty("comments")]
    public Comment[] Comments { get; set; }
}

public class Comment
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("comment")]
    public CommentEntry[] CommentEntries { get; set; }

    [JsonProperty("comment_text")]
    public string CommentText { get; set; }

    [JsonProperty("user")]
    public ResponseModelUser user { get; set; }

    [JsonProperty("reactions")]
    public object[] Reactions { get; set; }

    [JsonProperty("date")]
    [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
    public DateTime Date { get; set; }
}

public class CommentEntry
{
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("attributes")]
    public Attributes Attributes { get; set; } = new(); // An instance is required to avoid "Cannot destructure property 'link' of 'attributes'" error

    [JsonProperty("type")]
    public string EntryType { get; set; }

    [JsonProperty("attachment")]
    public Attachment Attachment { get; set; }

    public static readonly CommentEntry NextLine = new() { Text = "\n" };

    public static CommentEntry CreateText(string text, bool isBold = false, bool isItalic = false, bool isUnderline = false, bool isStrike = false) =>
        new()
        {
            Text = text,
            Attributes = new()
            {
                IsBold = isBold,
                IsItalic = isItalic,
                IsUnderline = isUnderline,
                IsStrike = isStrike
            },
        };

    public static CommentEntry CreateLink(string text, string link) =>
        new()
        {
            Text = text,
            Attributes = new()
            {
                Link = link
            },
        };
}

public class Attributes
{
    [JsonProperty("block-id")]
    public string BlockId { get; set; }

    [JsonProperty("code")]
    public bool Code { get; set; }

    [JsonProperty("bold")]
    public bool IsBold { get; set; }

    [JsonProperty("italic")]
    public bool IsItalic { get; set; }

    [JsonProperty("underline")]
    public bool IsUnderline { get; set; }

    [JsonProperty("strike")]
    public bool IsStrike { get; set; }

    [JsonProperty("link")] 
    public string Link { get; set; } 
}

public class Attachment
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("date")]
    [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
    public DateTime Date { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("type")]
    public int AttachmentType { get; set; }

    [JsonProperty("source")]
    public int AttachmentSource { get; set; }

    [JsonProperty("version")]
    public int Version { get; set; }

    [JsonProperty("extension")]
    public string Extension { get; set; }

    [JsonProperty("thumbnail_small")]
    public string ThumbnailSmall { get; set; }

    [JsonProperty("thumbnail_medium")]
    public string ThumbnailMedium { get; set; }

    [JsonProperty("thumbnail_large")]
    public string ThumbnailLarge { get; set; }

    [JsonProperty("is_folder")]
    public object IsFolder { get; set; }

    [JsonProperty("mimetype")]
    public string MimeType { get; set; }

    [JsonProperty("hidden")]
    public bool Hidden { get; set; }

    [JsonProperty("parent_id")]
    public string ParentCommentId { get; set; }

    [JsonProperty("size")]
    public int Size { get; set; }

    [JsonProperty("total_comments")]
    public int TotalComments { get; set; }

    [JsonProperty("resolved_comments")]
    public int ResolvedComments { get; set; }

    [JsonProperty("user")]
    public ResponseModelUser User { get; set; }

    [JsonProperty("deleted")]
    public bool Deleted { get; set; }

    [JsonProperty("orientation")]
    public object Orientation { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("parent_comment_type")]
    public int ParentCommentType { get; set; }

    [JsonProperty("parent_comment_parent")]
    public string ParentCommentTaskId { get; set; }

    [JsonProperty("email_data")]
    public object EmailData { get; set; }

    [JsonProperty("url_w_query")]
    public string UrlWQuery { get; set; }

    [JsonProperty("url_w_host")]
    public string UrlWHost { get; set; }
}