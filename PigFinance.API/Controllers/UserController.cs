using Microsoft.AspNetCore.Mvc;
using PigFinance.API.Models;
using PigFinance.API.Services;
using System.Net.Mime; 

namespace PigFinance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        
        [HttpPost("register")]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<Usuario> Register([FromBody] Usuario usuario)
        {
            try
            {
               
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

               
                var novoUsuario = _service.Register(usuario);

                return CreatedAtAction(nameof(GetById), new { id = novoUsuario.Id }, novoUsuario);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Erro ao registrar usuário: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

       
        [HttpPost("authenticate")]
        public ActionResult<Usuario> Authenticate([FromBody] LoginRequest loginRequest)
        {
            try
            {
                
                var usuario = _service.Authenticate(loginRequest.Email, loginRequest.Senha);

                if (usuario == null)
                {
                    
                    return Unauthorized("Credenciais inválidas.");
                }

                
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante a autenticação: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno durante a autenticação.");
            }
        }

        
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetById(int id)
        {
            try
            {
                var usuario = _service.GetById(id);

                if (usuario == null)
                {
                    return NotFound($"Usuário com ID {id} não encontrado.");
                }

                
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro interno ao buscar o usuário.");
            }
        }
    }

    
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
