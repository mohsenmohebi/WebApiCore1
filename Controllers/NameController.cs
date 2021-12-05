using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApiWithToken.Controllers;
[ApiController]
[Route("[controller]")]
public class NameController:ControllerBase{

    private readonly ILogger<NameController> _logger;
    private readonly IJwtAuthenticationManger _jwtAuthenticationManager;

    public NameController(ILogger<NameController> logger, IJwtAuthenticationManger jwtAuthenticationManger){
        _logger = logger;
        _jwtAuthenticationManager = jwtAuthenticationManger;
    }

    [Authorize]
    [HttpGet("")]
    public IEnumerable<string> Get(){
        return new string[]{"name","value"};
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] UserCred userCred){
        _logger.LogWarning("Authenticate ....");
        var token =_jwtAuthenticationManager.Authenticate(userCred.Username,userCred.Password);
        if (!string.IsNullOrEmpty(token))
            return Ok(token);
        return Unauthorized();
    }


}