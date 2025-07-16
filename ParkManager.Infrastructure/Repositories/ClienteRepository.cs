using Microsoft.EntityFrameworkCore;
using ParkManager.Domain;
using ParkManager.Domain.Interfaces;
using ParkManager.Infrastructure.Data;

namespace ParkManager.Infrastructure.Repositories
{
    public class ClienteRepository : ICliente
    {
        private readonly ParkManagerContext _context;

        public ClienteRepository(ParkManagerContext context)
        {
            _context = context;
        }

        public async Task<Cliente> AdicionarAsync(Cliente cliente)
        {
            if (cliente.Id == Guid.Empty)
                cliente.Id = Guid.NewGuid();

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> ObterPorIdAsync(Guid id)
        {
            return await _context.Clientes
                .Include(c => c.Veiculos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes
                .Include(c => c.Veiculos)
                .ToListAsync();
        }

        public async Task<Cliente> AtualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            var cliente = await ObterPorIdAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}