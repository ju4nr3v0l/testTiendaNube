using Microsoft.AspNetCore.Mvc;
using SistePay.TiendaNube.API.Dtos;
using SistePay.TiendaNube.API.Services;

namespace SistePay.TiendaNube.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TiendaNubeService _tiendaNubeService;

    public AuthController(TiendaNubeService tiendaNubeService)
    {
        _tiendaNubeService = tiendaNubeService;
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetToken([FromBody] TokenRequestDto request)
    {
        var token = await _tiendaNubeService.GetAccessTokenAsync(request.Code);
        
        if (token == null)
            return BadRequest("Error obteniendo token");

        return Ok(token);
    }
}
