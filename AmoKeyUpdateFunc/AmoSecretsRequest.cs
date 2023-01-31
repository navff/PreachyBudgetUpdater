using System;
using Newtonsoft.Json;

namespace AmoKeyUpdateFunc
{
    public class AmoSecretsRequest
    {
        [JsonProperty("client_id")] 
        public string ClientId { get; set; } = string.Empty;
        
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; } = string.Empty;
        
        [JsonProperty("grant_type")]
        public string GrantType { get; set; } = string.Empty;
        
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; } = string.Empty;
        
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}