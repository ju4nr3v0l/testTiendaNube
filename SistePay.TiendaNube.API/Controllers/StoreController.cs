using Microsoft.AspNetCore.Mvc;
using SistePay.TiendaNube.API.Services;

namespace SistePay.TiendaNube.API.Controllers;

[ApiController]
[Route("api/store")]
public class StoreController : ControllerBase
{
    private readonly TiendaNubeService _tiendaNubeService;

    public StoreController(TiendaNubeService tiendaNubeService)
    {
        _tiendaNubeService = tiendaNubeService;
    }

    [HttpGet("info")]
    public async Task<IActionResult> GetStoreInfo()
    {
        var storeInfo = await _tiendaNubeService.GetStoreInfoAsync();
        
        if (storeInfo == null)
            return NotFound("No se pudo obtener información de la tienda");

        return Ok(storeInfo);
    }
}
