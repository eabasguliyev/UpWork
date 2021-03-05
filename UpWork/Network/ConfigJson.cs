using Newtonsoft.Json;

namespace UpWork.Network
{
    public struct ConfigJson
    {
        [JsonProperty("mail")]
        public string Mail { get; private set; }

        [JsonProperty("password")]
        public string Password { get; private set; }
    }
}