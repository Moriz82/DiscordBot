using Newtonsoft.Json;

namespace DiscordBot
{
    public class ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("menu")]
        public string Menu { get; set; }
    }
}