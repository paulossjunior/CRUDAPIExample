using APIUsuarioEndereco.DTO;
using APIUsuarioEndereco.Model;
using APIUsuarioEndereco.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APIUsuarioEndereco.Controller;

[ApiController]
[Route("api/[controller]")]
public class EnderecoController : ControllerBase
{
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public EnderecoController(IEnderecoRepository enderecoRepository, IUsuarioRepository usuarioRepository)
    {
        _enderecoRepository = enderecoRepository;
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Endereco>> CriarEndereco(EnderecoDTO enderecoDTO, int usuarioId)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);
        if (usuario == null)
        {
            return NotFound($"Usuário com ID {usuarioId} não encontrado");
        }

        var endereco = new Endereco
        {
            Logradouro = enderecoDTO.Logradouro,
            Numero = enderecoDTO.Numero,
            Complemento = enderecoDTO.Complemento,
            Bairro = enderecoDTO.Bairro,
            Cidade = enderecoDTO.Cidade,
            Estado = enderecoDTO.Estado,
            CEP = enderecoDTO.CEP,
            UsuarioId = usuarioId
        };
        
        
        await _enderecoRepository.AdicionarAsync(endereco);
        return CreatedAtAction(nameof(ObterEndereco), new { id = endereco.Id }, endereco);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Endereco>> ObterEndereco(int id)
    {
        var endereco = await _enderecoRepository.ObterPorIdAsync(id);
        if (endereco == null)
        {
            return NotFound();
        }

        return endereco;
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<Endereco>>> ListarEnderecosPorUsuario(int usuarioId)
    {
        return Ok(await _enderecoRepository.ObterPorUsuarioIdAsync(usuarioId));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarEndereco(int id, EnderecoDTO enderecoDTO)
    {
        if (!await _enderecoRepository.ExisteAsync(id))
        {
            return NotFound();
        }

        var endereco = await _enderecoRepository.ObterPorIdAsync(id);
        endereco.Logradouro = enderecoDTO.Logradouro;
        endereco.Numero = enderecoDTO.Numero;
        endereco.Complemento = enderecoDTO.Complemento;
        endereco.Bairro = enderecoDTO.Bairro;
        endereco.Cidade = enderecoDTO.Cidade;
        endereco.Estado = enderecoDTO.Estado;
        endereco.CEP = enderecoDTO.CEP;

        await _enderecoRepository.AtualizarAsync(endereco);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarEndereco(int id)
    {
        if (!await _enderecoRepository.ExisteAsync(id))
        {
            return NotFound();
        }

        await _enderecoRepository.DeletarAsync(id);
        return NoContent();
    }
}
