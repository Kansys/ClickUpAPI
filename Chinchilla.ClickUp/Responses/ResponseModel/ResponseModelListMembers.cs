using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.Responses.Model;

/// <summary>
/// Model object of people who have access to a List response
/// </summary>
public class ResponseModelListMembers
    : Helpers.IResponse
{

    /// <summary>
    /// List of members
    /// </summary>
    [JsonProperty("members")]
    public List<ResponseModelUser> Members { get; set; }
}