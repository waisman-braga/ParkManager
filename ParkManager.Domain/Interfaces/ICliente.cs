using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkManager.Domain.Interfaces
{
    public interface ICliente
    {
        Task<Cliente> AdicionarAsync(Cliente cliente);
        Task<Cliente> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task<Cliente> AtualizarAsync(Cliente cliente);
        Task<bool> DeletarAsync(Guid id);
    }
}