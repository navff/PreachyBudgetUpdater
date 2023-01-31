using Newtonsoft.Json;

namespace AmoKeyUpdateFunc
{
    public class AmoTokenConfig
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; } = "Bearer";
        
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;
        
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}