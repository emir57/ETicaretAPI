using System.Text.Json.Serialization;

namespace ETicaretAPI.Application.Dtos.Facebook
{
    public class FacebookUserAccessTokenValidation
    {
        [JsonPropertyName("data.is_valid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("data.user_id")]
        public string UserId { get; set; }
    }
}
