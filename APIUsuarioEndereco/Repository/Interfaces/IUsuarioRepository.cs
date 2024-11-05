using APIUsuarioEndereco.Model;

namespace APIUsuarioEndereco.Repository;

public interface IUsuarioRepository
{
    Task<Usuario> ObterPorIdAsync(int id);
    Task<IEnumerable<Usuario>> ObterTodosAsync();
    Task<Usuario> AdicionarAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
    Task DeletarAsync(int id);
    Task<bool> ExisteAsync(int id);
}