using Microsoft.EntityFrameworkCore;
using ParkManager.Domain;
using ParkManager.Domain.Interfaces;
using ParkManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkManager.Infrastructure.Repositories
{
    public class MensalistaRepository : IMensalista
    {
        private readonly ParkManagerContext _context;

        public MensalistaRepository(ParkManagerContext context)
        {
            _context = context;
        }

        public async Task<Mensalista> AdicionarAsync(Mensalista mensalista)
        {
            if (mensalista.Id == Guid.Empty)
                mensalista.Id = Guid.NewGuid();

            _context.Mensalistas.Add(mensalista);
            await _context.SaveChangesAsync();
            return mensalista;
        }

        public async Task<Mensalista> ObterPorIdAsync(Guid id)
        {
            return await _context.Mensalistas
                .Include(m => m.Cliente)
                .Include(m => m.Veiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Mensalista>> ObterTodosAsync()
        {
            return await _context.Mensalistas
                .Include(m => m.Cliente)
                .Include(m => m.Veiculo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Mensalista>> ObterAtivosAsync()
        {
            return await _context.Mensalistas
                .Include(m => m.Cliente)
                .Include(m => m.Veiculo)
                .Where(m => m.Ativo)
                .ToListAsync();
        }

        public async Task<Mensalista> ObterPorClienteAsync(Guid clienteId)
        {
            return await _context.Mensalistas
                .Include(m => m.Cliente)
                .Include(m => m.Veiculo)
                .FirstOrDefaultAsync(m => m.ClienteId == clienteId && m.Ativo);
        }

        public async Task<Mensalista> AtualizarAsync(Mensalista mensalista)
        {
            _context.Mensalistas.Update(mensalista);
            await _context.SaveChangesAsync();
            return mensalista;
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            var mensalista = await ObterPorIdAsync(id);
            if (mensalista != null)
            {
                _context.Mensalistas.Remove(mensalista);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}