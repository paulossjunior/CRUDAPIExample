using APIUsuarioEndereco.DTO;
using APIUsuarioEndereco.Model;
using APIUsuarioEndereco.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APIUsuarioEndereco.Controller;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> CriarUsuario(UsuarioDTO usuarioDTO)
    {
        var usuario = new Usuario
        {
            Nome = usuarioDTO.Nome,
            Email = usuarioDTO.Email,
            Enderecos = new List<Endereco>() 
        };

        await _usuarioRepository.AdicionarAsync(usuario);
        return CreatedAtAction(nameof(ObterUsuario), new { id = usuario.Id }, usuario);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> ObterUsuario(int id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        return usuario;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> ListarUsuarios()
    {
        return Ok(await _usuarioRepository.ObterTodosAsync());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarUsuario(int id, UsuarioDTO usuarioDTO)
    {
        if (!await _usuarioRepository.ExisteAsync(id))
        {
            return NotFound();
        }

        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        usuario.Nome = usuarioDTO.Nome;
        usuario.Email = usuarioDTO.Email;

        await _usuarioRepository.AtualizarAsync(usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarUsuario(int id)
    {
        if (!await _usuarioRepository.ExisteAsync(id))
        {
            return NotFound();
        }

        await _usuarioRepository.DeletarAsync(id);
        return NoContent();
    }
}