using APIUsuarioEndereco.Data;
using APIUsuarioEndereco.Model;
using Microsoft.EntityFrameworkCore;

namespace APIUsuarioEndereco.Repository;

public class EnderecoRepository: IEnderecoRepository
{
    private readonly AppDbContext _context;

    public EnderecoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Endereco> ObterPorIdAsync(int id)
    {
        return await _context.Enderecos
            .Include(e => e.Usuario)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Endereco>> ObterPorUsuarioIdAsync(int usuarioId)
    {
        return await _context.Enderecos
            .Where(e => e.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<Endereco> AdicionarAsync(Endereco endereco)
    {
        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync();
        return endereco;
    }

    public async Task AtualizarAsync(Endereco endereco)
    {
        _context.Entry(endereco).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(int id)
    {
        var endereco = await _context.Enderecos.FindAsync(id);
        if (endereco != null)
        {
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Enderecos.AnyAsync(e => e.Id == id);
    }
 
}