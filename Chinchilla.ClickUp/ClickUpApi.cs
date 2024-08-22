using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinchilla.ClickUp.Helpers;
using Chinchilla.ClickUp.Params;
using Chinchilla.ClickUp.Requests;
using Chinchilla.ClickUp.Responses;
using Chinchilla.ClickUp.Responses.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Chinchilla.ClickUp
{

    /// <summary>
	/// Object that interact through methods the API (v2) of ClickUp
	/// </summary>
	public class ClickUpApi : IClickUpApi
    {
		#region Private Attributes

		/// <summary>
		/// Base Address of API call
		/// </summary>
		private static readonly Uri BaseAddress = new("https://api.clickup.com/api/v2/");

        private static readonly JsonSerializerSettings JsonSerializerSettings = new() { DateParseHandling = DateParseHandling.None };

        /// <summary>
		/// The Access Token to add during the request
		/// </summary>
		public string AccessToken { get; protected set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Create object with Personal Access Token
		/// </summary>
		/// <param name="accessToken">Personal Access Token</param>
		public ClickUpApi(string accessToken)
		{
			AccessToken = accessToken;
		}

		/// <summary>
		/// Create Object with <see cref="ParamsAccessToken"/>
		/// </summary>
		/// <param name="paramAccessToken">param access token object</param>
		public static ClickUpApi Create(ParamsAccessToken paramAccessToken)
		{
			var client = GetRestClient();
			var request = new RestRequest("oauth/token", Method.Post);
			request.AddParameter("client_id", paramAccessToken.ClientId);
			request.AddParameter("client_secret", paramAccessToken.ClientSecret);
			request.AddParameter("code", paramAccessToken.Code);

			// execute the request
			ResponseGeneric<ResponseAccessToken, ResponseError> response = RestSharperHelper.ExecuteRequest<ResponseAccessToken, ResponseError>(client, request);

			string accessToken;
			// Manage Response
			if (response.ResponseSuccess == null)
				throw new Exception(response.ResponseError.Err);

			accessToken = response.ResponseSuccess.AccessToken;

			return new ClickUpApi(accessToken);
		}

		/// <summary>
		/// Create Object with <see cref="ParamsAccessToken"/>
		/// </summary>
		/// <param name="paramAccessToken">param access token object</param>
		public static Task<ClickUpApi> CreateAsync(ParamsAccessToken paramAccessToken)
		{
			var client = GetRestClient();
			var request = new RestRequest("oauth/token", Method.Post);
			request.AddParameter("client_id", paramAccessToken.ClientId);
			request.AddParameter("client_secret", paramAccessToken.ClientSecret);
			request.AddParameter("code", paramAccessToken.Code);

			// execute the request
			var taskCompletionSource = new TaskCompletionSource<ClickUpApi>();
			_ = RestSharperHelper.ExecuteRequestAsync<ResponseAccessToken, ResponseError>(client, request)
				.ContinueWith(responseTask => {
					ResponseGeneric<ResponseAccessToken, ResponseError> response = responseTask.Result;

					// Manage Response
					if (response.ResponseSuccess == null)
						throw new Exception(response.ResponseError.Err);

					string accessToken = response.ResponseSuccess.AccessToken;

					taskCompletionSource.SetResult(new ClickUpApi(accessToken));
				});

			return taskCompletionSource.Task;
		}

		#endregion

		#region API Methods

		#region User
		/// <summary>
		/// Get the user that belongs to this token
		/// </summary>
		/// <returns>ResponseGeneric with ResponseAuthorizedUser response object</returns>
		public ResponseGeneric<ResponseAuthorizedUser, ResponseError> GetAuthorizedUser()
		{
			var client = GetRestClient();
			var request = new RestRequest($"user");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseAuthorizedUser, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseAuthorizedUser, ResponseError>(client, request);
			return result;
		}
		#endregion

		#region Teams
		/// <summary>
		/// Get the authorized teams for this token
		/// </summary>
		/// <returns>ResponseGeneric with ResponseAuthorizedTeams response object</returns>
		public ResponseGeneric<ResponseAuthorizedTeams, ResponseError> GetAuthorizedTeams()
		{
			var client = GetRestClient();
			var request = new RestRequest($"team");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseAuthorizedTeams, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseAuthorizedTeams, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Get a team's details. This team must be one of the authorized teams for this token.
		/// </summary>
		/// <param name="paramsGetTeamById">param object of get team by ID request</param>
		/// <returns>ResponseGeneric with ResponseTeam response object</returns>
		public ResponseGeneric<ResponseTeam, ResponseError> GetTeamById(ParamsGetTeamById paramsGetTeamById)
		{
			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsGetTeamById.TeamId}");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseTeam, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseTeam, ResponseError>(client, request);
			return result;
		}
		#endregion

		#region Spaces
		/// <summary>
		/// Get a team's spaces. This team must be one of the authorized teams for this token.
		/// </summary>
		/// <param name="paramsGetTeamSpace">param object of get team space request</param>
		/// <returns>ResponseGeneric with ResponseTeamSpace response object</returns>
		public ResponseGeneric<ResponseTeamSpaces, ResponseError> GetTeamSpaces(ParamsGetTeamSpaces paramsGetTeamSpace)
		{
			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsGetTeamSpace.TeamId}/space");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseTeamSpaces, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseTeamSpaces, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Create space in a Team
		/// </summary>
		/// <param name="paramsCreateTeamSpace">param object of create space request</param>
		/// <param name="requestData">RequestCreateTeamSpace object</param>
		/// <returns>ResponseGeneric with ModelList response object</returns>
		public ResponseGeneric<ResponseModelSpace, ResponseError> CreateTeamSpace(ParamsCreateTeamSpace paramsCreateTeamSpace, RequestCreateTeamSpace requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsCreateTeamSpace.TeamId}/space", Method.Post);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelSpace, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelSpace, ResponseError>(client, request);
			return result;
		}
		#endregion

		#region Folders
		/// <summary>
		/// Get a space's folders. The folders' lists will also be included.
		/// </summary>
		/// <param name="paramsGetSpaceFolders">param object of get space folder request</param>
		/// <returns>ResponseGeneric with ResponseSpaceFolders response object</returns>
		public ResponseGeneric<ResponseSpaceFolders, ResponseError> GetSpaceFolders(ParamsGetSpaceFolders paramsGetSpaceFolders)
		{
			var client = GetRestClient();
			var request = new RestRequest($"space/{paramsGetSpaceFolders.SpaceId}/folder");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseSpaceFolders, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseSpaceFolders, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Create a folder
		/// </summary>
		/// <param name="paramsCreateFolder">param object of create folder request</param>
		/// <param name="requestData">RequestCreateFolder object</param>
		/// <returns>ResponseGeneric with ModelFolder object expected</returns>
		public ResponseGeneric<ResponseModelFolder, ResponseError> CreateFolder(ParamsCreateFolder paramsCreateFolder, RequestCreateFolder requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var request = new RestRequest($"space/{paramsCreateFolder.SpaceId}/folder", Method.Post);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelFolder, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelFolder, ResponseError>(client, request);
			return result;
		}
		#endregion

		#region Lists
		/// <summary>
		/// Get a list by id
		/// </summary>
		/// <param name="paramsGetListById">param object of get list by id request</param>
		/// <returns>ResponseGeneric with ResponseModelList response object</returns>
		public ResponseGeneric<ResponseModelList, ResponseError> GetListById(ParamsGetListById paramsGetListById)
		{
			var client = GetRestClient();
			var request = new RestRequest($"list/{paramsGetListById.ListId}");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseModelList, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelList, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Create List in Folder
		/// </summary>
		/// <param name="paramsCreateList">param object of create list request</param>
		/// <param name="requestData">RequestCreateList object</param>
		/// <returns>ResponseGeneric with ModelList response object</returns>
		public ResponseGeneric<ResponseModelList, ResponseError> CreateList(ParamsCreateFolderList paramsCreateList, RequestCreateList requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var request = new RestRequest($"folder/{paramsCreateList.FolderId}/list", Method.Post);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelList, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelList, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Get a space's lists AKA folderless lists
		/// </summary>
		/// <param name="paramsGetFolderlessLists">param object of get folderless lists request</param>
		/// <returns>ResponseGeneric with ResponseFolderlessLists response object</returns>
		public ResponseGeneric<ResponseFolderlessLists, ResponseError> GetFolderlessLists(ParamsGetFolderlessLists paramsGetFolderlessLists)
		{
			var client = GetRestClient();
			var request = new RestRequest($"space/{paramsGetFolderlessLists.SpaceId}/list");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseFolderlessLists, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseFolderlessLists, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Create folderless List
		/// </summary>
		/// <param name="paramsCreateList">param object of create list request</param>
		/// <param name="requestData">RequestCreateList object</param>
		/// <returns>ResponseGeneric with ModelList response object</returns>
		public ResponseGeneric<ResponseModelList, ResponseError> CreateFolderlessList(ParamsCreateFolderlessList paramsCreateList, RequestCreateList requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var request = new RestRequest($"space/{paramsCreateList.SpaceId}/list", Method.Post);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelList, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelList, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Edit List informations
		/// </summary>
		/// <param name="paramsEditList">params object of edit list request</param>
		/// <param name="requestData">RequestEditList object</param>
		/// <returns>ResponseGeneric with ModelList response object</returns>
		public ResponseGeneric<ResponseModelList, ResponseError> EditList(ParamsEditList paramsEditList, RequestEditList requestData)
		{
			var client = GetRestClient();
			var request = new RestRequest($"list/{paramsEditList.ListId}", Method.Put);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelList, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelList, ResponseError>(client, request);
			return result;
		}

        /// <summary>
        /// View the people who have access to a List.
        /// </summary>
        public ResponseGeneric<ResponseModelListMembers, ResponseError> GetMembersInList(ParamsGetMembersInList paramsGetMembersInList)
        {
            var client = GetRestClient();
            var request = new RestRequest($"list/{paramsGetMembersInList.ListId}/member");
            request.AddHeader("authorization", AccessToken);

            // execute the request
            var result = RestSharperHelper.ExecuteRequest<ResponseModelListMembers, ResponseError>(client, request);
            return result;
        }
        #endregion

        #region Tasks
        /// <summary>
        /// Get a task by id
        /// </summary>
        /// <param name="paramsGetTaskById">param object of get task by id request</param>
        /// <returns>ResponseGeneric with ResponseModelTask response object</returns>
        public ResponseGeneric<ResponseModelTask, ResponseError> GetTaskById(ParamsGetTaskById paramsGetTaskById)
		{
			var client = GetRestClient();
            var resource = string.IsNullOrEmpty(paramsGetTaskById.CustomTaskId) ? 
                $"task/{paramsGetTaskById.TaskId}" :
                $"task/{paramsGetTaskById.CustomTaskId}/?custom_task_ids=true&team_id={paramsGetTaskById.TeamId}";
            var request = new RestRequest(resource);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			var result = RestSharperHelper.ExecuteRequest<ResponseModelTask, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Get Tasks of the Team and filter its by optionalParams
		/// </summary>
		/// <param name="paramsGetTasks">params object of get tasks request</param>
		/// <returns>ResponseGeneric with ResponseTasks response object</returns>
		public ResponseGeneric<ResponseTasks, ResponseError> GetTasks(ParamsGetTasks paramsGetTasks)
		{
			var client = GetRestClient();
            var resource = $"team/{paramsGetTasks.TeamId}/task";

            var additionalParams = new List<string>();
            if (paramsGetTasks.Page > 0)
                additionalParams.Add($"page={paramsGetTasks.Page}");
            if (paramsGetTasks.IncludeClosed is not null)
                additionalParams.Add($"include_closed={paramsGetTasks.IncludeClosed.Value}");
            if (paramsGetTasks.Subtasks is not null)
                additionalParams.Add($"subtasks={paramsGetTasks.Subtasks.Value}");
			if (paramsGetTasks.CustomFieldFilters.Any())
				additionalParams.Add($"custom_fields=[{string.Join(',', paramsGetTasks.CustomFieldFilters.Select(f => f.Expression))}]");

            // parameter below causes an error: {"err":"Internal server error","ECODE":"ITEMV2_003"}
            //if (paramsGetTasks.SpaceIds is not null && paramsGetTasks.SpaceIds.Any())
            //    additionalParams.Add($"space_ids=[{string.Join(',', paramsGetTasks.SpaceIds)}]");

            if (additionalParams.Count > 0)
                resource += "?" + string.Join("&", additionalParams);

            var request = new RestRequest(resource);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseTasks, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseTasks, ResponseError>(client, request);
			return result;
		}

        /// <summary>
        /// Get Tasks of the Team and filter its by optionalParams
        /// </summary>
        /// <param name="paramsGetTasks">params object of get tasks request</param>
        /// <returns>ResponseGeneric with ResponseTasks response object</returns>
        public ResponseGeneric<ResponseTasks, ResponseError> GetTasks(ParamsGetTasksFromList paramsGetTasks)
        {
            var client = GetRestClient();
            var resource = $"list/{paramsGetTasks.ListId}/task";

            var additionalParams = new List<string>();
            if (paramsGetTasks.Page > 0)
                additionalParams.Add($"page={paramsGetTasks.Page}");
			if(paramsGetTasks.IncludeClosed)
                additionalParams.Add("include_closed=true");
			if(paramsGetTasks.IncludeSubTasks)
                additionalParams.Add("subtasks=true");

			if (additionalParams.Count > 0)
                resource += "?" + string.Join("&", additionalParams);

            var request = new RestRequest(resource);
            request.AddHeader("authorization", AccessToken);

            // execute the request
            ResponseGeneric<ResponseTasks, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseTasks, ResponseError>(client, request);
            return result;
        }

		/// <summary>
		/// Create Task in List.
		/// </summary>
		/// <param name="paramsCreateTaskInList">params object of create task in list request</param>
		/// <param name="requestData">RequestCreateTaskInList object</param>
		/// <returns>ResponseGeneric with ModelTask object Expected</returns>
		public ResponseGeneric<ResponseModelTask, ResponseError> CreateTaskInList(ParamsCreateTaskInList paramsCreateTaskInList, RequestCreateTaskInList requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var createListRequest = new RestRequest($"list/{paramsCreateTaskInList.ListId}/task", Method.Post);
			createListRequest.AddHeader("authorization", AccessToken);
			createListRequest.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelTask, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelTask, ResponseError>(client, createListRequest);
			return result;
		}

		/// <summary>
		/// Edit Task informations.
		/// </summary>
		/// <param name="paramsEditTask">param object of Edit Task request</param>
		/// <param name="requestData">RequestEditTask object</param>
		/// <returns>ResponseGeneric with ResponseSuccess response object</returns>
		public ResponseGeneric<ResponseModelTask, ResponseError> EditTask(ParamsEditTask paramsEditTask, RequestEditTask requestData)
        {
            throw new ApplicationException("This method damages the task. Do not use it without refactoring.");
            //var client = GetRestClient();
            //var request = new RestRequest($"task/{paramsEditTask.TaskId}", Method.Put);
            //request.AddHeader("authorization", AccessToken);
            //request.AddJsonBody(requestData);

            //// execute the request
            //ResponseGeneric<ResponseModelTask, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelTask, ResponseError>(client, request);
            //return result;
        }

        /// <summary>
        /// Edit Task information.
        /// </summary>
        /// <param name="paramsEditTask">param object of Edit Task request</param>
        /// <param name="requestData">RequestEditTask object. Should contain only fields which need to be modified.</param>
        /// <returns>ResponseGeneric with ResponseSuccess response object</returns>
        public ResponseGeneric<ResponseModelTask, ResponseError> EditTask(ParamsEditTask paramsEditTask, object requestData)
        {
            var client = GetRestClient();
            var request = new RestRequest($"task/{paramsEditTask.TaskId}", Method.Put);
            request.AddHeader("authorization", AccessToken);
            request.AddJsonBody(requestData);

            // execute the request
            var result = RestSharperHelper.ExecuteRequest<ResponseModelTask, ResponseError>(client, request);
            return result;
        }


		/// <summary>
		/// Edit Task custom field.
		/// </summary>
		/// <param name="paramsEditTaskCustomField">param object of Edit Task request</param>
		/// <param name="value">custom field value as object</param>
		/// <returns>ResponseGeneric with ResponseSuccess response object</returns>
		public ResponseGeneric<ResponseSuccess, ResponseError> EditTaskCustomField(ParamsEditTaskCustomField paramsEditTaskCustomField, object value)
        {
            var client = GetRestClient();
            var request = new RestRequest($"task/{paramsEditTaskCustomField.TaskId}/field/{paramsEditTaskCustomField.FieldId}/", Method.Post);
            request.AddHeader("authorization", AccessToken);
            request.AddJsonBody(new{ value});

            // execute the request
            var result = RestSharperHelper.ExecuteRequest<ResponseSuccess, ResponseError>(client, request);
            return result;
        }

        /// <summary>
        /// Get Accessible Custom Fields, which available for the particular list
        /// </summary>
        /// <param name="paramsGetListById"></param>
        /// <returns></returns>
        public ResponseGeneric<ResponseModelAccessibleCustomFields, ResponseError> GetAccessibleCustomFields(ParamsGetListById paramsGetListById)
        {
            var client = GetRestClient();
            var request = new RestRequest($"list/{paramsGetListById.ListId}/field");
            request.AddHeader("authorization", AccessToken);

            // execute the request
            var result = RestSharperHelper.ExecuteRequest<ResponseModelAccessibleCustomFields, ResponseError>(client, request);
            return result;
        }

        /// <summary>
        /// Delete a task by id
        /// </summary>
        /// <param name="paramsGetTaskById">param object of get task by id request</param>
        /// <returns>ResponseGeneric with ResponseModelTask response object</returns>
        public ResponseGeneric<ResponseSuccess, ResponseError> DeleteTaskById(ParamsGetTaskById paramsGetTaskById)
        {
            var client = GetRestClient();
            var resource = string.IsNullOrEmpty(paramsGetTaskById.CustomTaskId) ?
                $"task/{paramsGetTaskById.TaskId}" :
                $"task/{paramsGetTaskById.CustomTaskId}/?custom_task_ids=true&team_id={paramsGetTaskById.TeamId}";
            var request = new RestRequest(resource, Method.Delete);
            request.AddHeader("authorization", AccessToken);

            // execute the request
            var result = RestSharperHelper.ExecuteRequest<ResponseSuccess, ResponseError>(client, request);
            return result;
        }

        /// <summary>
        /// Get task's comments by a task id
        /// </summary>
        /// <param name="paramsGetTaskById">param object of get task by id request</param>
        /// <returns>ResponseGeneric with ResponseModelTask response object</returns>
        public ResponseGeneric<ResponseModelTaskComments, ResponseError> GetTaskComments(ParamsGetTaskById paramsGetTaskById)
        {
            var client = GetRestClient();
            var resource = string.IsNullOrEmpty(paramsGetTaskById.CustomTaskId) ?
                $"task/{paramsGetTaskById.TaskId}/comment" :
                $"task/{paramsGetTaskById.CustomTaskId}/comment?custom_task_ids=true&team_id={paramsGetTaskById.TeamId}";
            var request = new RestRequest(resource);
            request.AddHeader("authorization", AccessToken);

            var result = RestSharperHelper.ExecuteRequest<ResponseModelTaskComments, ResponseError>(client, request);
            return result;
        }

        public ResponseGeneric<ResponseCreatedTaskComment, ResponseError> CreateTaskComment(
            ParamsGetTaskById paramsGetTaskById, RequestCreateTaskComment requestData)
        {
			var client = GetRestClient();
            var resource = string.IsNullOrEmpty(paramsGetTaskById.CustomTaskId) ?
                $"task/{paramsGetTaskById.TaskId}/comment" :
                $"task/{paramsGetTaskById.CustomTaskId}/comment?custom_task_ids=true&team_id={paramsGetTaskById.TeamId}";
            var request = new RestRequest(resource, Method.Post);
            request.AddHeader("authorization", AccessToken);
            request.AddJsonBody(requestData);

            var result = RestSharperHelper.ExecuteRequest<ResponseCreatedTaskComment, ResponseError>(client, request);
            return result;
        }
        #endregion

        #region Webhooks
        /// <summary>
        /// Get a team's webhooks. This team must be one of the authorized teams for this token.
        /// </summary>
        /// <param name="paramsGetTeamWebhook">param object of get team Webhook request</param>
        /// <returns>ResponseGeneric with ResponseTeamWebhook response object</returns>
        public ResponseGeneric<ResponseWebhooks, ResponseError> GetWebhooks(ParamsGetTeamWebhooks paramsGetTeamWebhook)
		{
			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsGetTeamWebhook.TeamId}/webhook");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			var result = RestSharperHelper.ExecuteRequest<ResponseWebhooks, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Create a webhook in a Team
		/// </summary>
		/// <param name="paramsCreateWebhook">param object of create webhook request</param>
		/// <param name="requestData">RequestCreateTeamWebhook object</param>
		/// <returns>ResponseGeneric with ResponseWebhook response object</returns>
		public ResponseGeneric<ResponseWebhook, ResponseError> CreateWebhook(ParamsCreateWebhook paramsCreateWebhook, RequestCreateWebhook requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsCreateWebhook.TeamId}/webhook", Method.Post);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			var result = RestSharperHelper.ExecuteRequest<ResponseWebhook, ResponseError>(client, request);
			return result;
		}

        public ResponseGeneric<ResponseWebhook, ResponseError> EditWebhook(ParamsEditWebhook paramEditWebhook, RequestEditWebhook requestData)
        {
            requestData.ValidateData();

            var client = GetRestClient();
            var request = new RestRequest($"webhook/{paramEditWebhook.WebhookId}", Method.Put);
            request.AddHeader("authorization", AccessToken);
            request.AddJsonBody(requestData);

            // execute the request
            var result = RestSharperHelper.ExecuteRequest<ResponseWebhook, ResponseError>(client, request);
            return result;
        }

        #endregion

        #endregion

        #region API Methods Async

        #region User
        /// <summary>
        /// Get the user that belongs to this token
        /// </summary>
        /// <returns>ResponseGeneric with ResponseAuthorizedUser object expected</returns>
        public Task<ResponseGeneric<ResponseAuthorizedUser, ResponseError>> GetAuthorizedUserAsync()
		{
			var client = GetRestClient();
			var request = new RestRequest($"user");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseAuthorizedUser, ResponseError>(client, request);
		}
		#endregion

		#region Teams
		/// <summary>
		/// Get the authorized teams for this token
		/// </summary>
		/// <returns>ResponseGeneric with ResponseAuthorizedTeams expected</returns>
		public Task<ResponseGeneric<ResponseAuthorizedTeams, ResponseError>> GetAuthorizedTeamsAsync()
		{
			var client = GetRestClient();
			var request = new RestRequest($"team");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseAuthorizedTeams, ResponseError>(client, request);
		}

		/// <summary>
		/// Get a team's details. This team must be one of the authorized teams for this token.
		/// </summary>
		/// <param name="paramsGetTeamById">param object of get team by ID request</param>
		/// <returns>ResponseGeneric with ResponseTeam response object</returns>
		public Task<ResponseGeneric<ResponseTeam, ResponseError>> GetTeamByIdAsync(ParamsGetTeamById paramsGetTeamById)
		{
			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsGetTeamById.TeamId}");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseTeam, ResponseError>(client, request);
		}
		#endregion

		#region Spaces
		/// <summary>
		/// Get a team's spaces. This team must be one of the authorized teams for this token.
		/// </summary>
		/// <param name="paramsGetTeamSpace">param object of get team space request</param>
		/// <returns>ResponseGeneric with ResponseTeamSpace object expected</returns>
		public Task<ResponseGeneric<ResponseTeamSpaces, ResponseError>> GetTeamSpacesAsync(ParamsGetTeamSpaces paramsGetTeamSpace)
		{
			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsGetTeamSpace.TeamId}/space");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseTeamSpaces, ResponseError>(client, request);
		}

		/// <summary>
		/// Create space in a Team
		/// </summary>
		/// <param name="paramsCreateTeamSpace">param object of create space request</param>
		/// <param name="requestData">RequestCreateTeamSpace object</param>
		/// <returns>ResponseGeneric with ModelList response object</returns>
		public Task<ResponseGeneric<ResponseModelSpace, ResponseError>> CreateTeamSpaceAsync(ParamsCreateTeamSpace paramsCreateTeamSpace, RequestCreateTeamSpace requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsCreateTeamSpace.TeamId}/space", Method.Post);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelSpace, ResponseError>(client, request);
		}
		#endregion

		#region Folders
		/// <summary>
		/// Get a space's folders. The folders' lists will also be included.
		/// </summary>
		/// <param name="paramsGetSpaceFolders">params object of get space folders request</param>
		/// <returns>ResponseGeneric with ResponseSpaceFolders object expected</returns>
		public Task<ResponseGeneric<ResponseSpaceFolders, ResponseError>> GetSpaceFoldersAsync(ParamsGetSpaceFolders paramsGetSpaceFolders)
		{
			var client = GetRestClient();
			var request = new RestRequest($"space/{paramsGetSpaceFolders.SpaceId}/folder");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseSpaceFolders, ResponseError>(client, request);
		}

		/// <summary>
		/// Create a folder
		/// </summary>
		/// <param name="paramsCreateFolder">param object of create folder request</param>
		/// <param name="requestData">RequestCreateFolder object</param>
		/// <returns>ResponseGeneric with ModelFolder object expected</returns>
		public Task<ResponseGeneric<ResponseModelFolder, ResponseError>> CreateFolderAsync(ParamsCreateFolder paramsCreateFolder, RequestCreateFolder requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var request = new RestRequest($"space/{paramsCreateFolder.SpaceId}/folder", Method.Post);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelFolder, ResponseError>(client, request);
		}
		#endregion

		#region Lists
		/// <summary>
		/// Get a list by id
		/// </summary>
		/// <param name="paramsGetListById">param object of get list by id request</param>
		/// <returns>ResponseGeneric with ResponseModelList response object</returns>
		public Task<ResponseGeneric<ResponseModelList, ResponseError>> GetListByIdAsync(ParamsGetListById paramsGetListById)
		{
			var client = GetRestClient();
			var request = new RestRequest($"list/{paramsGetListById.ListId}");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelList, ResponseError>(client, request);
		}

		/// <summary>
		/// Create List in Folder
		/// </summary>
		/// <param name="paramsCreateList">param object of create list request</param>
		/// <param name="requestData">RequestCreateList object</param>
		/// <returns>ResponseGeneric with ModelList object expected</returns>
		public Task<ResponseGeneric<ResponseModelList, ResponseError>> CreateListAsync(ParamsCreateFolderList paramsCreateList, RequestCreateList requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var request = new RestRequest($"folder/{paramsCreateList.FolderId}/list", Method.Post);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelList, ResponseError>(client, request);
		}

		/// <summary>
		/// Get a space's lists AKA folderless lists
		/// </summary>
		/// <param name="paramsGetFolderlessLists">param object of get folderless lists request</param>
		/// <returns>ResponseGeneric with ResponseFolderlessLists response object</returns>
		public Task<ResponseGeneric<ResponseFolderlessLists, ResponseError>> GetFolderlessListsAsync(ParamsGetFolderlessLists paramsGetFolderlessLists)
		{
			var client = GetRestClient();
			var request = new RestRequest($"space/{paramsGetFolderlessLists.SpaceId}/list");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseFolderlessLists, ResponseError>(client, request);
		}

		/// <summary>
		/// Create a folderless List
		/// </summary>
		/// <param name="paramsCreateList">param object of create list request</param>
		/// <param name="requestData">RequestCreateList object</param>
		/// <returns>ResponseGeneric with ModelList object expected</returns>
		public Task<ResponseGeneric<ResponseModelList, ResponseError>> CreateFolderlessListAsync(ParamsCreateFolderlessList paramsCreateList, RequestCreateList requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var request = new RestRequest($"space/{paramsCreateList.SpaceId}/list", Method.Post);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelList, ResponseError>(client, request);
		}

		/// <summary>
		/// Edit List informations
		/// </summary>
		/// <param name="paramsEditList">param object of Edi List request</param>
		/// <param name="requestData">RequestEditList object</param>
		/// <returns>ResponseGeneric with ModelList object expected</returns>
		public Task<ResponseGeneric<ResponseModelList, ResponseError>> EditListAsync(ParamsEditList paramsEditList, RequestEditList requestData)
		{
			var client = GetRestClient();
			var request = new RestRequest($"list/{paramsEditList.ListId}", Method.Put);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelList, ResponseError>(client, request);
		}
		#endregion

		#region Tasks
		/// <summary>
		/// Get a task by id
		/// </summary>
		/// <param name="paramsGetTaskById">param object of get task by id request</param>
		/// <returns>ResponseGeneric with ResponseModelTask response object</returns>
		public Task<ResponseGeneric<ResponseModelTask, ResponseError>> GetTaskByIdAsync(ParamsGetTaskById paramsGetTaskById)
		{
			var client = GetRestClient();
            var resource = string.IsNullOrEmpty(paramsGetTaskById.CustomTaskId) ?
                $"task/{paramsGetTaskById.TaskId}" :
                $"task/{paramsGetTaskById.CustomTaskId}/?custom_task_ids=true&team_id={paramsGetTaskById.TeamId}";
            var request = new RestRequest(resource);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelTask, ResponseError>(client, request);
		}

		/// <summary>
		/// Get Tasks of the Team and filter its by optionalParams
		/// </summary>
		/// <param name="paramsGetTasks">param object of get tasks request</param>
		/// <returns>ResponseGeneric with ResponseTasks object expected</returns>
		public Task<ResponseGeneric<ResponseTasks, ResponseError>> GetTasksAsync(ParamsGetTasks paramsGetTasks)
		{
			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsGetTasks.TeamId}/task");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseTasks, ResponseError>(client, request);
		}

		/// <summary>
		/// Create Task in List.
		/// </summary>
		/// <param name="paramsCreateTaskInList">param object of Create Task in List request</param>
		/// <param name="requestData">RequestCreateTaskInList object</param>
		/// <returns>ResponseGeneric with ModelTask object Expected</returns>
		public Task<ResponseGeneric<ResponseModelTask, ResponseError>> CreateTaskInListAsync(ParamsCreateTaskInList paramsCreateTaskInList, RequestCreateTaskInList requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var createListRequest = new RestRequest($"list/{paramsCreateTaskInList.ListId}/task", Method.Post);
			createListRequest.AddHeader("authorization", AccessToken);
			createListRequest.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelTask, ResponseError>(client, createListRequest);
		}

		/// <summary>
		/// Edit Task informations.
		/// </summary>
		/// <param name="paramsEditTask">param object of edit task request</param>
		/// <param name="requestData">RequestEditTask object</param>
		/// <returns>ResponseGeneric with ResponseSuccess object expected</returns>
		public Task<ResponseGeneric<ResponseModelTask, ResponseError>> EditTaskAsync(ParamsEditTask paramsEditTask, RequestEditTask requestData)
		{
            throw new ApplicationException("This method damages the task. Do not use it without refactoring.");
			//var client = GetRestClient();
			//var request = new RestRequest($"task/{paramsEditTask.TaskId}", Method.Put);
			//request.AddHeader("authorization", AccessToken);
			//request.AddJsonBody(requestData);

			//// execute the request
			//return RestSharperHelper.ExecuteRequestAsync<ResponseModelTask, ResponseError>(client, request);
		}
		
        #endregion

		#region Webhooks
		/// <summary>
		/// Get a team's webhooks. This team must be one of the authorized teams for this token.
		/// </summary>
		/// <param name="paramsGetTeamWebhook">param object of get team Webhook request</param>
		/// <returns>ResponseGeneric with ResponseTeamWebhook response object</returns>
		public Task<ResponseGeneric<ResponseWebhooks, ResponseError>> GetTeamWebhooksAsync(ParamsGetTeamWebhooks paramsGetTeamWebhook)
		{
			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsGetTeamWebhook.TeamId}/webhook");
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseWebhooks, ResponseError>(client, request);
		}

		/// <summary>
		/// Create a webhook in a Team
		/// </summary>
		/// <param name="paramsCreateTeamWebhook">param object of create webhook request</param>
		/// <param name="requestData">RequestCreateTeamWebhook object</param>
		/// <returns>ResponseGeneric with ResponseWebhook response object</returns>
		public Task<ResponseGeneric<ResponseWebhook, ResponseError>> CreateTeamWebhookAsync(ParamsCreateWebhook paramsCreateTeamWebhook, RequestCreateWebhook requestData)
		{
			requestData.ValidateData();

			var client = GetRestClient();
			var request = new RestRequest($"team/{paramsCreateTeamWebhook.TeamId}/webhook", Method.Post);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseWebhook, ResponseError>(client, request);
		}
        #endregion

        #endregion

        private static RestClient GetRestClient() =>
            new (BaseAddress, configureSerialization: s => s.UseNewtonsoftJson(JsonSerializerSettings));
    }
}