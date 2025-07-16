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
    public class VeiculoRepository : IVeiculo
    {
        private readonly ParkManagerContext _context;

        public VeiculoRepository(ParkManagerContext context)
        {
            _context = context;
        }

        public async Task<Veiculo> AdicionarAsync(Veiculo veiculo)
        {
            if (veiculo.Id == Guid.Empty)
                veiculo.Id = Guid.NewGuid();

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
            return veiculo;
        }

        public async Task<Veiculo> ObterPorIdAsync(Guid id)
        {
            return await _context.Veiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Veiculo> ObterPorPlacaAsync(string placa)
        {
            return await _context.Veiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Placa == placa);
        }

        public async Task<IEnumerable<Veiculo>> ObterTodosAsync()
        {
            return await _context.Veiculos
                .Include(v => v.Cliente)
                .ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> ObterPorClienteAsync(Guid clienteId)
        {
            return await _context.Veiculos
                .Where(v => v.ClienteId == clienteId)
                .Include(v => v.Cliente)
                .ToListAsync();
        }

        public async Task<Veiculo> AtualizarAsync(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();
            return veiculo;
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            var veiculo = await ObterPorIdAsync(id);
            if (veiculo != null)
            {
                _context.Veiculos.Remove(veiculo);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> PlacaExisteAsync(string placa)
        {
            return await _context.Veiculos
                .AnyAsync(v => v.Placa == placa);
        }

        public async Task<bool> PlacaExisteAsync(string placa, Guid veiculoId)
        {
            return await _context.Veiculos
                .AnyAsync(v => v.Placa == placa && v.Id != veiculoId);
        }
    }
}