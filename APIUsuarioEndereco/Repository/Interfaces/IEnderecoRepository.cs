using APIUsuarioEndereco.Model;

namespace APIUsuarioEndereco.Repository;

public interface IEnderecoRepository
{
    Task<Endereco> ObterPorIdAsync(int id);
    Task<IEnumerable<Endereco>> ObterPorUsuarioIdAsync(int usuarioId);
    Task<Endereco> AdicionarAsync(Endereco endereco);
    Task AtualizarAsync(Endereco endereco);
    Task DeletarAsync(int id);
    Task<bool> ExisteAsync(int id);
}