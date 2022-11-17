using System;
using System.Linq;
using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Params
{
    public class ParamsUpdateUsers
    {
        [JsonProperty("add")]
        public long[] UsersToAdd { get; }

        [JsonProperty("rem")]
        public long[] UsersToRemove { get; }

        public ParamsUpdateUsers(long[] usersToAdd, long[] usersToRemove)
        {
            UsersToAdd = usersToAdd;
            UsersToRemove = usersToRemove;
        }
        public ParamsUpdateUsers(long[] usersToAdd): this(usersToAdd, Array.Empty<long>())
        {
        }

        public override string ToString()
        {
            var toRemove = UsersToRemove.Any() ? $" To remove: {string.Join(',', UsersToRemove)}." : "";
            return $"To add: {string.Join(',', UsersToAdd)}.{toRemove}";
        }
    }
}
