using Newtonsoft.Json;
using Chinchilla.ClickUp.Enums;
using System;
using System.Collections.Generic;
using Chinchilla.ClickUp.Helpers;

namespace Chinchilla.ClickUp.Requests
{
	/// <summary>
	/// Request object for method CreateTaskInList()
	/// </summary>
	public class RequestCreateTaskInList
	{
		#region Attributes

		/// <summary>
		/// Name of the task
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Content of the task
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		/// List of user id that will be added to this task
		/// </summary>
		[JsonProperty("assignees")]
		public List<long> Assignees { get; set; }

		/// <summary>
		/// Status of the task
		/// </summary>
		[JsonProperty("status")]
		public string Status { get; set; }

		/// <summary>
		/// Priority of the task
		/// </summary>
		[JsonProperty("priority")]
		public TaskPriority? Priority { get; set; }

		/// <summary>
		/// Due Date of the task
		/// </summary>
		[JsonProperty("due_date")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DueDate { get; set; }
        #endregion


        #region Constructor

        /// <summary>
        /// Constructor of RequestCreateTaskInList
        /// </summary>
        /// <param name="name"></param>
        public RequestCreateTaskInList(string name)
		{
			Name = name;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Validation method of data
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(Name))
			{
				throw new ArgumentNullException("Name");
			}
		}

		#endregion
	}
}