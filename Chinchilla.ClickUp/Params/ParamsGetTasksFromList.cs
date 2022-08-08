using System;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of Get Task request
	/// </summary>
	public class ParamsGetTasksFromList
	{
		#region Attributes

		/// <summary>
		/// The Team Id 
		/// </summary>
		public string ListId { get; set; }

		/// <summary>
		/// Page to fetch (starts at 0)
		/// </summary>
		public int Page { get; set; } = 0;

        public bool IncludeClosed { get; set; } = false;
		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of ParamsGetTasksFromList
		/// </summary>
		/// <param name="listId"></param>
		public ParamsGetTasksFromList(string listId)
		{
            ListId = listId;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Method that validate the data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(ListId))
			{
				throw new ArgumentNullException(nameof(ListId));
			}
		}

		#endregion
	}
}