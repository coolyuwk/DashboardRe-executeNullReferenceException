using DotNetCore.CAP.Filter;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public static class JsonExtension
    {
        private static JsonSerializerOptions _jsonSerializerOptions;
        public static JsonSerializerOptions JsonSerializerOptions
        {
            get
            {
                if (_jsonSerializerOptions == null)
                {
                    _jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = false };
                }
                return _jsonSerializerOptions;
            }
        }

        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj, JsonSerializerOptions);
        }

        public static T ToObject<T>(this string str)
        {
            return JsonSerializer.Deserialize<T>(str, options: JsonSerializerOptions);
        }
    }
}
