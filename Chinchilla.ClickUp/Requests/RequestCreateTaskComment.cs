using System.Collections.Generic;
using Newtonsoft.Json;
using Chinchilla.ClickUp.Responses.Model;

namespace Chinchilla.ClickUp.Requests
{
	/// <summary>
	/// Request object for method CreateTaskComment()
	/// </summary>
	public class RequestCreateTaskComment
    {
        public RequestCreateTaskComment(string commentText = null) => 
            CommentText = commentText;

        [JsonProperty("comment_text")]
        public string CommentText { get; set; }

        [JsonProperty("comment")]
        public List<CommentEntry> CommentEntries { get; } = new();

        public void AddRange(params CommentEntry[] commentEntries) => 
            CommentEntries.AddRange(commentEntries);
    }
}