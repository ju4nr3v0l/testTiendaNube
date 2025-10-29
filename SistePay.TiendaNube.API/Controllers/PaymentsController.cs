using Microsoft.AspNetCore.Mvc;
using SistePay.TiendaNube.API.Dtos;
using SistePay.TiendaNube.API.Services;

namespace SistePay.TiendaNube.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> _logger;
    private readonly TiendaNubeService _tiendaNubeService;

    public PaymentsController(ILogger<PaymentsController> logger, TiendaNubeService tiendaNubeService)
    {
        _logger = logger;
        _tiendaNubeService = tiendaNubeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDto request)
    {
        _logger.LogInformation("Procesando pago: Order {OrderId} - Amount {Amount}", request.OrderId, request.Amount);

        // Crear transacción en Tienda Nube
        var transaction = await _tiendaNubeService.CreateTransactionAsync(request.OrderId, request.Amount);

        if (transaction == null)
        {
            return BadRequest("Error creando transacción");
        }

        return Ok(new { 
            transactionId = transaction,
            status = "approved"
        });
    }
}
