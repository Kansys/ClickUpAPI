using System;
using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{

	public partial class ResponseModelWebhook
		: Helpers.IResponse
	{
		/// <summary>
		/// The id of the Webhook
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("userid")]
		public int UserId { get; set; }

		[JsonProperty("team_id")]
		public int TeamId { get; set; }

		[JsonProperty("endpoint")]
		public string Endpoint { get; set; }

		[JsonProperty("client_id")]
		public string ClientId { get; set; }

		[JsonProperty("events")]
		public string[] Events { get; set; }

		[JsonProperty("task_id")]
		public int? TaskId { get; set; }

		[JsonProperty("list_id")]
		public long? ListId { get; set; }

		[JsonProperty("folder_id")]
		public int? FolderId { get; set; }

		[JsonProperty("space_id")]
		public long? SpaceId { get; set; }

		[JsonProperty("health")]
		public ResponseModelWebhookHealth Health { get; set; }

		[JsonProperty("secret")]
		public string Secret { get; set; }
	}
}