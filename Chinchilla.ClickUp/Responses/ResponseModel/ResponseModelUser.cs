using System;
using System.Runtime.Serialization;
using Chinchilla.ClickUp.Enums;
using Chinchilla.ClickUp.Helpers;
using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{
    /// <summary>
    /// Model Object of User information response
    /// </summary>
    [Serializable]
    [DataContract]
    public class ResponseModelUser
        : IResponse
    {
        /// <summary>
        /// Id of the User
        /// </summary>
        [JsonProperty("id")]
        [DataMember(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Username of the User
        /// </summary>
        [JsonProperty("username")]
        [DataMember(Name = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Email address of the User
        /// </summary>
        [JsonProperty("email")]
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Theme Color selected by the User
        /// </summary>
        [JsonProperty("color")]
        [DataMember(Name = "color")]
        public string Color { get; set; }

        /// <summary>
        /// The url of the Profile Picture of the user
        /// </summary>
        [JsonProperty("profilePicture")]
        [DataMember(Name = "profilePicture")]
        public string ProfilePicture { get; set; }

        /// <summary>
        /// Initials of the User
        /// </summary>
        [JsonProperty("initials")]
        [DataMember(Name = "initials")]
        public string Initials { get; set; }

        /// <summary>
        /// The start day of the Week setted by the user
        /// </summary>
        [JsonProperty("week_start_day")]
        [DataMember(Name = "week_start_day")]
        public DayOfWeek? WeekStartDay { get; set; }

        /// <summary>
        /// Check if user support global font
        /// </summary>
        [JsonProperty("global_font_support")]
        [DataMember(Name = "global_font_support")]
        public bool? GlobalFontSupport { get; set; }

        /// <summary>
        /// Role of the user
        /// </summary>
        [JsonProperty("role")]
        [DataMember(Name = "role")]
        public UserRole? Role { get; set; }

        /// <summary>
        /// The <see cref="DateTime"/> the user was last active
        /// </summary>
        [JsonProperty("last_active")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [DataMember(Name = "last_active")]
        public DateTime? DateLastActive { get; set; }

        /// <summary>
        /// The <see cref="DateTime"/> the user joined the team
        /// </summary>
        [JsonProperty("date_joined")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [DataMember(Name = "date_joined")]
        public DateTime? DateJoined { get; set; }

        /// <summary>
        /// The <see cref="DateTime"/> the user was invited to the team
        /// </summary>
        [JsonProperty("date_invited")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [DataMember(Name = "date_invited")]
        public DateTime? DateInvited { get; set; }

        [JsonProperty("profileInfo")]
        public ResponseModelProfileInfo ProfileInfo { get; set; }
    }
}