using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace VTS.Models.Impl{
    public class JsonUtilityImpl : IJsonUtility
    {
        public T FromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public string ToJson(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
