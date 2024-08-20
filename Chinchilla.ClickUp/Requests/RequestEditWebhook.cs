using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Requests
{
    /// <summary>
    /// Request object for method EditWebhook()
    /// </summary>
    [Serializable]
	[DataContract]
	public class RequestEditWebhook
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
        /// Status of the webhook
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// The constructor of RequestEditWebhook
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="events"></param>
        /// <param name="status"></param>
        public RequestEditWebhook(string endpoint, string[] events, string status)
		{
			Endpoint = endpoint;
			Events = events.Any() ? events : ["*"];
            Status = status;
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

			if (string.IsNullOrEmpty(Status))
			{
				throw new ArgumentNullException(nameof(Status));
			}

			if (!Events.Any())
			{
				throw new ArgumentNullException(nameof(Events));
			}
		}

		#endregion
	}
}