using Chinchilla.ClickUp.Helpers;
using Chinchilla.ClickUp.Params;
using Chinchilla.ClickUp.Requests;
using Chinchilla.ClickUp.Responses;
using Chinchilla.ClickUp.Responses.Model;

namespace Chinchilla.ClickUp;

public interface IClickUpApi
{
    /// <summary>
    /// Create Task in List.
    /// </summary>
    ResponseGeneric<ResponseModelTask, ResponseError> CreateTaskInList(ParamsCreateTaskInList paramsCreateTaskInList, RequestCreateTaskInList requestData);

    /// <summary>
    /// Create a comment for a task
    /// </summary>
    ResponseGeneric<ResponseCreatedTaskComment, ResponseError> CreateTaskComment(
        ParamsGetTaskById paramsGetTaskById, RequestCreateTaskComment requestData);
}