namespace SistePay.TiendaNube.API.Models;

public class TokenResponse
{
    public string access_token { get; set; } = string.Empty;
    public string token_type { get; set; } = string.Empty;
    public string scope { get; set; } = string.Empty;
    public int user_id { get; set; }
}
