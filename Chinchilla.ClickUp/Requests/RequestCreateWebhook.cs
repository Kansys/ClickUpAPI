using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Requests
{
    /// <summary>
	/// Request object for method CreateWebhook()
	/// </summary>
	[Serializable]
	[DataContract]
	public abstract class RequestCreateWebhook
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

		#endregion


        #region Constructor

        /// <summary>
        /// The constructor of RequestCreateTeamWebhook
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="events"></param>
        protected RequestCreateWebhook(string endpoint, string[] events)
		{
			Endpoint = endpoint;
			Events = events.Any() ? events : ["*"];
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

    /// <summary>
    /// Request object for method CreateWebhook()
    /// </summary>
    [Serializable]
    [DataContract]
    public class RequestCreateWebhookForSpace : RequestCreateWebhook
    {
        /// <summary>
        /// ID of the Space
        /// </summary>
        [JsonProperty("space_id")]
        public long SpaceId { get; set; }

        /// <summary>
        /// Subscribe to events for the List
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="events"></param>
        /// <param name="spaceId"></param>
        public RequestCreateWebhookForSpace(string endpoint, string[] events, long spaceId) : base(
            endpoint, events)
        {
            SpaceId = spaceId;
        }
    }

    /// <summary>
    /// Request object for method CreateWebhook()
    /// </summary>
    [Serializable]
    [DataContract]
    public class RequestCreateWebhookForFolder: RequestCreateWebhook
    {
        #region Attributes

        /// <summary>
        /// ID of the Folder
        /// </summary>
        [JsonProperty("folder_id")]
        public string FolderId { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// The constructor of RequestCreateTeamWebhook
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="events"></param>
        /// <param name="folderId"></param>
        private RequestCreateWebhookForFolder(string endpoint, string[] events, string folderId) : base(endpoint, events)
        {
            FolderId = folderId;
        }

        #endregion
    }

    /// <summary>
    /// Request object for method CreateWebhook()
    /// </summary>
    [Serializable]
    [DataContract]
    public class RequestCreateWebhookForList : RequestCreateWebhook
    {
        #region Attributes

        /// <summary>
        /// ID of the List
        /// </summary>
        [JsonProperty("list_id")]
        public string ListId { get; set; }
        #endregion


        #region Constructor

        /// <summary>
        /// The constructor of RequestCreateTeamWebhook
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="events"></param>
        /// <param name="listId"></param>
        private RequestCreateWebhookForList(string endpoint, string[] events, string listId) : base(endpoint, events)
        {
            ListId = listId;
        }

        #endregion
    }
    /// <summary>
    /// Request object for method CreateWebhook()
    /// </summary>
    [Serializable]
    [DataContract]
    public class RequestCreateWebhookForTask : RequestCreateWebhook
    {
        #region Attributes

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
        /// <param name="taskId"></param>
        private RequestCreateWebhookForTask(string endpoint, string[] events, string taskId) : base(endpoint, events)
        {
            TaskId = taskId;
        }

        #endregion
    }
}
