namespace SistePay.TiendaNube.API.Dtos;

public class PaymentRequestDto
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
}
