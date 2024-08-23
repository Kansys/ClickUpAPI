using System;
using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Helpers
{
	/// <summary>
	/// Json Converter class of DateTime object to / from milliseconds time
	/// </summary>
	public class JsonConverterDateTimeMilliseconds : JsonConverter
	{
		/// <summary>
		/// Check if can convert this type of object. (Can only convert DateTime)
		/// </summary>
		/// <param name="objectType">object Type</param>
		/// <returns>Bool if can convert this object</returns>
		public override bool CanConvert(Type objectType)
		{
			return typeof(DateTime).IsAssignableFrom(objectType) || typeof(DateTime?).IsAssignableFrom(objectType);
		}

		/// <summary>
		/// Read long with milliseconds and convert to Datetime with Unix method
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="objectType"></param>
		/// <param name="existingValue"></param>
		/// <param name="serializer"></param>
		/// <returns></returns>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			object value = reader.Value;
            if (value == null) return null;

            var milliseconds = value is string s
                ? long.Parse(s)
                : (long)value;
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime;
        }

		/// <summary>
		/// Get Datetime and serialize it to long for Unix method
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="value"></param>
		/// <param name="serializer"></param>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value == null ? null : (object)new DateTimeOffset(((DateTime)value)).ToUnixTimeMilliseconds());
		}
	}
}