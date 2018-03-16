using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AMS.frontend.web.Extensions
{
    public static class SessionExtensions
    {
        #region Public Methods

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null
                ? default(T)
                : JsonConvert.DeserializeObject<T>(value,
                    new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        }

        public static void Set(this ISession session, string key, object value)
        {
            session.SetString(key,
                JsonConvert.SerializeObject(value,
                    new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
        }

        #endregion Public Methods
    }
}