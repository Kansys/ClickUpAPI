using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of create team space request
	/// </summary>
	public class ParamsCreateWebhook
	{
		#region Attributes

		/// <summary>
		/// The Team Id
		/// </summary>
		[JsonProperty("team_id")]
		[DataMember(Name = "team_id")]
		public string TeamId { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of ParamsCreateTeamWebhook
		/// </summary>
		/// <param name="teamId"></param>
		public ParamsCreateWebhook(string teamId)
		{
			TeamId = teamId;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Method that validate data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(TeamId))
			{
				throw new ArgumentNullException(nameof(TeamId));
			}
		}

		#endregion
	}
}