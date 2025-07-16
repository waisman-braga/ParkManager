using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkManager.Domain.Interfaces
{
    public interface IMensalista
    {
        Task<Mensalista> AdicionarAsync(Mensalista mensalista);
        Task<Mensalista> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Mensalista>> ObterTodosAsync();
        Task<IEnumerable<Mensalista>> ObterAtivosAsync();
        Task<Mensalista> ObterPorClienteAsync(Guid clienteId);
        Task<Mensalista> AtualizarAsync(Mensalista mensalista);
        Task<bool> DeletarAsync(Guid id);
    }
}