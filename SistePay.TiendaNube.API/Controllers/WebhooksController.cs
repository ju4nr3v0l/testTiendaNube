using Microsoft.AspNetCore.Mvc;

namespace SistePay.TiendaNube.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebhooksController : ControllerBase
{
    private readonly ILogger<WebhooksController> _logger;

    public WebhooksController(ILogger<WebhooksController> logger)
    {
        _logger = logger;
    }

    [HttpPost("orders")]
    public IActionResult OrdersCreated([FromBody] object orderData)
    {
        _logger.LogInformation("Webhook recibido: orders/created");
        _logger.LogInformation("Datos: {OrderData}", orderData);

        return Ok();
    }
}
