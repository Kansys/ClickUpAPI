using Newtonsoft.Json;
using Chinchilla.ClickUp.Enums;
using Chinchilla.ClickUp.Requests.Model;
using System;
using Chinchilla.ClickUp.Helpers;

namespace Chinchilla.ClickUp.Requests
{
	/// <summary>
	/// Request object for method EditTask()
	/// </summary>
	public class RequestEditTask
	{
		#region Attributes

		/// <summary>
		/// Name of the task
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

        /// <summary>
        /// description of the task
        /// </summary>
        [JsonProperty("description")]
		public string Description { get; set; }

        /// <summary>
        /// Status of the Task
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Priority of the Task
        /// </summary>
        [JsonProperty("priority")]
        public TaskPriority? Priority { get; set; }

        /// <summary>
        /// Due Date of the task
        /// </summary>
        [JsonProperty("due_date")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Is there time in the DueDate value
        /// </summary>
        [JsonProperty("due_date_time")]
        public bool? HasTimeInDueDate => DueDate is null? null : DueDate.Value.TimeOfDay.TotalSeconds > 0;

        /// <summary>
        /// parent task
        /// </summary>
        [JsonProperty("parent")]
		public string Parent { get; set; }

        /// <summary>
        /// Content of the task
        /// </summary>
        [JsonProperty("time_estimate")]
		public int? TimeEstimate { get; set; }

        /// <summary>
        /// Start Date of the task
        /// </summary>
        [JsonProperty("start_date")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Is there time in the StartDate value
        /// </summary>
        [JsonProperty("start_date_time")]
        public bool? HasTimeInStartDate => StartDate is null? null : StartDate.Value.TimeOfDay.TotalSeconds > 0;

        /// <summary>
        /// Points of the task
        /// </summary>
        [JsonProperty("points")]
        public double? Points { get; set; }

        /// <summary>
        /// List of users id added or removed to the task
        /// </summary>
        [JsonProperty("assignees")]
        public RequestModelSupportAssignees Assignees { get; set; }

        //[JsonProperty("group_assignees ")] //TODO
        //[JsonProperty("watchers ")] //TODO

        /// <summary>
        /// Should the task be archived
        /// </summary>
		[JsonProperty("archived")]
		public bool? IsArchived { get; set; }

		#endregion
	}
}