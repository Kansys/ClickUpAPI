using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of Edit Webhook Request
	/// </summary>
	public class ParamsEditWebhook
	{
		#region Attributes

		/// <summary>
		/// The Webhook Id
		/// </summary>
		[JsonProperty("webhook_id")]
		[DataMember(Name = "webhook_id")]
		public string WebhookId { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor of ParamsEditWebhook
        /// </summary>
        /// <param name="webhookId"></param>
        public ParamsEditWebhook(string webhookId)
		{
            WebhookId = webhookId;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Method that validate data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(WebhookId))
			{
				throw new ArgumentNullException(nameof(WebhookId));
			}
		}

		#endregion
	}
}