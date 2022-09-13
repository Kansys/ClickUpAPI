using System;
using System.Linq;
using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Params
{
    public class ParamsUpdateRelationships
    {
        [JsonProperty("add")]
        public string[] TasksToAdd { get; }

        [JsonProperty("rem")]
        public string[] TasksToRemove { get; }

        public ParamsUpdateRelationships(string[] tasksToAdd, string[] tasksToRemove)
        {
            TasksToAdd = tasksToAdd;
            TasksToRemove = tasksToRemove;
        }
        public ParamsUpdateRelationships(string[] tasksToAdd): this(tasksToAdd, Array.Empty<string>())
        {
        }

        public override string ToString()
        {
            var toRemove = TasksToRemove.Any() ? $" To remove: {string.Join(',', TasksToRemove)}." : "";
            return $"To add: {string.Join(',', TasksToAdd)}.{toRemove}";
        }
    }
}
