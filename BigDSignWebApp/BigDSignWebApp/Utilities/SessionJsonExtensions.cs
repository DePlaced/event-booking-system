using Newtonsoft.Json;

namespace BigDSignWebApp.Utilities
{
    public static class SessionJsonExtensions
    {
        // Serialize an object as a JSON string and store it in the session.
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Retrieve a JSON string from the session and deserialize it to the specified type.
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value is not null ? JsonConvert.DeserializeObject<T>(value) : default(T);
        }
    }
}
