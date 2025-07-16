using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkManager.Domain.Interfaces
{
    public interface IFaturamento
    {
        Task<IEnumerable<Faturamento>> ObterTodosAsync();
        Task<Faturamento> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Faturamento>> ObterPorMensalistaAsync(Guid mensalistaId);
        Task<IEnumerable<Faturamento>> ObterPorMesAnoAsync(int mesAno);
        Task<IEnumerable<Faturamento>> ObterPendentesAsync();
        Task<Faturamento> AdicionarAsync(Faturamento faturamento);
        Task<Faturamento> AtualizarAsync(Faturamento faturamento);
        Task<bool> DeletarAsync(Guid id);
        Task<IEnumerable<Faturamento>> GerarFaturamentoMensalAsync(int mesAno);
        Task<bool> MarcarComoPagoAsync(Guid faturamentoId, DateTime dataPagamento);
    }
}
