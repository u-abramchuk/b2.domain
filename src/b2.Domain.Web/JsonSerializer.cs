using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace b2.Domain.Web
{
    public class JsonSerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}