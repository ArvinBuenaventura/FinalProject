using Newtonsoft.Json;

namespace HttpClientLibrary.DataModels
{
    /// <summary>
    /// Token JSON Model
    /// </summary>
    public class TokenModels
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
