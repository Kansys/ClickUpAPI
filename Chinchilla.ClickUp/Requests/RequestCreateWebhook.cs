using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Requests
{
	/// <summary>
	/// Request object for method CreateTeamSpace()
	/// </summary>
	[Serializable]
	[DataContract]
	public class RequestCreateWebhook
	{
		#region Attributes

		/// <summary>
		/// Name of the new list
		/// </summary>
		[JsonProperty("endpoint")]
		[DataMember(Name = "endpoint")]
		public string Endpoint { get; set; }

		[JsonProperty("events")]
		[DataMember(Name = "events")]
		public string[] Events { get; set; }

        /// <summary>
        /// ID of the Space
        /// </summary>
        [JsonProperty("space_id")]
        public string SpaceId { get; set; }

        /// <summary>
        /// ID of the Folder
        /// </summary>
        [JsonProperty("folder_id")]
        public string FolderId { get; set; }

        /// <summary>
        /// ID of the List
        /// </summary>
        [JsonProperty("list_id")]
        public string ListId { get; set; }

        /// <summary>
        /// ID of the Task
        /// </summary>
        [JsonProperty("task_id")]
        public string TaskId { get; set; }

		#endregion


        #region Constructor

        /// <summary>
        /// The constructor of RequestCreateTeamWebhook
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="events"></param>
        private RequestCreateWebhook(string endpoint, string[] events)
		{
			Endpoint = endpoint;
			Events = events.Any() ? events : ["*"];
		}

        /// <summary>
        /// Subscribe to events for the List
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="spaceId"></param>
        /// <param name="listId"></param>
        /// <param name="events"></param>
        public RequestCreateWebhook(string endpoint, string spaceId, string listId, string[] events) : this(
            endpoint, events)
        {
            SpaceId = spaceId;
            ListId = listId;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Validation method of data
        /// </summary>
        public void ValidateData()
		{
			if (string.IsNullOrEmpty(Endpoint))
			{
				throw new ArgumentNullException(nameof(Endpoint));
			}

			if (!Events.Any())
			{
				throw new ArgumentNullException(nameof(Events));
			}
		}

		#endregion
	}
}