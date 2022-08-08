using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of Get Task By ID request
	/// </summary>
	public class ParamsGetTaskById
	{
		#region Attributes

		/// <summary>
		/// The Team Id 
		/// </summary>
		[JsonProperty("task_id")]
		[DataMember(Name = "task_id")]
		public string TaskId { get; set; }

		#endregion

        public string CustomTaskId { get; } = string.Empty;
        public string TeamId { get; }


		#region Constructor

		/// <summary>
		/// The constructor of ParamsGetTasksById
		/// </summary>
		/// <param name="taskId"></param>
		public ParamsGetTaskById(string taskId)
		{
			TaskId = taskId;
		}

        public ParamsGetTaskById(string customTaskId, string teamId)
        {
            CustomTaskId = customTaskId;
			TeamId = teamId;
        }
		#endregion


		#region Public Methods

		/// <summary>
		/// Method that validate the data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(TaskId))
			{
				throw new ArgumentNullException(nameof(TaskId));
			}
		}

		#endregion
	}
}