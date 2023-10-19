using System;

namespace Chinchilla.ClickUp.Params
{

    /// <summary>
    /// The param object of view the people who have access to a List.
    /// </summary>
    public class ParamsGetMembersInList
    {
		#region Attributes

		/// <summary>
		/// The List Id
		/// </summary>
		public string ListId { get; }

		#endregion


		#region Constructor

		/// <summary>
		/// The Constructor of <see cref="ParamsGetMembersInList"/>
		/// </summary>
		/// <param name="listId"></param>
		public ParamsGetMembersInList(string listId) => ListId = listId;

        #endregion


		#region Public Methods

		/// <summary>
		/// Method that validate data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(ListId))
			{
				throw new ArgumentNullException("ListId");
			}
		}

		#endregion

	}
}