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

    /// <summary>
    /// Get a space's folders. The folders' lists will also be included.
    /// </summary>
    /// <param name="paramsGetSpaceFolders">param object of get space folder request</param>
    /// <returns>ResponseGeneric with ResponseSpaceFolders response object</returns>
    ResponseGeneric<ResponseSpaceFolders, ResponseError> GetSpaceFolders(ParamsGetSpaceFolders paramsGetSpaceFolders);

    /// <summary>
    /// Edit Task custom field.
    /// </summary>
    /// <param name="paramsEditTaskCustomField">param object of Edit Task request</param>
    /// <param name="value">custom field value as object</param>
    /// <returns>ResponseGeneric with ResponseSuccess response object</returns>
    ResponseGeneric<ResponseSuccess, ResponseError> EditTaskCustomField(ParamsEditTaskCustomField paramsEditTaskCustomField, object value);
}