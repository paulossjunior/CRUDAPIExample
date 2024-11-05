using APIUsuarioEndereco.Data;
using APIUsuarioEndereco.Model;
using Microsoft.EntityFrameworkCore;

namespace APIUsuarioEndereco.Repository;

public class UsuarioRepository: IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> ObterPorIdAsync(int id)
    {
        return await _context.Usuarios
            .Include(u => u.Enderecos)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        return await _context.Usuarios
            .Include(u => u.Enderecos)
            .ToListAsync();
    }

    public async Task<Usuario> AdicionarAsync(Usuario usuario)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));
        
        try
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao adicionar usuário", ex);
        }
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        _context.Entry(usuario).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Usuarios.AnyAsync(u => u.Id == id);
    }

}