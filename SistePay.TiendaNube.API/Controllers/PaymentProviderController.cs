using Microsoft.AspNetCore.Mvc;
using SistePay.TiendaNube.API.Services;

namespace SistePay.TiendaNube.API.Controllers;

[ApiController]
[Route("api/payment-provider")]
public class PaymentProviderController : ControllerBase
{
    private readonly TiendaNubeService _tiendaNubeService;
    private readonly ILogger<PaymentProviderController> _logger;

    public PaymentProviderController(TiendaNubeService tiendaNubeService, ILogger<PaymentProviderController> logger)
    {
        _tiendaNubeService = tiendaNubeService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterPaymentProvider()
    {
        var result = await _tiendaNubeService.RegisterPaymentProviderAsync();
        
        if (result == null)
            return BadRequest("Error registrando payment provider");

        return Ok(result);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetPaymentProviders()
    {
        var result = await _tiendaNubeService.GetPaymentProvidersAsync();
        
        if (result == null)
            return NotFound("No se pudieron obtener los payment providers");

        return Ok(result);
    }
}
