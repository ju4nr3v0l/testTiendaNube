namespace SistePay.TiendaNube.API.Dtos;

public class PaymentRequestDto
{
    public string OrderId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = "approved";
    public string Description { get; set; } = string.Empty;
}
