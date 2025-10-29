using SistePay.TiendaNube.API.Models;
using System.Text;
using System.Text.Json;

namespace SistePay.TiendaNube.API.Services;

public class TiendaNubeService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<TiendaNubeService> _logger;
    private static string? _accessToken;
    private const string TokenFilePath = "/app/token.txt";

    public TiendaNubeService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TiendaNubeService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
        LoadTokenFromFile();
    }

    private void LoadTokenFromFile()
    {
        try
        {
            if (File.Exists(TokenFilePath))
            {
                _accessToken = File.ReadAllText(TokenFilePath);
                _logger.LogInformation("Token cargado desde archivo");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cargando token desde archivo");
        }
    }

    private void SaveTokenToFile(string token)
    {
        try
        {
            File.WriteAllText(TokenFilePath, token);
            _logger.LogInformation("Token guardado en archivo");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error guardando token en archivo");
        }
    }

    public async Task<TokenResponse?> GetAccessTokenAsync(string code)
    {
        var client = _httpClientFactory.CreateClient("TiendaNube");
        
        var requestData = new
        {
            client_id = _configuration["TiendaNube:ClientId"],
            client_secret = _configuration["TiendaNube:ClientSecret"],
            grant_type = "authorization_code",
            code
        };

        var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://www.tiendanube.com/apps/authorize/token", content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(jsonResponse);
            if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.access_token))
            {
                _accessToken = tokenResponse.access_token;
                SaveTokenToFile(_accessToken);
            }
            return tokenResponse;
        }

        _logger.LogError("Error obteniendo token: {StatusCode}", response.StatusCode);
        return null;
    }

    public async Task<object?> GetStoreInfoAsync()
    {
        if (string.IsNullOrEmpty(_accessToken))
            return null;

        var client = _httpClientFactory.CreateClient("TiendaNube");
        client.DefaultRequestHeaders.Add("Authentication", $"bearer {_accessToken}");

        var storeId = _configuration["TiendaNube:StoreId"];
        var response = await client.GetAsync($"https://api.tiendanube.com/v1/{storeId}/store");

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<object>(jsonResponse);
        }

        return null;
    }

    public async Task<object?> GetOrdersAsync()
    {
        if (string.IsNullOrEmpty(_accessToken))
            return null;

        var client = _httpClientFactory.CreateClient("TiendaNube");
        client.DefaultRequestHeaders.Add("Authentication", $"bearer {_accessToken}");

        var storeId = _configuration["TiendaNube:StoreId"];
        var response = await client.GetAsync($"https://api.tiendanube.com/v1/{storeId}/orders");

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<object>(jsonResponse);
        }

        return null;
    }

    public async Task<object?> RegisterPaymentProviderAsync()
    {
        if (string.IsNullOrEmpty(_accessToken))
            return null;

        var client = _httpClientFactory.CreateClient("TiendaNube");
        client.DefaultRequestHeaders.Add("Authentication", $"bearer {_accessToken}");

        var storeId = _configuration["TiendaNube:StoreId"];
        
        var paymentProvider = new Dictionary<string, object>
        {
            ["name"] = "SistePay",
            ["description"] = "Medio de pago SistePay",
            ["logo_urls"] = new Dictionary<string, string>
            {
                ["400x120"] = "https://www.sistecredito.com/wp-content/themes/sistecredito/assets/img/logo.svg",
                ["160x100"] = "https://www.sistecredito.com/wp-content/themes/sistecredito/assets/img/logo.svg"
            },
            ["configuration_url"] = "http://localhost:4200/payments",
            ["supported_currencies"] = new[] { "COP", "USD" },
            ["supported_payment_methods"] = new[]
            {
                new Dictionary<string, object>
                {
                    ["payment_method_type"] = "credit_card",
                    ["payment_methods"] = new[] { "visa", "mastercard", "amex" }
                }
            },
            ["checkout_payment_options"] = new[]
            {
                new Dictionary<string, object>
                {
                    ["id"] = "credit_card",
                    ["name"] = "Tarjeta de Crédito",
                    ["supported_billing_countries"] = new[] { "CO", "US" },
                    ["supported_payment_method_types"] = new[] { "credit_card" }
                }
            },
            ["checkout_js_url"] = $"https://3354daf8bf33.ngrok-free.app/checkout.js?v={DateTime.Now.Ticks}",
            ["enabled"] = true
        };

        var content = new StringContent(JsonSerializer.Serialize(paymentProvider), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"https://api.tiendanube.com/v1/{storeId}/payment_providers", content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<object>(jsonResponse);
        }

        _logger.LogError("Error registrando payment provider: {StatusCode} - {Content}", 
            response.StatusCode, await response.Content.ReadAsStringAsync());
        return null;
    }

    public async Task<object?> GetPaymentProvidersAsync()
    {
        if (string.IsNullOrEmpty(_accessToken))
            return null;

        var client = _httpClientFactory.CreateClient("TiendaNube");
        client.DefaultRequestHeaders.Add("Authentication", $"bearer {_accessToken}");

        var storeId = _configuration["TiendaNube:StoreId"];
        var response = await client.GetAsync($"https://api.tiendanube.com/v1/{storeId}/payment_providers");

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<object>(jsonResponse);
        }

        return null;
    }

    public async Task<string?> CreateTransactionAsync(string orderId, decimal amount)
    {
        if (string.IsNullOrEmpty(_accessToken))
            return null;

        var client = _httpClientFactory.CreateClient("TiendaNube");
        client.DefaultRequestHeaders.Add("Authentication", $"bearer {_accessToken}");

        var storeId = _configuration["TiendaNube:StoreId"];
        
        var transactionId = "SISTEPAY_" + Guid.NewGuid().ToString("N").Substring(0, 16).ToUpper();
        
        var transaction = new
        {
            order_id = orderId,
            amount = amount.ToString("F2"),
            currency = "COP",
            gateway = "sistepay",
            gateway_id = transactionId,
            status = "success",
            payment_method_id = "credit_card"
        };

        var content = new StringContent(JsonSerializer.Serialize(transaction), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"https://api.tiendanube.com/v1/{storeId}/orders/{orderId}/transactions", content);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Transacción creada exitosamente: {TransactionId}", transactionId);
            return transactionId;
        }

        var errorContent = await response.Content.ReadAsStringAsync();
        _logger.LogError("Error creando transacción: {StatusCode} - {Content}", response.StatusCode, errorContent);
        return null;
    }

    public async Task<object?> UpdatePaymentProviderAsync(string providerId, string checkoutJsUrl)
    {
        if (string.IsNullOrEmpty(_accessToken))
            return null;

        var client = _httpClientFactory.CreateClient("TiendaNube");
        client.DefaultRequestHeaders.Add("Authentication", $"bearer {_accessToken}");

        var storeId = _configuration["TiendaNube:StoreId"];
        
        var paymentProvider = new Dictionary<string, object>
        {
            ["checkout_js_url"] = checkoutJsUrl
        };

        var content = new StringContent(JsonSerializer.Serialize(paymentProvider), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://api.tiendanube.com/v1/{storeId}/payment_providers/{providerId}", content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<object>(jsonResponse);
        }

        _logger.LogError("Error actualizando payment provider: {StatusCode} - {Content}", 
            response.StatusCode, await response.Content.ReadAsStringAsync());
        return null;
    }

    public async Task<bool> DeletePaymentProviderAsync(string providerId)
    {
        if (string.IsNullOrEmpty(_accessToken))
            return false;

        var client = _httpClientFactory.CreateClient("TiendaNube");
        client.DefaultRequestHeaders.Add("Authentication", $"bearer {_accessToken}");

        var storeId = _configuration["TiendaNube:StoreId"];
        var response = await client.DeleteAsync($"https://api.tiendanube.com/v1/{storeId}/payment_providers/{providerId}");

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Payment Provider eliminado: {ProviderId}", providerId);
            return true;
        }

        _logger.LogError("Error eliminando payment provider: {StatusCode}", response.StatusCode);
        return false;
    }
}
