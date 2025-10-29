using Microsoft.AspNetCore.Mvc;
using SistePay.TiendaNube.API.Services;

namespace SistePay.TiendaNube.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly TiendaNubeService _tiendaNubeService;

    public OrdersController(TiendaNubeService tiendaNubeService)
    {
        _tiendaNubeService = tiendaNubeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _tiendaNubeService.GetOrdersAsync();
        
        if (orders == null)
            return NotFound("No se pudieron obtener las órdenes");

        return Ok(orders);
    }
}
