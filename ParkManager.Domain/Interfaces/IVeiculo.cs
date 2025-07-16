using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkManager.Domain.Interfaces
{
    public interface IVeiculo
    {
        Task<Veiculo> AdicionarAsync(Veiculo veiculo);
        Task<Veiculo> ObterPorIdAsync(Guid id);
        Task<Veiculo> ObterPorPlacaAsync(string placa);
        Task<IEnumerable<Veiculo>> ObterTodosAsync();
        Task<IEnumerable<Veiculo>> ObterPorClienteAsync(Guid clienteId);
        Task<Veiculo> AtualizarAsync(Veiculo veiculo);
        Task<bool> DeletarAsync(Guid id);
        Task<bool> PlacaExisteAsync(string placa);
        Task<bool> PlacaExisteAsync(string placa, Guid veiculoId);
    }
}