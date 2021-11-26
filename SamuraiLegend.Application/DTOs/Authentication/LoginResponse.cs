using System.Text.Json.Serialization;

namespace SamuraiLegend.Application.DTOs.Authentication
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public string JWToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
