using Microsoft.AspNetCore.Mvc;
using TokenAPI.Services;
using TokenAPI.Data.TokenModels;
using TokenAPI.Data.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace TokenAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class ClientControllers : ControllerBase
{

private readonly ClienteService _service;
private IConfiguration Config; 
private readonly IConfiguration _config;

    public ClientControllers(ClienteService cliente, IConfiguration config){
        this._service = cliente;
        this.Config = config;
    }


[HttpGet]
public async Task<IEnumerable<Cliente>> Get(){
    return await _service.GetAll();
}


[HttpGet("{id}")]
public async Task<ActionResult<Cliente>> GetById(int id){

var cliente = await _service.GetById(id);

if(cliente is null)
return NotFound();

return cliente;

}




[HttpPost]
public async Task<IActionResult> Create (Cliente cliente){
    
    var newCliente = await _service.Create(cliente);

    return CreatedAtAction(nameof(GetById),new {id = newCliente.Id}, newCliente);
}


[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, Cliente cliente)
{
if(id != cliente.Id)
return BadRequest();

    
var clienteToUpdate = _service.GetById(id);

if(clienteToUpdate is not null){
    await _service.Update(id, cliente);
    return NoContent();
}else{
    return NotFound();
}

}


[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    var clienteToDelete = await _service.GetById(id); 

    if(clienteToDelete is not null)
    {
        await _service.Delete(id); 
        return Ok();
    }
    else
    {
        return NotFound();
    }
}




[HttpGet("token")]
public IActionResult GetToken()
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(Config["Jwt:Key"]);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "username")
        }),
        Expires = DateTime.UtcNow.AddMinutes(10), // Tiempo de vida de 10 minutos
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return Ok(new { token = tokenHandler.WriteToken(token) });
}


[HttpGet("validate-token")]
public IActionResult ValidateToken()
{
    var currentUser = HttpContext.User;

    // Verifica si el token todavía está vigente
    if (currentUser.Identity.IsAuthenticated)
    {
        return Ok("El token JWT aún está vigente.");
    }
    else
    {
        return Unauthorized("El token JWT ha expirado o es inválido.");
    }


    }
}
