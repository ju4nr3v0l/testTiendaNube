using Microsoft.AspNetCore.Mvc;
using SistePay.TiendaNube.API.Dtos;

namespace SistePay.TiendaNube.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(ILogger<PaymentsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CreatePayment([FromBody] PaymentRequestDto request)
    {
        _logger.LogInformation("Creando pago: {Amount} - {Description}", request.Amount, request.Description);

        var paymentId = Guid.NewGuid().ToString();
        var paymentUrl = $"https://checkout.sistepay.com/payment/{paymentId}";

        return Ok(new { paymentUrl, paymentId });
    }
}
