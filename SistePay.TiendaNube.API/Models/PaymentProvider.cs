namespace SistePay.TiendaNube.API.Models;

public class PaymentProviderRequest
{
    public string name { get; set; } = "SistePay";
    public string description { get; set; } = "Medio de pago SistePay";
    public string logo_urls { get; set; } = "https://sistecredito.com/logo.png";
    public string configuration_url { get; set; } = "http://localhost:4200/payments";
    public List<string> supported_currencies { get; set; } = new() { "COP", "USD" };
    public List<string> supported_payment_methods { get; set; } = new() { "credit_card", "debit_card" };
    public bool enabled { get; set; } = true;
    public List<string> checkout_payment_options { get; set; } = new() { "credit_card" };
    public string checkout_js_url { get; set; } = "";
    public List<string> rates_definition { get; set; } = new() { "percentage" };
}
